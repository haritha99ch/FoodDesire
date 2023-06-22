# Food Desire Data Access Layer

The Food Desire Data Access Layer (DAL) project provides a database access layer that encapsulates the underlying database operations. It implements the generic Repository pattern to handle data retrieval, modification, and persistence.

## Project Structure

The DAL project is structured as follows:

- **Repositories/**: Contains the repository classes that provide access to the data entities.
- **Context/**: Contains the database context class that represents the database connection and manages the entity sets.
- **Migrations/**: Contains the database migration files created by Entity Framework Core.

## Prerequisites

Before using the DAL project, make sure you have the [prerequisites](../../README.md#development-environment-setup) installed.

## Configuration

To configure the DAL project, follow these steps:

`pwd`

`FoodDesire/Src/FoodDesire/DAL`

- Make sure the database connection string is set.

    `dotnet user-secrets list`

## Usage

The DAL project provides a generic repository pattern implementation to interact with the database. Use the repository classes in the Repositories folder to perform CRUD (Create, Read, Update, Delete) operations on the data entities.

To use the DAL in your application, follow these steps:

1. Configure the repository classes as dependencies in [Configure.cs](./Configure.cs) and refer in [Core](../FoodDesire.Core/) services.
2. Use the repository methods to query or manipulate the data entities.

## Database Migrations

The DAL project uses Entity Framework Core for database migrations. To create or update the database schema based on the model changes, follow these steps:

`pwd`

`FoodDesire/Src/FoodDesire/DAL`

1. Install dotnet EF core tools.

    `dotnet tool install --global dotnet-ef`

2. Add Migrations.

    `dotnet ef migrations add Initial`

3. Update Database.

    `dotnet ef database update`
