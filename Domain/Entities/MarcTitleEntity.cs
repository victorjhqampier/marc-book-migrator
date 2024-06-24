using Domain.Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Domain.Entities;

public class MarcTitleEntity
{
    public int IdTitle { get; set; }
    public string? CDewey { get; set; }
    public string? CTitle { get; set; }
    public string? CSubtitle { get; set; }
    public string? CEdition { get; set; }
    public string? NReleased { get; set; }
    public string? CContent { get; set; }
    public string? CIsbn { get; set; }
    public string? CPhysicaldesc { get; set; }
    public string? CNotes { get; set; }
    public string? CTopics { get; set; }
    public string CType { get; set; }
    public string? CImage { get; set; }
}

public class MarcTitleEntityValidator : AbstractValidator<MarcTitleEntity>
{
    public MarcTitleEntityValidator()
    {
        RuleFor(x => x.CDewey)
            .NotEmpty().WithMessage(MessageException.GetErrorByCode(10002, "CDewey")).WithErrorCode("10002")
            .Matches(new Regex("^\\d+(\\.\\d+)?$")).WithMessage(MessageException.GetErrorByCode(10014, "CDewey")).WithErrorCode("10014");

        RuleFor(x => x.CTitle)
            .NotEmpty().WithMessage(MessageException.GetErrorByCode(10002, "CTitle")).WithErrorCode("10002");
    }
}