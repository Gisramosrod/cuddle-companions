#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CuddleCompanions.Api/CuddleCompanions.Api.csproj", "CuddleCompanions.Api/"]
COPY ["CuddleCompanions.Infrastructure/CuddleCompanions.Infrastructure.csproj", "CuddleCompanions.Infrastructure/"]
COPY ["CuddleCompanions.Application/CuddleCompanions.Application.csproj", "CuddleCompanions.Application/"]
COPY ["CuddleCompanions.Domain/CuddleCompanions.Domain.csproj", "CuddleCompanions.Domain/"]
RUN dotnet restore "./CuddleCompanions.Api/CuddleCompanions.Api.csproj"
COPY . .
WORKDIR "/src/CuddleCompanions.Api"
RUN dotnet build "./CuddleCompanions.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CuddleCompanions.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CuddleCompanions.Api.dll"]