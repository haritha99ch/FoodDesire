# Food Desire Core

The Food Desire Core project contains the core business logic and services for the Food Desire application. It depends on the repositories from the Data Access Layer (DAL) to perform data operations and implements the application-specific business rules and workflows.

## Project Structure

The Core project is structured as follows:

- **Services/**: Contains the service classes that implement the core business logic and workflows.
- **Contracts/**: Contains the contract interfaces defining the API service contracts.

## Prerequisites

Before using the Core project, make sure you have the [prerequisites](../../README.md#development-environment-setup) installed.

## Usage

To use the Core project in your application, follow these steps:

1. Configure the required services in [Configure.cs](./Configure.cs) and refer in application's services.
2. Utilize the provided service methods to implement the application-specific business logic and workflows.
