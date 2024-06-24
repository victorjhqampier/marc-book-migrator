using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoInfrastructure.Collections;
using MongoInfrastructure.Collections.Attributes;

namespace MongoInfrastructure.Commands;

public class BookCommandInfrastructure : IBookCommandInfrastructure
{
    private readonly IMongoCollection<BookMarcCollection> _dbSuccess;
    private readonly IMongoCollection<BookErrorCollection> _dbError;
    private readonly IMongoCollection<BookEvaluateCollection> _dbWarning;

    public BookCommandInfrastructure(IOptions<MongodbContext> _dbMongo)
    {
        var mongoClient = new MongoClient(_dbMongo.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(_dbMongo.Value.DatabaseName);
        _dbSuccess = mongoDatabase.GetCollection<BookMarcCollection>("BookSuccess");
        _dbError = mongoDatabase.GetCollection<BookErrorCollection>("BookError");
        _dbWarning = mongoDatabase.GetCollection<BookEvaluateCollection>("BookWarning");
    }

    public async Task SaveCompleteBook(MarcTitleEntity objTitle, List<MarcAuthorEntity> arrAutor, MarcCassifyEntity objClasify, List<MarcPublishEntity> arrPublisher, List<MarcCopyEntity> arrCopy, MarcSerieEntity? objSerie = null)
    {
        var newObject = new BookMarcCollection()
        {
            IdTitle = objTitle.IdTitle,
            Dewey = objTitle.CDewey??string.Empty,
            Title = objTitle.CTitle ?? string.Empty,
            Subtitle = objTitle.CSubtitle ?? string.Empty,
            Edition = objTitle.CEdition ?? string.Empty,
            Released = objTitle.NReleased ?? string.Empty,
            Content = objTitle.CContent ?? string.Empty,
            Isbn = objTitle.CIsbn ?? string.Empty,
            Physicaldesc = objTitle.CPhysicaldesc ?? string.Empty,
            Notes = objTitle.CNotes ?? string.Empty,
            Topics = objTitle.CTopics ?? string.Empty,
            Type = objTitle.CType,
            Image = objTitle.CImage ?? string.Empty,
            arrAuthor = arrAutor.Select(x=>new MarcAuthorAttribute
            {
                IdAuthor = x.IdAuthor,
                IdTitle = x.IdTitle,
                Role = x.CRole,
                Surname = x.CSurname,
                Name = x.CName,
                Date = x.CDate
            }).ToList(),
            objClassification = new MarcCassifyAttribute()
            {
                IdClasification= objClasify.IdClasification,
                IdTitle=objClasify.IdTitle,
                Dewey=objClasify.CDewey??string.Empty,
                Description=objClasify.CDescription
            },
            arrPublisher = arrPublisher.Select(x=>new MarcPublishAttribute 
            {
                IdPublisher=x.IdPublisher,
                IdTitle=x.IdTitle,
                Name=x.CName,
                Place=x.CPlace??string.Empty
            }).ToList(),
            objSerie = objSerie == null? null: new MarcSerieAttribute()
            {
                IdSerial= objSerie.IdSerial,
                IdTitle=objSerie.IdTitle,
                Number=objSerie.CNumber,
                Title=objSerie.CTitle
            },
            arrCopy = arrCopy.Select(x=>new MarcCopyAttribute 
            {
                IdCopy = x.IdCopy,
                IdTitle = x.IdTitle,
                Barcode = x.CBarcode??string.Empty,
                Notation = x.CNotation ?? string.Empty,
                IdDocumentType = x.IdDocumentType,
                DocumentType = x.CDocumentType ?? string.Empty,
                IdLocation = x.IdLocation,
                Location = x.CLocation ?? string.Empty,
                IdSection = x.IdSection,
                Section = x.CSection ?? string.Empty,
                IdStatus = x.IdStatus,
                Status = x.CStatus ?? string.Empty
            }).ToList(),
            ExportedAt = DateTime.Now
        };
        await _dbSuccess.InsertOneAsync(newObject);
    }

    public async Task SaveErrorBook(int idTitle, string title, string MessageError)
    {
        await _dbError.InsertOneAsync(new BookErrorCollection
        {
            IdTitle = idTitle,
            Title = title,
            MessageError = MessageError,
            CreatedAt = DateTime.Now
        });
    }
    public async Task UpdateErrorStatusBook(int idTitle, bool isSuccessed = true, string? message = null)
    {
        var update = Builders<BookErrorCollection>.Update
            .Set(x => x.UpdatedAt, DateTime.Now)
            .Set(x=> x.IsReproccessed, isSuccessed);

        if (!string.IsNullOrEmpty(message))
        {
            update = update.Set(x => x.MessageError, message);
        }

        await _dbError.UpdateOneAsync(update: update, filter: Builders<BookErrorCollection>.Filter.Eq(x => x.IdTitle, idTitle),  options: new UpdateOptions { IsUpsert = true });
        return;
    }

    public async Task SaveDuplicateCopies(int idTitle, string title, List<MarcCopyEntity> arrCopy)
    {
        if (arrCopy.Count() == 0) return;
        await _dbWarning.InsertOneAsync(new BookEvaluateCollection
        {
            IdTitle = idTitle,
            Title = title,
            arrCopy = arrCopy.Select(x => new MarcCopyAttribute
            {
                IdCopy = x.IdCopy,
                IdTitle = x.IdTitle,
                Barcode = x.CBarcode ?? string.Empty,
                Notation = x.CNotation ?? string.Empty,
                IdDocumentType = x.IdDocumentType,
                DocumentType = x.CDocumentType ?? string.Empty,
                IdLocation = x.IdLocation,
                Location = x.CLocation ?? string.Empty,
                IdSection = x.IdSection,
                Section = x.CSection ?? string.Empty,
                IdStatus = x.IdStatus,
                Status = x.CStatus ?? string.Empty
            }).ToList(),
            CreatedAt = DateTime.Now
        });
    }
}
