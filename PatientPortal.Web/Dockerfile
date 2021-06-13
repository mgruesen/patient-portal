FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /build
COPY ./PatientPortal.Web.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build --no-restore -o pkg

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
ENV ASPNETCORE_URLS=http://*:8081
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY --from=build /build/pkg .
COPY --from=build /build/wwwroot ./wwwroot
ENTRYPOINT [ "dotnet", "PatientPortal.Web.dll" ]
