FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS develop
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src/Workers/Turquoise.Worker.Comms
EXPOSE 80
EXPOSE 81

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src/
COPY ["Workers/Turquoise.Worker.Comms/Turquoise.Worker.Comms.csproj", "Workers/Turquoise.Worker.Comms/"]
RUN dotnet restore "Workers/Turquoise.Worker.Comms/Turquoise.Worker.Comms.csproj"
COPY . .
WORKDIR "/src/Workers/Turquoise.Worker.Comms"
RUN dotnet build "Turquoise.Worker.Comms.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Turquoise.Worker.Comms.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Turquoise.Worker.Comms.dll"]
