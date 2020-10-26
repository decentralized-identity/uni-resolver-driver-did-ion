FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /working
COPY . .
RUN dotnet publish "src/uni-resolver-driver-did-ion.csproj" -c Release -o /app/publish
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
LABEL maintainer="code@global.ly"
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://+:8080
ENTRYPOINT ["dotnet", "uni-resolver-driver-did-ion.dll"]

