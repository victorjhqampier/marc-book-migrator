using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExtractDatabaseInfrastructure.Queries;

public class BookQueryExtracDatabase : IBookQueryInfrastructure
{
    private readonly Collections.EntityFrameworkContext _db;

    public BookQueryExtracDatabase(Collections.EntityFrameworkContext extracDatabase) => _db = extracDatabase;

    public int CountTotalBook()
    {
        return _db.Axtitles.Count();
    }

    public List<int> GetPagnateBook(int offset = 0, int limit = 100)
    {
        return _db.Axtitles
            .Select(x=>(int)x.IdTitle)
            .Skip(offset)
            .Take(limit)
            .ToList();
    }

    public async Task<MarcTitleEntity> GetTitleAsync(int idTtile)
    {
        return await _db.Axtitles
            .Where(x => x.IdTitle == idTtile)
            .Select(x => new MarcTitleEntity
            {
                IdTitle = (int) x.IdTitle,
                CDewey = x.CDewey,
                CTitle = x.CTitle,
                CSubtitle = x.CSubtitle,
                CEdition = x.CEdition,
                NReleased = x.NReleased,
                CContent = x.CContent,
                CIsbn = x.CIsbn,
                CPhysicaldesc = x.CPhysicaldesc,
                CNotes = x.CNotes,
                CTopics = x.CTopics,
                CType = x.CType,
                CImage = x.CImage
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<MarcCopyEntity>> GetCopyAsync(int idTtile)
    {
        return await _db.Axcopies
            .Where(x => x.IdTitle == idTtile)
            .OrderBy(x=>x.CBarcode)
            .Select(x => new MarcCopyEntity
            {
                IdCopy = (int)x.IdCopy,
                IdTitle = (int)x.IdTitle,
                CBarcode = x.CBarcode,
                CNotation = x.CNotation,
                IdDocumentType = (int)x.IdDocumentType,
                CDocumentType = x.CDocumentType,
                IdLocation = x.IdLocation,
                CLocation = x.CLocation,
                IdSection = x.IdSection,
                CSection = x.CSection,
                IdStatus = x.IdStatus,
                CStatus = x.CStatus
            })
            .ToListAsync();
    }

    public async Task<List<MarcPublishEntity>> GetPublisherAsync(int idTtile)
    {
        return await _db.Axpublishers
            .Where(x => x.IdTitle == idTtile)
            .Select(x => new MarcPublishEntity
            {
                IdPublisher = (int)x.IdPublisher,
                IdTitle = (int)x.IdTitle,
                CName = x.CName,
                CPlace = x.CPlace
            })
            .ToListAsync();
    }

    public async Task<MarcSerieEntity?> GetSerialAsync(int idTtile)
    {
        return await _db.Axserials.Where(x => x.IdTitle == idTtile)
            .Select(x => new MarcSerieEntity
            {
                IdSerial = (int)x.IdSerial,
                IdTitle = (int)x.IdTitle,
                CNumber = x.CNumber,
                CTitle = x.CTitle
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<MarcAuthorEntity>> GetAuthorAsync(int idTtile)
    {
        return await _db.Axauthors
            .Where(x => x.IdTitle == idTtile)
            .Select(x => new MarcAuthorEntity
            {
                IdAuthor = (int)x.IdAuthor,
                IdTitle = (int)x.IdTitle,
                CRole = x.CRole,
                CSurname = x.CSurname,
                CName = x.CName,
                CDate = x.CDate
            })
            .ToListAsync();
    }

    public async Task<MarcCassifyEntity> GetClassificationAsync (int idTitle)
    {
        return await _db.Axclasifications
            .Where(x => x.IdTitle == idTitle)
            .Select(x => new MarcCassifyEntity
            {
                IdClasification = (int)x.IdClasification,
                IdTitle = (int)x.IdTitle,
                CDewey = x.CDewey,
                CDescription = x.CDescription
            })
            .FirstOrDefaultAsync();
    }
}
