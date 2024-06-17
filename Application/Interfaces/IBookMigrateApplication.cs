
using Application.Adapters.Internals;

namespace Application.Interfaces;

public interface IBookMigrateApplication
{
    public Task<ResponseInternalAdapter> GetVariableAsync();
}
