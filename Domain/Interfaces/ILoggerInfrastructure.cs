using Domain.Entities;

namespace Domain.Interfaces;

public interface ILoggerInfrastructure
{
    public Task<string> SaveLog(LoggerEntity input);
    public Task UpdateLog(string loggerId, LoggerEntity input);
    public LoggerEntity? GetLog(string loggerId);
}
