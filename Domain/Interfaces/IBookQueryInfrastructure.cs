using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookQueryInfrastructure
{
    public int CountTotalBook();
    public List<int> GetPagnateBook(int offset = 0, int limit = 100);
    public Task<MarcTitleEntity> GetTitleAsync(int idTtile);
    public Task<List<MarcCopyEntity>> GetCopyAsync(int idTtile);
    public Task<List<MarcPublishEntity>> GetPublisherAsync(int idTtile);
    public Task<MarcSerieEntity?> GetSerialAsync(int idTtile);
    public Task<List<MarcAuthorEntity>> GetAuthorAsync(int idTtile);
    public Task<MarcCassifyEntity> GetClassificationAsync(int idTitle);
}
