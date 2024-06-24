using Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoInfrastructure.Collections;
using System.Collections.Generic;

namespace MongoInfrastructure.Queries;

public class LoggerQueryMongo : IBookTrackerQueryInfrastructure
{
    private readonly IMongoCollection<BookMarcCollection> _dbSuccess;
    private readonly IMongoCollection<BookErrorCollection> _dbError;
    private readonly IMongoCollection<BookEvaluateCollection> _dbWarning;

    public LoggerQueryMongo(IOptions<MongodbContext> _dbMongo)
    {
        var mongoClient = new MongoClient(_dbMongo.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(_dbMongo.Value.DatabaseName);
        _dbSuccess = mongoDatabase.GetCollection<BookMarcCollection>("BookSuccess");
        _dbError = mongoDatabase.GetCollection<BookErrorCollection>("BookError");
        _dbWarning = mongoDatabase.GetCollection<BookEvaluateCollection>("BookWarning");
    }

    public bool ProccessedYet()
    {
        return _dbSuccess.AsQueryable().Select(x => x.IdTitle).FirstOrDefault() != 0;
    }

    public int CountTotalSuccesss()
    {
        return _dbSuccess.AsQueryable().ToList().Count();
    }

    public int CountTotalError()
    {
        return _dbError.AsQueryable().Where(x => x.IsReproccessed == false).ToList().Count();
        
        //var filter = Builders<BookErrorCollection>.Filter.Eq(x => x.IsReproccessed, false);
        //return (int)_dbError.CountDocuments(filter);
    }

    public int CountTotalWarning()
    {
        return _dbWarning.AsQueryable().ToList().Count();
    }

    public List<int> GetIdsError(int offset, int limit = 50)
    {
        return _dbError
            .AsQueryable()
            .Where(x => x.IsReproccessed == false)
            .Select(x => x.IdTitle)
            .Skip(offset)
            .Take(limit)
            .ToList();
    }
}
