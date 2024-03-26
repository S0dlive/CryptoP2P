FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CryptoP2P.Network/CryptoP2P.Network.csproj", "src/CryptoP2P.Network/"]
RUN dotnet restore "src/CryptoP2P.Network/CryptoP2P.Network.csproj"
COPY . .
WORKDIR "/src/src/CryptoP2P.Network"
RUN dotnet build "CryptoP2P.Network.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CryptoP2P.Network.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoP2P.Network.dll"]
