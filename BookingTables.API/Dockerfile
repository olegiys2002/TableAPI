#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookingTables.API/BookingTables.API.csproj", "BookingTables.API/"]
COPY ["BookingTables.Core/BookingTables.Core.csproj", "BookingTables.Core/"]
COPY ["BookingTables.Infrastructure/BookingTables.Infrastructure.csproj", "BookingTables.Infrastructure/"]
COPY ["BookingTables.Models/BookingTables.Models.csproj", "BookingTables.Models/"]
COPY ["BookingTables.SharedAssembly/BookingTables.Shared.csproj", "BookingTables.SharedAssembly/"]
RUN dotnet restore "BookingTables.API/BookingTables.API.csproj"
COPY . .
WORKDIR "/src/BookingTables.API"
RUN dotnet build "BookingTables.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingTables.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingTables.API.dll"]