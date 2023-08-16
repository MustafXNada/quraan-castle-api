#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["quraan-castle-api/quraan-castle-api.csproj", "quraan-castle-api/"]
RUN dotnet restore "quraan-castle-api/quraan-castle-api.csproj"
COPY . .
WORKDIR "/src/quraan-castle-api"
RUN dotnet build "quraan-castle-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "quraan-castle-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "quraan-castle-api.dll"]