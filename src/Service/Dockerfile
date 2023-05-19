FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["src/Service/ParkingSpace.csproj", "src/Service/"]
COPY ["src/Common/ParkingSpace.Common.csproj", "src/Common/"]
RUN dotnet restore "src/Service/ParkingSpace.csproj"
COPY . .
WORKDIR "/src/src/Service"
RUN dotnet build "ParkingSpace.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParkingSpace.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParkingSpace.dll"]
