using Amazon.Runtime.Internal;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoInfrastructure.Collections;

namespace MongoInfrastructure.Commands;

public class LoggerCommandMongo : ILoggerInfrastructure
{
    private readonly IMongoCollection<LoggerCollection> db;

    public LoggerCommandMongo(IOptions<MongodbContext> _dbMongo)
    {
        var mongoClient = new MongoClient(_dbMongo.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(_dbMongo.Value.DatabaseName);
        db = mongoDatabase.GetCollection<LoggerCollection>("LoggerCollection");
    }

    async public Task<string> SaveLog(LoggerEntity input)
    {
        var newObject = new LoggerCollection()
        {
            IdPadre = input.parentId,
            cTipoOperacion = input.operationType,
            dFechaRequest = input.requestDate,
            cJsonRequest = input.jsonRequest,
            dFechaResponse = input.responseDate,
            cJsonResponse = input.jsonResponse,
            lResponse = input.responseStatus
        };
        await db.InsertOneAsync(newObject);

        return newObject.Id;
    }

    async public Task UpdateLog(string loggerId, LoggerEntity input)
    {        
        var data = db.AsQueryable().Where(x => x.Id == loggerId).FirstOrDefault();
        if (data == null) return;

        data.IdPadre = input.parentId;                
        data.dFechaResponse = input.responseDate;
        data.cJsonResponse = input.jsonResponse;
        data.lResponse = input.responseStatus;

        await db.ReplaceOneAsync(x => x.Id == loggerId, data);
        return;
    }

    public LoggerEntity? GetLog(string loggerId)
    {
        return db.AsQueryable().Where(x => x.Id == loggerId)
               .Select(x => new LoggerEntity
               {
                   loggerId = x.Id,
                    parentId = x.IdPadre,
                    operationType = x.cTipoOperacion,
                    requestDate = x.dFechaRequest,
                    jsonRequest = x.cJsonRequest,
                    responseDate = x.dFechaResponse,
                    jsonResponse = x.cJsonResponse,
                    responseStatus = x.lResponse

               }).FirstOrDefault();
    }
}
