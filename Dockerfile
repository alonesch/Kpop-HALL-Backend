FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["KpopHall.Api/KpopHall.Api.csproj", "KpopHall.Api/"]
COPY ["KpopHall.Application/KpopHall.Application.csproj", "KpopHall.Application/"]
COPY ["KpopHall.Domain/KpopHall.Domain.csproj", "KpopHall.Domain/"]
COPY ["KpopHall.Infrastructure/KpopHall.Infrastructure.csproj", "KpopHall.Infrastructure/"]
RUN dotnet restore "KpopHall.Api/KpopHall.Api.csproj"
COPY . .
WORKDIR "/src/KpopHall.Api"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KpopHall.Api.dll"]