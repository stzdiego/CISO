﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CISO.ApiGateway/CISO.ApiGateway.csproj", "CISO.ApiGateway/"]
RUN dotnet restore "CISO.ApiGateway/CISO.ApiGateway.csproj"
COPY . .
WORKDIR "/src/CISO.ApiGateway"
RUN dotnet build "./CISO.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CISO.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CISO.ApiGateway.dll"]
