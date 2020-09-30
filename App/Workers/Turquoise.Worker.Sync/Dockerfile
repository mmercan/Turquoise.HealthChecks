FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS develop
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src/Workers/Turquoise.Worker.Sync
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src/
COPY ["Workers/Turquoise.Worker.Sync/Turquoise.Worker.Sync.csproj", "Workers/Turquoise.Worker.Sync/"]
RUN dotnet restore "Workers/Turquoise.Worker.Sync/Turquoise.Worker.Sync.csproj"
COPY . .
WORKDIR "/src/Workers/Turquoise.Worker.Sync"
RUN dotnet build "Turquoise.Worker.Sync.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Turquoise.Worker.Sync.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Turquoise.Worker.Sync.dll"]
