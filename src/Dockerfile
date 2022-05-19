FROM mcr.microsoft.com/dotnet/sdk:6.0 AS PUBLISH_STAGE
WORKDIR /build
ADD sample-app/SampleApp.Web ./SampleApp.Web
RUN dotnet publish SampleApp.Web/SampleApp.Web.csproj -r linux-x64 -o bin -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS RUNTIME_STAGE
WORKDIR /app
COPY --from=PUBLISH_STAGE /build/bin ./
