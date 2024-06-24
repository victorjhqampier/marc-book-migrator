
using Application.Adapters.Internals;

namespace Application.Interfaces;

public interface IBookMigrateApplication
{
    public Task<ResponseInternalAdapter> ProcessAsync();
    public Task<ResponseInternalAdapter> ReprocessAsync();
}
