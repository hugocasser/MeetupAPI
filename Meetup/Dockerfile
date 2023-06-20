FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /source

COPY . .
RUN dotnet restore "./Meetup.csproj" --disable-parallel
RUN dotnet publish "./Meetup.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app ./

ENTRYPOINT ["dotnet", "Meetup.dll"]