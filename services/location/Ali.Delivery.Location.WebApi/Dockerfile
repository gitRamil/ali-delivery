FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Копируем .csproj из корня проекта
COPY ["Ali.Delivery.Location.WebApi/Ali.Delivery.Location.WebApi.csproj", "Ali.Delivery.Location.WebApi/"]
COPY ["Ali.Delivery.Location.Application/Ali.Delivery.Location.Application.csproj", "Ali.Delivery.Location.Application/"]
COPY ["Ali.Delivery.Location.Domain/Ali.Delivery.Location.Domain.csproj", "Ali.Delivery.Location.Domain/"]
COPY ["Ali.Delivery.Location.Infrastructure/Ali.Delivery.Location.Infrastructure.csproj", "Ali.Delivery.Location.Infrastructure/"]
RUN dotnet restore "Ali.Delivery.Location.WebApi/Ali.Delivery.Location.WebApi.csproj"

COPY . .
WORKDIR "/src/Ali.Delivery.Location.WebApi"

FROM build AS publish
RUN dotnet publish "Ali.Delivery.Location.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ali.Delivery.Location.WebApi.dll"]
