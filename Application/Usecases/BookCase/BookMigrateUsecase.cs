using Application.Adapters.Internals;
using Application.Helpers;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Usecases.BookCase;

public class BookMigrateUsecase : IBookMigrateApplication
{
    private readonly IBookQueryInfrastructure _dbQuery;

    public BookMigrateUsecase(IBookQueryInfrastructure dbQuery) => _dbQuery = dbQuery;

    public async Task<ResponseInternalAdapter> GetVariableAsync()
    {
        var nTotal = _dbQuery.CountTotalBook();
        return EasyResponseHelper.EasySuccessRespond(new {total= nTotal});
    }
}
