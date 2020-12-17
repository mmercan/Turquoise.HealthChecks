FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS develop
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src/Workers/Turquoise.Worker.HealthChecker
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src/
COPY ["Workers/Turquoise.Worker.HealthChecker/Turquoise.Worker.HealthChecker.csproj", "Workers/Turquoise.Worker.HealthChecker/"]
RUN dotnet restore "Workers/Turquoise.Worker.HealthChecker/Turquoise.Worker.HealthChecker.csproj"
COPY . .
WORKDIR "/src/Workers/Turquoise.Worker.HealthChecker"
RUN dotnet build "Turquoise.Worker.HealthChecker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Turquoise.Worker.HealthChecker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Turquoise.Worker.HealthChecker.dll"]
