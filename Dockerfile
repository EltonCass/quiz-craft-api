#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/QuizCraft.Api.Models/QuizCraft.Api.Models.csproj", "."]
RUN dotnet restore "QuizCraft.Api.Models.csproj"
COPY ["src/QuizCraft.Api/QuizCraft.Api.csproj", "."]
RUN dotnet restore "QuizCraft.Api.csproj"
COPY ["src/.", "."]
WORKDIR "/src/QuizCraft.Api.Models"
RUN dotnet build "QuizCraft.Api.Models.csproj" -c Release -o /app/build
WORKDIR "/src/QuizCraft.Api"
RUN dotnet build "QuizCraft.Api.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/QuizCraft.Api"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizCraft.Api.dll"]