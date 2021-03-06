# patient-portal
A web based patient portal to collect and update patient information. Comprised of a web API and MVC web application with a SQL Server database.

## Api
Contains basic CRUD operations to manage patient information stored in a relational database. It also handles user authentication and enforces basic authentication to the endpoints.

## Web
MVC web application that interacts with the Api. Allows users to login, view and edit profile information.

## Database
SQL Server database with application credential seed script. Runs in a container locally.

## Build
Use the docker-compose file to build the Api, Web and Database containers.

```bash
docker-compose build
```

## Run
Use the docker-compose file to run the Api, Web, and Database containers. EF Core is used to manage the database state and must apply migrations before connecting the Api to it.

```bash
docker-compose up -d db
dotnet-compose run db-migration
```

After that the Api and Web applications can be started.

```bash
docker-compose up -d api web
```

Web application is hosted at http://localhost:8081

Api application is hosted at http://localhost:5001

The database is seeded with example users:

Username | Password
--- | ---
`bob` | `password1`
`bill` | `password2`

## Tests
Use the docker-compose file to build and run tests. Ensure your Database container is built and EF core migrations have been applied before running tests.

```bash
docker-compose build tests
docker-compose run tests
```
