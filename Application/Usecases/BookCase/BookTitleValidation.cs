using Application.Adapters.Internals;
using Domain.Entities;

namespace Application.Usecases.BookCase;

public static class BookTitleValidation
{
    public static List<FieldErrorInternalAdapter> ValidateTitle(MarcTitleEntity request)
    {
        var validador = new MarcTitleEntityValidator();
        var valResult = validador.Validate(request);

        //if (!valResult.IsValid)
        //{
        return valResult.Errors.Select(x => new FieldErrorInternalAdapter()
        {
            Code = x.ErrorCode,
            Message = x.ErrorMessage,
            Field = x.PropertyName
        })
        .ToList();
        //}

        //return null;
    }
}
