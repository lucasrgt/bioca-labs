FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BiocaLabs.API/BiocaLabs.API.csproj", "BiocaLabs.API/"]
RUN dotnet restore "BiocaLabs.API/BiocaLabs.API.csproj"
COPY . .
WORKDIR "/src/BiocaLabs.API"
RUN dotnet build "BiocaLabs.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BiocaLabs.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BiocaLabs.API.dll"]
