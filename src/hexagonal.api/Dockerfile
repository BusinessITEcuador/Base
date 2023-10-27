#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/hexagonal.api/hexagonal.infrastructure.api.csproj", "src/hexagonal.api/"]
COPY ["src/hexagonal.application/hexagonal.application.csproj", "src/hexagonal.application/"]
COPY ["src/hexagonal.data/hexagonal.infrastructure.data.csproj", "src/hexagonal.data/"]
COPY ["src/hexagonal.domain/hexagonal.domain.csproj", "src/hexagonal.domain/"]
RUN dotnet restore "src/hexagonal.api/hexagonal.infrastructure.api.csproj"
COPY . .
WORKDIR "/src/src/hexagonal.api"
RUN dotnet build "hexagonal.infrastructure.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "hexagonal.infrastructure.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hexagonal.infrastructure.api.dll"]