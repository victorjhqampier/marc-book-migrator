using ExtractDatabaseInfrastructure.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Commons;
using Domain.Interfaces;
using ExtractDatabaseInfrastructure.Queries;

namespace ExtractDatabaseInfrastructure;

public static class ExtractDatabaseSetting
{

    public static IServiceCollection AddExtractDatabaseServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        // Add Conection to MySQL        
        services.AddDbContext<EntityFrameworkContext>(options =>
        {
            if (isDevelopment) options.UseMySql(configuration.GetConnectionString("ExtractServerConnection"), ServerVersion.Parse("11.4.2-mariadb"));
            else options.UseMySql(ConnectionCommon.CreateSqlConnection($"server={Environment.GetEnvironmentVariable("DB_MYSQL_SERVER")}; database={Environment.GetEnvironmentVariable("DB_MYSQL_NAME")}; user={Environment.GetEnvironmentVariable("DB_MYSQL_USER")}; password={Environment.GetEnvironmentVariable("DB_PASSWORD")}"), ServerVersion.Parse("11.4.2-mariadb"));
        });

        // Add Dependency Injection
        //services.AddTransient<ILoggerInfrastructure, LoggerCommandMongo>();
        services.AddTransient<IBookQueryInfrastructure, BookQueryExtracDatabase>();        

        return services;
    }
}
