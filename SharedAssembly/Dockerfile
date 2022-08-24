FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5053

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["SharedAssembly/Shared.csproj", "SharedAssembly/"]
RUN dotnet restore "SharedAssembly/Shared.csproj"
COPY . .
WORKDIR "/src/SharedAssembly"
RUN dotnet build "Shared.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shared.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shared.dll"]
