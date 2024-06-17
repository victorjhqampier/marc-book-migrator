using CoredbInfrastructure.Collections.Tables;
using CoredbInfrastructure.Queries;
using Domain.Commons;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/********************************************************************************************************          
* Copyright © 2024 Victor Jhampier Caxi - All rights reserved.   
* 
* Info                  : DB Admin Service handler.
*
* By                    : Victor Jhampier Caxi Maquera
* Email/Mobile/Phone    : victorjhampier@gmail.com | 968991*14
*
* Creation date         : 20/04/2024
* 
**********************************************************************************************************/

namespace CoredbInfrastructure;

public static class CoredbSetting
{
    public static IServiceCollection AddInfrastructureCoreServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {        
        // Add Conection to SQL SERVER
        services.AddDbContext<EntityFrameworkContext>(options =>
        {
            if (isDevelopment) options.UseSqlServer(ConnectionCommon.CreateSqlConnection(configuration.GetConnectionString("DevelopServerConnection")));
            else options.UseSqlServer(ConnectionCommon.CreateSqlConnection($"Server={Environment.GetEnvironmentVariable("DB_SERVER")}; Database={Environment.GetEnvironmentVariable("DB_NAME")}; User={Environment.GetEnvironmentVariable("DB_USER")}; Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}; Encrypt=false; TrustServerCertificate=True; MultiSubnetFailover=False"));
        });

        // Add Dependency Injection
        services.AddTransient<ICompanyInfrastructure, CompanyQueryCoredb>();
        services.AddTransient<ICustommerInfrastructure, CustommerQueryCoredb>();

        return services;
    }
}
