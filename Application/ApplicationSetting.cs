using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoInfrastructure;
using Application.Interfaces;
using Application.Usecases.LoggerCase;
using ExtractDatabaseInfrastructure;
using Application.Usecases.BookCase;

/********************************************************************************************************          
* Copyright © 2024 Victor Jhampier Caxi - All rights reserved.   
* 
* Info                  : Application Layer.
*
* By                    : Victor Jhampier Caxi Maquera
* Email/Mobile/Phone    : victorjhampier@gmail.com | 968991*14
*
* Creation date         : 20/04/2024
* Docs
* https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/ignore-properties
**********************************************************************************************************/

namespace Application;

public static class ApplicationSetting
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        //SQL Server Database
        //services.AddInfrastructureCoreServices(configuration, isDevelopment);

        //MySQL Database
        services.AddExtractDatabaseServices(configuration, isDevelopment);

        //MongoDB Database
        services.AddInfrastructureLogServices(configuration, isDevelopment);

        //Dependency inyection        
        services.AddTransient<ILoggerCase, LoggerUsecase>();
        services.AddTransient<IBookMigrateApplication, BookMigrateUsecase>();
        return services;
    }
}
