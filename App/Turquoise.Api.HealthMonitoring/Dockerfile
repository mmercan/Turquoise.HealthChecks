FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS develop
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src/Turquoise.Api.HealthMonitoring
EXPOSE 80

FROM develop AS build
WORKDIR /src
COPY Turquoise.Api.HealthMonitoring/Turquoise.Api.HealthMonitoring.csproj Turquoise.Api.HealthMonitoring/
RUN dotnet restore Turquoise.Api.HealthMonitoring/Turquoise.Api.HealthMonitoring.csproj
COPY . .
WORKDIR /src/Turquoise.Api.HealthMonitoring
RUN dotnet build Turquoise.Api.HealthMonitoring.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Turquoise.Api.HealthMonitoring.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Turquoise.Api.HealthMonitoring.dll"]

