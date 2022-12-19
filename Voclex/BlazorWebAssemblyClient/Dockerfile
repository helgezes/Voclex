FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorWebAssemblyClient/BlazorWebAssemblyClient.csproj", "BlazorWebAssemblyClient/"]
RUN dotnet restore "BlazorWebAssemblyClient/BlazorWebAssemblyClient.csproj"
COPY . .
WORKDIR "/src/BlazorWebAssemblyClient"
RUN dotnet build "BlazorWebAssemblyClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorWebAssemblyClient.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=publish /app/publish/wwwroot .
COPY BlazorWebAssemblyClient/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
EXPOSE 443