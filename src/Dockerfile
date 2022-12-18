FROM mcr.microsoft.com/dotnet/sdk:6.0 AS PUBLISH_STAGE
WORKDIR /build
ADD sample-app/SampleApp.Web ./SampleApp.Web
RUN dotnet publish SampleApp.Web/SampleApp.Web.csproj -runtime linux-x64 -o bin -c Release
# comment the line above and uncomment the line below if you're running on a Silicon M1 chip
# RUN dotnet publish SampleApp.Web/SampleApp.Web.csproj --self-contained --runtime linux-arm64 -o bin -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS RUNTIME_STAGE
WORKDIR /app
COPY --from=PUBLISH_STAGE /build/bin ./
