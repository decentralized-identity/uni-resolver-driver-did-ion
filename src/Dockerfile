FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

ENV ASPNETCORE_URLS http://+:5000

EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["uni-resolver-driver-did-ion.csproj", "./"]
RUN dotnet restore "./uni-resolver-driver-did-ion.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "uni-resolver-driver-did-ion.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "uni-resolver-driver-did-ion.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "uni-resolver-driver-did-ion.dll"]
