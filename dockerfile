FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS=http://+:80 

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["haymatlosApi.csproj", "."]
RUN dotnet dev-certs https
RUN dotnet restore "./haymatlosApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "haymatlosApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "haymatlosApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
EXPOSE 80/tcp
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "haymatlosApi.dll"]