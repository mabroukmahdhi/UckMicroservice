FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Talks/Talks.csproj", "Talks/"]
RUN dotnet restore "Talks/Talks.csproj"
COPY . .
WORKDIR "/src/Talks"
RUN dotnet build "Talks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Talks.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Talks.dll"]