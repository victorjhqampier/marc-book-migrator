using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CustomerAgreement.Handlers;

public static class JwtHandler
{
    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        //https://cognito-idp.us-east-1.amazonaws.com/us-east-1_RyxEjxH9S
        string authorityUrl = isDevelopment ? configuration["Jwt:Issuer"] : Environment.GetEnvironmentVariable("JWT_ISSUER");
        string validAudience = isDevelopment ? configuration["Jwt:Audience"] : Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = authorityUrl;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authorityUrl,
                ValidAudience = validAudience,
                IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                {
                    using var httpClient = new HttpClient();
                    return Task.Run(async () =>
                    {
                        var discoveryResponse = await httpClient.GetStringAsync($"{options.Authority}/.well-known/jwks.json");
                        var jwks = new JsonWebKeySet(discoveryResponse);
                        return jwks.Keys;
                    }).Result;
                }
            };
        });
    }

    public static void ConfigureJwtScopes(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ReadScope", policy =>
            {
                policy.RequireClaim("scope", isDevelopment ? configuration["Jwt:ReadScope"].ToString() : Environment.GetEnvironmentVariable("JWT_READ_SCOPE"));
            });

            options.AddPolicy("UpdateScope", policy =>
            {
                policy.RequireClaim("scope", isDevelopment ? configuration["Jwt:UpdateScope"].ToString() : Environment.GetEnvironmentVariable("JWT_UPDATE_SCOPE"));
            });

            options.AddPolicy("WriteScope", policy =>
            {
                policy.RequireClaim("scope", isDevelopment ? configuration["Jwt:WriteScope"].ToString() : Environment.GetEnvironmentVariable("JWT_WRITE_SCOPE"));
            });
        });
    }

    public static void ConfigureJwtKeycloak(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        //string authorityUrl = "http://localhost:8080/realms/master";
        string authorityUrl = isDevelopment ? configuration["Jwt:Issuer"] : Environment.GetEnvironmentVariable("JWT_ISSUER");
        string validAudience = isDevelopment ? configuration["Jwt:Audience"] : Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = authorityUrl;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authorityUrl,
                ValidAudience = validAudience,
                IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                {
                    using var httpClient = new HttpClient();
                    return Task.Run(async () =>
                    {
                        var discoveryResponse = await httpClient.GetStringAsync($"{options.Authority}/protocol/openid-connect/certs");
                        var jwks = new JsonWebKeySet(discoveryResponse);
                        return jwks.Keys;
                    }).Result;
                }
            };
        });
    }

}
