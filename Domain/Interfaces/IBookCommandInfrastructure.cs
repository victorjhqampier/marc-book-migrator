using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookCommandInfrastructure
{
    public Task SaveCompleteBook(MarcTitleEntity objTitle, List<MarcAuthorEntity> arrAutor, MarcCassifyEntity objClasify, List<MarcPublishEntity> arrPublisher, List<MarcCopyEntity> arrCopy, MarcSerieEntity? objSerie = null);
    public Task SaveErrorBook(int idTitle, string title, string MessageError);
    public Task UpdateErrorStatusBook(int idTitle, bool isSuccessed = true, string? message = null);
    public Task SaveDuplicateCopies(int idTitle, string title, List<MarcCopyEntity> arrCopy);
}
