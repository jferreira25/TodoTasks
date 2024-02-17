FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["Tasks.Api/Tasks.Api.csproj", "src/Tasks.Api/"]
COPY ["Tasks.Domain/Tasks.Domain.csproj", "Tasks.Domain/"]
COPY ["Tasks.Infrastructure/Tasks.Infrastructure.csproj", "Tasks.Infrastructure/"]

RUN dotnet restore "src/Tasks.Api/Tasks.Api.csproj"
COPY . .
WORKDIR "/src/Tasks.Api"
RUN dotnet build "Tasks.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasks.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks.Api.dll"]