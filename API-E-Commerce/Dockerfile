FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
ARG BUILD_CONFIGURATION=Release
COPY ["API-E-Commerce/API-E-Commerce.csproj", "API-E-Commerce/"]
RUN dotnet restore "API-E-Commerce/API-E-Commerce.csproj"
COPY . .
WORKDIR /src/API-E-Commerce
RUN dotnet build "API-E-Commerce.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "API-E-Commerce.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API-E-Commerce.dll"]
