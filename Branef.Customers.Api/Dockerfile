#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Branef.Customers.Api/Branef.Customers.Api.csproj", "Branef.Customers.Api/"]
COPY ["Branef.Customers.Business/Branef.Customers.Business.csproj", "Branef.Customers.Business/"]
COPY ["Branef.Customers.Data/Branef.Customers.Data.csproj", "Branef.Customers.Data/"]
COPY ["Branef.Customers.Entities/Branef.Customers.Entities.csproj", "Branef.Customers.Entities/"]
COPY ["Branef.Customers.Dtos/Branef.Customers.Dtos.csproj", "Branef.Customers.Dtos/"]
COPY ["Branef.Framework/Branef.Framework.csproj", "Branef.Framework/"]
RUN dotnet restore "Branef.Customers.Api/Branef.Customers.Api.csproj"
COPY . .
WORKDIR "/src/Branef.Customers.Api"
RUN dotnet build "Branef.Customers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Branef.Customers.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN apt-get update 
RUN apt-get --yes install curl
HEALTHCHECK --interval=10s --timeout=20s --retries=10 CMD curl "http://localhost/healthz" --fail --show-error --silent || exit 1

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Branef.Customers.Api.dll"]