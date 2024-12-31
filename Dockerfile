
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["DashBoredAPI.csproj", "./"]
RUN dotnet restore "DashBoredAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DashBoredAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "DashBoredAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DashBoredAPI.dll"]
