using Domain.Entities;

namespace Application.Adapters.Book;

public class BookCopyAdapter
{
    public List<MarcCopyEntity> Processed { set; get; }
    public List<MarcCopyEntity> Excessed { set; get; }
}
