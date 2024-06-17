using Domain.Enums;
using Domain.Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Adapters.Requests;

public class CardIdentifierAdapter
{
    public string? CustommerCardType { get; set; }
    public string? CustommerCardNumber { get; set; }
}

public class CardIdentifierAdapterValidator : AbstractValidator<CardIdentifierAdapter>
{
    public CardIdentifierAdapterValidator()
    {
        RuleFor(x => x.CustommerCardType)
                .NotEmpty().WithMessage(MessageException.GetErrorByCode(10002, "CustommerCardType")).WithErrorCode("10002")
                .Must((m, thisValue) => Enum.IsDefined(typeof(CardTypeEnum), Int32.Parse(thisValue??"0"))).WithMessage(MessageException.GetErrorByCode(10006, "CustommerCardType")).WithErrorCode("10006");

        RuleFor(x => x.CustommerCardNumber)
                .NotEmpty().WithMessage(MessageException.GetErrorByCode(10002, "CustommerCardNumber")).WithErrorCode("10002")
                .Length(8).WithMessage(MessageException.GetErrorByCode(10004, "8")).WithErrorCode("10004")
                .Matches(new Regex("^\\d+$")).WithMessage(MessageException.GetErrorByCode(10014)).WithErrorCode("10014");

    }
}