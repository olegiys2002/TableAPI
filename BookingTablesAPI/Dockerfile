#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookingTablesAPI/BookingTablesAPI.csproj", "BookingTablesAPI/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["SharedAssembly/Shared.csproj", "SharedAssembly/"]
COPY ["Tests/Tests.csproj","Tests/"]
RUN dotnet restore "BookingTablesAPI/BookingTablesAPI.csproj"
COPY . .
WORKDIR "/src/BookingTablesAPI"
RUN dotnet build "BookingTablesAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingTablesAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingTablesAPI.dll"]