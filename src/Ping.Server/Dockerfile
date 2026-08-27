FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/Ping.Server/Ping.Server.csproj", "src/Ping.Server/"]
RUN dotnet restore "src/Ping.Server/Ping.Server.csproj"

COPY . .
WORKDIR "/src/src/Ping.Server"
RUN dotnet publish "Ping.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

USER $APP_UID

ENTRYPOINT ["dotnet", "Ping.Server.dll"]