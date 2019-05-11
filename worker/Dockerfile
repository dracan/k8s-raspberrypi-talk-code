FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build
WORKDIR /src
COPY . .
RUN dotnet restore
# RUN dotnet publish -c Release -r linux-arm
RUN dotnet publish -c Release

# FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim-arm32v7
FROM mcr.microsoft.com/dotnet/core/runtime:2.2
WORKDIR /app
# COPY --from=build /src/bin/Release/netcoreapp2.2/linux-arm/publish .
COPY --from=build /src/bin/Release/netcoreapp2.2/publish .
EXPOSE 80
ENTRYPOINT dotnet worker.dll
