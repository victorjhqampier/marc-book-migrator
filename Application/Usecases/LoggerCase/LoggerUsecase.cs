using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Usecases.LoggerCase;

public class LoggerUsecase : ILoggerCase
{
    private readonly ILoggerInfrastructure loggerInfra;
    public LoggerUsecase(ILoggerInfrastructure _logger) => loggerInfra = _logger;

    async public Task CloseErrorTrace(string loggerId, string errorLog)
    {
        try
        {
            var log = loggerInfra.GetLog(loggerId);
            if (log == null) return;

            log.responseDate = DateTime.UtcNow;
            log.jsonResponse = errorLog;

            await loggerInfra.UpdateLog(loggerId, log);
            return;
        }
        catch
        {
            return;
        }
    }

    async public Task CloseTrace(string loggerId, string jsonResponse)
    {
        try
        {
            var log = loggerInfra.GetLog(loggerId);
            if (log == null) return;

            log.responseDate = DateTime.UtcNow;
            log.jsonResponse = jsonResponse;
            log.responseStatus = true;

            await loggerInfra.UpdateLog(loggerId, log);
            return;
        }
        catch
        {
            return;
        }        
    }

    async public Task<string> OpenTrace(string operationType, string jsonRequest)
    {
        return await loggerInfra.SaveLog(new LoggerEntity() {
            operationType = operationType,
            jsonRequest = jsonRequest
        });
    }
}
