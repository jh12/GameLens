FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG TARGETARCH
ARG RELEASE_VERSION
ARG BUILD_CONFIGURATION=Release
WORKDIR /sln

COPY ./*.sln ./
COPY src/GameLens src/GameLens
COPY src/GameLens.Client src/GameLens.Client
COPY src/GameLens.Shared src/GameLens.Shared

RUN ls -l .
RUN ls -l src/

RUN dotnet restore -a $TARGETARCH

COPY ./src ./src
RUN dotnet build "./src/GameLens/GameLens.csproj" -c $BUILD_CONFIGURATION -a $TARGETARCH -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./src/GameLens/GameLens.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false -p:VersionPrefix=$RELEASE_VERSION


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameLens.dll"]