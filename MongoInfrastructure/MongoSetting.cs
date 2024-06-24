using Domain.Commons;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoInfrastructure.Collections;
using MongoInfrastructure.Commands;
using MongoInfrastructure.Queries;

/********************************************************************************************************          
* Copyright © 2024 Victor Jhampier Caxi - All rights reserved.   
* 
* Info                  : Log Database settings.
*
* By                    : Victor Jhampier Caxi Maquera
* Email/Mobile/Phone    : victorjhampier@gmail.com | 968991*14
*
* Creation date         : 20/04/2024
* 
**********************************************************************************************************/

namespace MongoInfrastructure;

public static class MongoSetting
{
    public static IServiceCollection AddInfrastructureLogServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        // Add Conection to MongoDB
        services.Configure<MongodbContext>(
            isDevelopment ? ConnectionCommon.CreateMongoConnection(configuration.GetSection("MongoStoreDatabase")) :
            ConnectionCommon.CreateMongoConnection(configuration.GetSection("MongoStoreDatabase"), Environment.GetEnvironmentVariable("MONGO_DB_SERVER"), Environment.GetEnvironmentVariable("MONGO_DB_NAME"), Environment.GetEnvironmentVariable("MONGO_DB_USER"), Environment.GetEnvironmentVariable("MONGO_DB_PASSWORD"))
        );

        // Add Dependency Injection
        services.AddTransient<ILoggerInfrastructure, LoggerCommandMongo>();
        services.AddTransient<IBookCommandInfrastructure, BookCommandInfrastructure>();
        services.AddTransient<IBookTrackerQueryInfrastructure, LoggerQueryMongo>();

        return services;
    }
}
