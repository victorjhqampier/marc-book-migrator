using Application.Adapters.Book;
using Domain.Entities;

namespace Application.Usecases.BookCase;

public static class BooksCopyProccess
{
    public static BookCopyAdapter ProcessCopy(List<MarcCopyEntity> bookCopies)
    {
        var processedList = new List<MarcCopyEntity>();
        var excessCopiesList = new List<MarcCopyEntity>();

        var groupedCopies = bookCopies.GroupBy(b => b.CNotation);

        foreach (var group in groupedCopies)
        {
            var copies = group.ToList();
            processedList.AddRange(copies.Take(3));
            excessCopiesList.AddRange(copies.Skip(3));
        }

        return new BookCopyAdapter()
        {
            Processed = processedList,
            Excessed = excessCopiesList
        };
    }
}
