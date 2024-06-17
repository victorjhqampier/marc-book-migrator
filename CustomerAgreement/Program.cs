using Application;
using CustomerAgreement.Handlers;

/********************************************************************************************************          
* Copyright � 2024 Victor Jhampier Caxi - All rights reserved.   
* 
* Info                  : Application Setting.
*
* By                    : Victor Jhampier Caxi Maquera
* Email/Mobile/Phone    : victorjhampier@gmail.com  | 968991*14
*
* Creation date         : 20/04/2024
* "Console": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Information",
        "Microsoft.AspNetCore": "Information"
      }
    }
**********************************************************************************************************/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration, builder.Environment.IsDevelopment());

builder.Services.ConfigureJwtKeycloak(builder.Configuration, builder.Environment.IsDevelopment());

builder.Services.ConfigureJwtScopes(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UsePathBase("/api");

// Add JWT Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();