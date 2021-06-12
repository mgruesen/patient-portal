# patient-portal
A web based patient portal to collect and update patient information. Comprised of a web API and MVC web application.

## PatientPortal Api
Contains basic CRUD operations to manage patient information stored in a relational database as well as login functionality.

## PatientPortal Web
MVC web application that interacts with the web API.

## Run
A docker-compose file has been provided to run the Api, Web, and Database. EF Core is used to manage the database state, and must apply migrations before connecting the Api to it.

```bash
docker-compose up -d --build db
dotnet tool install
dotnet ef database update --project PatientPortal.Api --connection 'server=localhost;user=app;password=Notreallysecure1'
```

After that, the Api and Web applications can be started.

```bash
docker-compose up -d --build api web
```

## Tests
Some testing has been added to the Api.

```bash
dotnet test
```
