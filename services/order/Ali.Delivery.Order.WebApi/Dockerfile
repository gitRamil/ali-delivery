FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Ali.Delivery.Order.WebApi/Ali.Delivery.Order.WebApi.csproj", "Ali.Delivery.Order.WebApi/"]
COPY ["Ali.Delivery.Order.Application/Ali.Delivery.Order.Application.csproj", "Ali.Delivery.Order.Application/"]
COPY ["Ali.Delivery.Order.Domain/Ali.Delivery.Order.Domain.csproj", "Ali.Delivery.Order.Domain/"]
COPY ["Ali.Delivery.Order.Infrastructure/Ali.Delivery.Order.Infrastructure.csproj", "Ali.Delivery.Order.Infrastructure/"]
RUN dotnet restore "Ali.Delivery.Order.WebApi/Ali.Delivery.Order.WebApi.csproj"
COPY . .
WORKDIR "/src/Ali.Delivery.Order.WebApi"
FROM build AS publish
RUN dotnet publish "Ali.Delivery.Order.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ali.Delivery.Order.WebApi.dll"]
