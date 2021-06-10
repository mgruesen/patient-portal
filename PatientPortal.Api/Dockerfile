FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /build
COPY ./PatientPortal.Api.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build --no-restore -o pkg

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /app
COPY --from=build /build/pkg .
ENV ASPNETCORE_URLS=http://localhost:5001/
ENTRYPOINT [ "dotnet", "PatientPortal.Api.dll" ]