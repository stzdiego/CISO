FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CISO.CertificationService.Backend/CISO.CertificationService.Backend.csproj", "CISO.CertificationService.Backend/"]
COPY ["CISO.CertificationService.Access/CISO.CertificationService.Access.csproj", "CISO.CertificationService.Access/"]
COPY ["../STZ/STZ.Shared/STZ.Shared.csproj", "../STZ/STZ.Shared/"]
COPY ["CISO.CertificationService.Shared/CISO.CertificationService.Shared.csproj", "CISO.CertificationService.Shared/"]
COPY ["CISO.AuditService.Shared/CISO.AuditService.Shared.csproj", "CISO.AuditService.Shared/"]
COPY ["../STZ/STZ.Backend/STZ.Backend.csproj", "../STZ/STZ.Backend/"]
RUN dotnet restore "CISO.CertificationService.Backend/CISO.CertificationService.Backend.csproj"
COPY . .
WORKDIR "/src/CISO.CertificationService.Backend"
RUN dotnet build "./CISO.CertificationService.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CISO.CertificationService.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CISO.CertificationService.Backend.dll"]
