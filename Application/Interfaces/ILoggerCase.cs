namespace Application.Interfaces;

public interface ILoggerCase
{
    public Task<string> OpenTrace(string operationType, string jsonRequest);
    public Task CloseTrace(string loggerId, string jsonResponse);
    public Task CloseErrorTrace(string loggerId, string errorLog);
}
