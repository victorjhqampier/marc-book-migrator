using Application.Adapters.Internals;
using Application.Helpers;
using Application.Interfaces;
using Domain.Interfaces;
using System.Text.Json;

namespace Application.Usecases.BookCase;

public class BookMigrateUsecase : IBookMigrateApplication
{
    private readonly IBookQueryInfrastructure _dbQuery;
    private readonly IBookCommandInfrastructure _dbCommand;
    private readonly IBookTrackerQueryInfrastructure _dbTrackerQuery;

    public BookMigrateUsecase(IBookQueryInfrastructure dbQuery, IBookCommandInfrastructure dbCommand, IBookTrackerQueryInfrastructure dbTrackerQuery) { 
        _dbQuery = dbQuery; _dbCommand = dbCommand; _dbTrackerQuery = dbTrackerQuery;
    }

    public async Task<ResponseInternalAdapter> ProcessAsync()
    {
        if(_dbTrackerQuery.ProccessedYet() == true) return EasyResponseHelper.EasySuccessRespond(new { Actions = "Not Started", Status = "Already processed ..." });

        var nTotal = _dbQuery.CountTotalBook();
        int pageSize = 50;
        int currentPage = 0;

        while (nTotal > 0)
        {
            var arrPag = _dbQuery.GetPagnateBook(currentPage * pageSize, pageSize);
            //var arrPag = new List<int> { 488, 720, 923, 931, 1034, 1041, 1089, 1102, 1154, 1171, 1240, 1302 };
            if (arrPag.Count == 0) break;

            await this.StartMigrationAsync(arrPag);

            nTotal -= arrPag.Count;
            currentPage++;
        }

        return EasyResponseHelper.EasySuccessRespond(new {Actions="Started", Status="Proccessing ..."});
    }

    public async Task<ResponseInternalAdapter> ReprocessAsync()
    {
        var nTotal = _dbTrackerQuery.CountTotalError();
        int pageSize = 20;
        int currentPage = 0;

        while (nTotal > 0)
        {
            var arrPag = _dbTrackerQuery.GetIdsError(currentPage * pageSize, pageSize);
            if (arrPag.Count == 0) break;

            await this.RetryMigrationAsync(arrPag);

            nTotal -= arrPag.Count;
            currentPage++;
        }

        return EasyResponseHelper.EasySuccessRespond(new { Actions = "Reprocessed", Status = "Finished ..." });
    }

    private async Task StartMigrationAsync(List<int> arrIdBooks)
    {
        if (arrIdBooks.Count() == 0) return;

        string booTitle = string.Empty;
        string ErrorMessage = string.Empty;

        foreach (int idBook in arrIdBooks)
        {
            try
            {
                var book = await _dbQuery.GetTitleAsync(idBook);
                if(book == null)
                {
                    ErrorMessage = "No Book";
                    throw new Exception(ErrorMessage);
                }
                booTitle = book.CTitle ?? string.Empty;
                var arrTitleValid = BookTitleValidation.ValidateTitle(book);
                if(arrTitleValid.Count > 0)
                {
                    ErrorMessage = JsonSerializer.Serialize(arrTitleValid);
                    throw new Exception("No Book");
                }                
                //return EasyResponseHelper.EasySuccessRespond(data);

                var author = await _dbQuery.GetAuthorAsync(idBook);
                if (author == null || author.Count()==0)
                {
                    ErrorMessage = "No Authors";
                    throw new Exception(ErrorMessage);
                }
                //return EasyResponseHelper.EasySuccessRespond(data);

                var classify = await _dbQuery.GetClassificationAsync(idBook);
                if (classify == null)
                {
                    ErrorMessage = "No Classification";
                    throw new Exception(ErrorMessage);
                }
                //return EasyResponseHelper.EasySuccessRespond(data);

                var serie = await _dbQuery.GetSerialAsync(idBook);
                //return EasyResponseHelper.EasySuccessRespond(data);

                var publisher = await _dbQuery.GetPublisherAsync(idBook);
                if (publisher == null || publisher.Count() == 0)
                {
                    ErrorMessage = "No Publishers";
                    throw new Exception(ErrorMessage);
                }
                //return EasyResponseHelper.EasySuccessRespond(data);

                var copy = await _dbQuery.GetCopyAsync(idBook);
                if (copy == null || copy.Count() == 0)
                {
                    ErrorMessage = "No Copies";
                    throw new Exception(ErrorMessage);
                }
                var processcopy = BooksCopyProccess.ProcessCopy(copy);

                await _dbCommand.SaveCompleteBook(book, author, classify, publisher, processcopy.Processed, serie);
                await _dbCommand.SaveDuplicateCopies(book.IdTitle, book.CTitle ?? string.Empty, processcopy.Excessed);
            }
            catch
            {
                await _dbCommand.SaveErrorBook(idBook, booTitle, ErrorMessage);
            }
        }
    }

    private async Task RetryMigrationAsync(List<int> arrIdBooks)
    {
        if (arrIdBooks.Count() == 0) return;

        string booTitle = string.Empty;
        string ErrorMessage = string.Empty;

        foreach (int idBook in arrIdBooks)
        {
            try
            {
                var book = await _dbQuery.GetTitleAsync(idBook);
                if (book == null)
                {
                    ErrorMessage = "No Book";
                    throw new Exception(ErrorMessage);
                }
                booTitle = book.CTitle ?? string.Empty;
                var arrTitleValid = BookTitleValidation.ValidateTitle(book);
                if (arrTitleValid.Count > 0)
                {
                    ErrorMessage = JsonSerializer.Serialize(arrTitleValid);
                    throw new Exception("No Book");
                }

                var author = await _dbQuery.GetAuthorAsync(idBook);
                if (author == null || author.Count() == 0)
                {
                    ErrorMessage = "No Authors";
                    throw new Exception(ErrorMessage);
                }

                var classify = await _dbQuery.GetClassificationAsync(idBook);
                if (classify == null)
                {
                    ErrorMessage = "No Classification";
                    throw new Exception(ErrorMessage);
                }

                var serie = await _dbQuery.GetSerialAsync(idBook);

                var publisher = await _dbQuery.GetPublisherAsync(idBook);
                if (publisher == null || publisher.Count() == 0)
                {
                    ErrorMessage = "No Publishers";
                    throw new Exception(ErrorMessage);
                }

                var copy = await _dbQuery.GetCopyAsync(idBook);
                if (copy == null || copy.Count() == 0)
                {
                    ErrorMessage = "No Copies";
                    throw new Exception(ErrorMessage);
                }
                var processcopy = BooksCopyProccess.ProcessCopy(copy);

                await _dbCommand.SaveCompleteBook(book, author, classify, publisher, processcopy.Processed, serie);
                await _dbCommand.SaveDuplicateCopies(book.IdTitle, book.CTitle ?? string.Empty, processcopy.Excessed);
                await _dbCommand.UpdateErrorStatusBook(idBook, true, "Retry Success ...");
            }
            catch
            {
                await _dbCommand.UpdateErrorStatusBook(idBook, false, ErrorMessage);
            }
        }
    }
}
