# Use Alpine Linux
ARG VERSION=3.1-alpine

FROM mcr.microsoft.com/dotnet/core/sdk:$VERSION as build-env

# Publish the app as self-contained, a single binary and
# trim any unneccesary .NET core functionality
WORKDIR /app
ADD /src .
RUN dotnet publish \
  --runtime alpine-x64 \
  --self-contained true \
  /p:PublishTrimmed=true \
  /p:PublishSingleFile=true \
  -c Release \
  -o ./output

# Use core runtime dependencies rather
FROM mcr.microsoft.com/dotnet/core/runtime-deps:$VERSION
WORKDIR /app
COPY --from=build-env /app/output .
ENV \
  DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://+:8080

EXPOSE 8080
ENTRYPOINT ["./uni-resolver-driver-did-ion"]