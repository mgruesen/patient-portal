FROM mcr.microsoft.com/dotnet/sdk:5.0
COPY ./PatientPortal.Api /app
COPY ./.config /app/.config
WORKDIR /app
RUN dotnet tool restore
ENTRYPOINT dotnet ef database update --connection 'server=db;user=app;password=Notreallysecure1'
#[ "dotnet", "ef", "database", "update", "--connection 'server=db;user=app;password=Notreallysecure1'"]
