FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5123

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Balance.BePaid.Web/Balance.BePaid.Web.csproj", "src/Balance.BePaid.Web/"]
COPY ["src/Balance.BePaid.UseCases/Balance.BePaid.UseCases.csproj", "src/Balance.BePaid.UseCases/"]
COPY ["src/Balance.BePaid.Application/Balance.BePaid.Application.csproj", "src/Balance.BePaid.Application/"]
COPY ["src/Balance.BePaid.Domain/Balance.BePaid.Domain.csproj", "src/Balance.BePaid.Domain/"]
COPY ["src/Balance.BePaid.Infrastructure/Balance.BePaid.Infrastructure.csproj", "src/Balance.BePaid.Infrastructure/"]
RUN dotnet restore "src/Balance.BePaid.Web/Balance.BePaid.Web.csproj"
COPY . .
WORKDIR "/src/src/Balance.BePaid.Web"
RUN dotnet build "Balance.BePaid.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Balance.BePaid.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Balance.BePaid.Web.dll"]
