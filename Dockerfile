#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Project-66-bit.csproj", "."]
RUN dotnet restore "./Project-66-bit.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Project-66-bit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project-66-bit.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project-66-bit.dll"]