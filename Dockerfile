FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/KpopHall.Api/KpopHall.Api.csproj", "src/KpopHall.Api/"]
COPY ["src/KpopHall.Application/KpopHall.Application.csproj", "src/KpopHall.Application/"]
COPY ["src/KpopHall.Domain/KpopHall.Domain.csproj", "src/KpopHall.Domain/"]
COPY ["src/KpopHall.Infrastructure/KpopHall.Infrastructure.csproj", "src/KpopHall.Infrastructure/"]
RUN dotnet restore "src/KpopHall.Api/KpopHall.Api.csproj"
COPY . .
WORKDIR "/src/src/KpopHall.Api"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KpopHall.Api.dll"]