# PostgreSQL Setup

1. Configure the `api-postgres` service in the [Docker Compose file](../../docker-compose.yaml). This acts as your PostgreSQL server. Create an empty directory in `api/postgres-data` and create the `api/init-postgres.sql` file.
2. Configure the `admin-site-api-postgres` service in the [Docker Compose file](../../docker-compose.yaml). This is your admin page for your SQL server. Configure this by creating an empty directory in `api/pgadmin` and create the `api/pgadmin_servers.json` file.
3. Add the SQL connection string to the [appsettings.json file](api/Api/appsettings.json).
4. Install the needed packages with the following.
    ```
    dotnet add package Microsoft.EntityFrameworkCore.Design Microsoft.EntityFrameworkCore.SqlServer Npgsql.EntityFrameworkCore.PostgreSQL
    ```
5. Create your Entity objects. Examples in the [Entity folder](api/Api/Entity)
6. Create the DbContext for the application. See [ApiDbContext](api/Api/Data/ApiDbContext.cs)
7. Configure your Entity objects. Examples in the [DataConfiguration folder](api/Api/Data/DataConfiguration)
8. Add the DbContext to the application builder services in [Program.cs](api/Api/Program.cs)