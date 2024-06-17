using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookQueryInfrastructure
{
    public int CountTotalBook();
    public Task<MarcTitleEntity> GetTitleAsync(int idTtile);
    public Task<List<MarcCopyEntity>> GetCopyAsync(int idTtile);
    public Task<List<MarcPublishEntity>> GetPublisherAsync(int idTtile);
    public Task<MarcSerieEntity?> GetSerialAsync(int idTtile);
    public Task<List<MarcAuthorEntity>> GetAuthorAsync(int idTtile);
    public Task<MarcCassifyEntity> GetClassificationAsync(int idTitle);
}
