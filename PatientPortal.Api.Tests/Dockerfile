FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
COPY ./PatientPortal.Api /build/PatientPortal.Api/
COPY ./PatientPortal.Api.Tests /build/PatientPortal.Api.Tests/
WORKDIR /build/PatientPortal.Api.Tests
RUN dotnet build
ENTRYPOINT [ "dotnet", "test" ]
