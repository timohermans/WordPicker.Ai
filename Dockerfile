FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY src/WordPicker.Web/WordPicker.Web.csproj src/WordPicker.Web/
COPY tests/WordPicker.Tests/WordPicker.Tests.csproj tests/WordPicker.Tests/
COPY WordPicker.slnx ./
RUN dotnet restore
COPY . .
RUN dotnet publish src/WordPicker.Web/WordPicker.Web.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "WordPicker.Web.dll"]
