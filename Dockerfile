#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# RUN sed -i 's/TLSv1.2/TLSv1/g' /etc/ssl/openssl.cnf
# RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
WORKDIR /app
ENV TZ="America/Lima"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["CoredbInfrastructure/CoredbInfrastructure.csproj", "CoredbInfrastructure/"]
COPY ["MongoInfrastructure/MongoInfrastructure.csproj", "MongoInfrastructure/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["CustomerAgreement/CustomerAgreement.csproj", "CustomerAgreement/"]

RUN dotnet restore "CustomerAgreement/CustomerAgreement.csproj"

COPY . .
WORKDIR "/src/CustomerAgreement"
RUN dotnet build "CustomerAgreement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerAgreement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerAgreement.dll"]

#------- To Deploy ----------------------------------------------
#> docker build -t img-customer-agreement .
#> docker run --restart=on-failure -p 5701:5001 -e ASPNETCORE_URLS=http://+:5001 --name api-customer-agreement -d img-customer-agreement 
#> --restart=always
#> --rm
