FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SignalrHelloWorld.Server/SignalrHelloWorld.Server.csproj", "SignalrHelloWorld.Server/"]
RUN dotnet restore "SignalrHelloWorld.Server/SignalrHelloWorld.Server.csproj"
COPY . .
WORKDIR "/src/SignalrHelloWorld.Server"
RUN dotnet build "SignalrHelloWorld.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalrHelloWorld.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalrHelloWorld.Server.dll"]
