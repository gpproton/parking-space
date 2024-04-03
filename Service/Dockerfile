FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM node:20-alpine AS node

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

COPY --from=node /usr/lib /usr/lib
COPY --from=node /usr/local/share /usr/local/share
COPY --from=node /usr/local/lib /usr/local/lib
COPY --from=node /usr/local/include /usr/local/include
COPY --from=node /usr/local/bin /usr/local/bin

WORKDIR /src
COPY . .
RUN dotnet restore "Service/ParkingSpace.csproj"
WORKDIR "/src/Service"
RUN dotnet build "ParkingSpace.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParkingSpace.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "parking-space.dll"]
