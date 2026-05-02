FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY EduMeilleurAPI/EduMeilleurAPI.csproj EduMeilleurAPI/
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore EduMeilleurAPI/EduMeilleurAPI.csproj

COPY . .
WORKDIR /src/EduMeilleurAPI

ARG TARGETARCH
RUN dotnet publish -c Release -o /app/publish -r linux-$TARGETARCH --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EduMeilleurAPI.dll"]