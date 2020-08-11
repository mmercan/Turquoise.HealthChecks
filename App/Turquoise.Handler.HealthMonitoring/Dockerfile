FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS develop
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src/Turquoise.Handler.HealthMonitoring
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Turquoise.Handler.HealthMonitoring/Turquoise.Handler.HealthMonitoring.csproj", "Turquoise.Handler.HealthMonitoring/"]
RUN dotnet restore "Turquoise.Handler.HealthMonitoring/Turquoise.Handler.HealthMonitoring.csproj"
COPY . .
WORKDIR "/src/Turquoise.Handler.HealthMonitoring"
RUN dotnet build "Turquoise.Handler.HealthMonitoring.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Turquoise.Handler.HealthMonitoring.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Turquoise.Handler.HealthMonitoring.dll"]
