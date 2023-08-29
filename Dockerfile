#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Core/QuizCraft.Models/QuizCraft.Models.csproj", "."]
RUN dotnet restore "QuizCraft.Models.csproj"
COPY ["src/Core/QuizCraft.Application/QuizCraft.Application.csproj", "."]
RUN dotnet restore "QuizCraft.Application.csproj"

COPY ["src/Infrastructure/QuizCraft.Infrastructure/QuizCraft.Infrastructure.csproj", "."]
RUN dotnet restore "QuizCraft.Infrastructure.csproj"
COPY ["src/Infrastructure/QuizCraft.Persistence/QuizCraft.Persistence.csproj", "."]
RUN dotnet restore "QuizCraft.Persistence.csproj"

COPY ["src/API/QuizCraft.Api/QuizCraft.Api.csproj", "."]
RUN dotnet restore "QuizCraft.Api.csproj"

COPY ["src/.", "."]
WORKDIR "/src/Core/QuizCraft.Models"
RUN dotnet build "QuizCraft.Models.csproj" -c Release -o /app/build
WORKDIR "/src/Core/QuizCraft.Application"
RUN dotnet build "QuizCraft.Application.csproj" -c Release -o /app/build

WORKDIR "/src/Infrastructure/QuizCraft.Infrastructure"
RUN dotnet build "QuizCraft.Infrastructure.csproj" -c Release -o /app/build
WORKDIR "/src/Infrastructure/QuizCraft.Persistence"
RUN dotnet build "QuizCraft.Persistence.csproj" -c Release -o /app/build

WORKDIR "/src/API/QuizCraft.Api"
RUN dotnet build "QuizCraft.Api.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/API/QuizCraft.Api"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizCraft.Api.dll"]