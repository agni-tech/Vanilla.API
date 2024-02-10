#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install -y locales-all

ENV LANG C.UTF-8
ENV LC_ALL C.UTF-8

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Vanilla.API/Vanilla.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Vanilla.API/Vanilla.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vanilla.API/Vanilla.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vanilla.API.dll"]
