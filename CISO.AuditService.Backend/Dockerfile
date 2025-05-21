FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CISO.AuditService.Backend/CISO.AuditService.Backend.csproj", "CISO.AuditService.Backend/"]
COPY ["../STZ/STZ.Backend/STZ.Backend.csproj", "../STZ/STZ.Backend/"]
COPY ["../STZ/STZ.Shared/STZ.Shared.csproj", "../STZ/STZ.Shared/"]
COPY ["CISO.AuditService.Access/CISO.AuditService.Access.csproj", "CISO.AuditService.Access/"]
COPY ["CISO.AuditService.Shared/CISO.AuditService.Shared.csproj", "CISO.AuditService.Shared/"]
COPY ["CISO.EvidenceService.Backend/CISO.EvidenceService.Backend.csproj", "CISO.EvidenceService.Backend/"]
COPY ["CISO.EvidenceService.Access/CISO.EvidenceService.Access.csproj", "CISO.EvidenceService.Access/"]
COPY ["CISO.EvidenceService.Shared/CISO.EvidenceService.Shared.csproj", "CISO.EvidenceService.Shared/"]
RUN dotnet restore "CISO.AuditService.Backend/CISO.AuditService.Backend.csproj"
COPY . .
WORKDIR "/src/CISO.AuditService.Backend"
RUN dotnet build "./CISO.AuditService.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CISO.AuditService.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CISO.AuditService.Backend.dll"]
