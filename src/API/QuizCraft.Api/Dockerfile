#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/QuizCraft.Api/QuizCraft.Api.csproj", "src/QuizCraft.Api/"]
COPY ["src/QuizCraft.Api.Models/QuizCraft.Api.Models.csproj", "src/QuizCraft.Api.Models/"]
RUN dotnet restore "src/QuizCraft.Api/QuizCraft.Api.csproj"
COPY . .
WORKDIR "/src/src/QuizCraft.Api"
RUN dotnet build "QuizCraft.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuizCraft.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizCraft.Api.dll"]