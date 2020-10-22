FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
LABEL maintainer="code@global.ly"

EXPOSE 8080
ENV ASPNETCORE_URLS http://+:8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["uni-resolver-driver-did-ion.csproj", "./src"]
RUN dotnet restore "./uni-resolver-driver-did-ion.csproj"
COPY . .
WORKDIR /src/.
RUN dotnet build "uni-resolver-driver-did-ion.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "uni-resolver-driver-did-ion.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "uni-resolver-driver-did-ion.dll"]
