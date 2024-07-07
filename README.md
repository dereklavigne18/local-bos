# Description

this is local bos

# Developer Setup

See the [Development Setup Guide](docs/development-setup.md)

# Running the Project

To run the API from VSCode simply select a new run configuration; C# -> API. Once the app is running it should launch the Swagger.

# Running Tests

To run the API's unit tests

```
cd api
dotnet test
```

# PostgreSQL Database

_How to Run_

```
docker-compose up -d api-postgres
```

_How to Access Admin Pages_

1. Start the service with
    ```
    docker-compose up -f admin-site-api-postgres
    ```
2. Access the [pgadmin page](http://localhost:15433) to see database activity, access schemas, run queries, etc. If prompted for credentials, use the following.
    ```
    User ID: localbos
    Password: localbos!
    ```

_How to Run Migrations_

Migrations are stored in the [Api Migrations folder](api/Api/Migrations). Run the following to apply them to your database.

```
cd api/Api
dotnet ef database update
```

_How to Perform DDLs_

Update the [Api Entity](api/Api/Entity) you'd like to change and run the following. Once done you should see your new migrations in the [Api Migrations folder](api/Api/Migrations). Don't forget to run them to apply them to the database!

```
cd api/Api
dotnet ef migrations add <migration-name>
```