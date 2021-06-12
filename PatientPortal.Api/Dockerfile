FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /build
COPY ./PatientPortal.Api.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build --no-restore -o pkg

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
ENV ASPNETCORE_URLS=http://*:5001
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY --from=build /build/pkg .
ENTRYPOINT [ "dotnet", "PatientPortal.Api.dll" ]
