FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["DashBored.Api/DashBored.Api.csproj", "DashBored.Api/"]
RUN dotnet restore "./DashBored.Api/DashBored.Api.csproj"
COPY . .
WORKDIR "/src/DashBored.Api"
RUN dotnet build "./DashBored.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "./DashBored.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "./DashBored.Api/DashBored.Api.dll"]
