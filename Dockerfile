# Build image from SDK .NET 7
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy files and restore Nuget packages
COPY DataSyncHub.sln .
COPY src/Bootstrapper/DataSyncHub.Bootstrapper/DataSyncHub.Bootstrapper.csproj ./src/Bootstrapper/DataSyncHub.Bootstrapper/
COPY src/Modules/Users/DataSyncHub.Modules.Users.Api/DataSyncHub.Modules.Users.Api.csproj ./src/Modules/Users/DataSyncHub.Modules.Users.Api/
COPY src/Modules/Users/DataSyncHub.Modules.Users.Core/DataSyncHub.Modules.Users.Core.csproj ./src/Modules/Users/DataSyncHub.Modules.Users.Core/
COPY src/Shared/DataSyncHub.Shared.Abstractions/DataSyncHub.Shared.Abstractions.csproj ./src/Shared/DataSyncHub.Shared.Abstractions/
COPY src/Shared/DataSyncHub.Shared.Infrastructure/DataSyncHub.Shared.Infrastructure.csproj ./src/Shared/DataSyncHub.Shared.Infrastructure/

RUN dotnet restore

# Copy source code
COPY . .

# Build app
RUN dotnet publish -c Release -o out

# Build image runtime ASP.NET 7
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Run app
ENTRYPOINT ["dotnet", "DataSyncHub.Bootstrapper.dll"]
