# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solução e restaurar dependências
COPY ../.. . 
WORKDIR /src/FluxoPedidos.Micro.Api
RUN dotnet restore FluxoPedidos.Micro.Api.csproj

# Build + Publish
RUN dotnet publish FluxoPedidos.Micro.Api.csproj -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FluxoPedidos.Micro.Api.dll"]
