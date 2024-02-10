ARG version='1.0.0'
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG version
WORKDIR /src

# Copia os arquivos para dentro do container
COPY . .

# Executa o build na solu��o inteira
RUN dotnet build "./Vanilla.Domain/Vanilla.Domain.csproj" -c Release -o /app
RUN dotnet build "./Vanilla.Infra.Data/Vanilla.Infra.Data.csproj" -c Release -o /app
RUN dotnet build "./Vanilla.Infra.IoC/Vanilla.Infra.IoC.csproj" -c Release -o /app
RUN dotnet build "./Vanilla.Shared/Vanilla.Shared.csproj" -c Release -o /app
RUN dotnet build "./Vanilla.API/Vanilla.API.csproj" -c Release -o /app

# Executa o publish da API
WORKDIR /src
RUN dotnet publish "./Vanilla.Domain/Vanilla.Domain.csproj" -c Release -o /app
RUN dotnet publish "./Vanilla.Infra.Data/Vanilla.Infra.Data.csproj" -c Release -o /app
RUN dotnet publish "./Vanilla.Infra.IoC/Vanilla.Infra.IoC.csproj" -c Release -o /app
RUN dotnet publish "./Vanilla.Shared/Vanilla.Shared.csproj" -c Release -o /app
RUN dotnet publish "./Vanilla.API/Vanilla.API.csproj" -c Release -o /app
RUN mkdir /app/logs/
RUN mkdir /app/wwwroot/

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
EXPOSE 443
WORKDIR /app
ENV ASPNETCORE__URLS=http://+:80;http://+:443;
COPY --from=build /app . 
ENTRYPOINT ["dotnet", "Vanilla.API.dll" ]
