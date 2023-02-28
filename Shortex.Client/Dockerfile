#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shortex.Client/Shortex.Client.csproj", "Shortex.Client/"]
COPY ["Shortex.BusinessLogic/Shortex.BusinessLogic.csproj", "Shortex.BusinessLogic/"]
COPY ["Shortex.Common/Shortex.Common.csproj", "Shortex.Common/"]
COPY ["Shortex.DataAccess/Shortex.DataAccess.csproj", "Shortex.DataAccess/"]
RUN dotnet restore "Shortex.Client/Shortex.Client.csproj"
COPY . .
WORKDIR "/src/Shortex.Client"
RUN dotnet build "Shortex.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shortex.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shortex.Client.dll"]