FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /project
COPY ["src/WGU-ESS.API/WGU-ESS.API.csproj", "src/WGU-ESS.API/"]
COPY . .
WORKDIR "/project/src/WGU-ESS.API"
RUN dotnet build "WGU-ESS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WGU-ESS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
ENTRYPOINT ["dotnet", "WGU-ESS.API.dll"]
