namespace Application.Adapters.Internals;

public class FieldErrorInternalAdapter
{
    public string Code { set; get; }
    public string Message { set; get; }
    public string Field { set; get; } = "";
}
