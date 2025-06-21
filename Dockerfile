# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# restore
COPY FiapCloudGames.API/FiapCloudGames.API.csproj FiapCloudGames.API/
RUN dotnet restore FiapCloudGames.API/FiapCloudGames.API.csproj

#build
COPY FiapCloudGames.API/ FiapCloudGames.API/
WORKDIR /src/FiapCloudGames.API
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

#Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "FiapCloudGames.API.dll" ]