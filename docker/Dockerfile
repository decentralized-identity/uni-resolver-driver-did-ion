FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /working
COPY . .
RUN dotnet publish "src/uni-resolver-driver-did-ion.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
LABEL maintainer="code@global.ly"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "uni-resolver-driver-did-ion.dll"]
EXPOSE 8080
ENV ASPNETCORE_URLS http://+:5000
