FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY EduMeilleurAPI/EduMeilleurAPI.csproj EduMeilleurAPI/
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore EduMeilleurAPI/EduMeilleurAPI.csproj

COPY . .
WORKDIR /src/EduMeilleurAPI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EduMeilleurAPI.dll"]