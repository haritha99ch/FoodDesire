# Food Desire Web API

The Food Desire Web API project provides a backend API for the Food Desire application, enabling communication between the client-side interface and the server-side services. It handles various endpoints for recipe management, user authentication, cart management, and order processing.

## Prerequisites

Before running the Web API project, make sure you have the [prerequisites](../../README.md#development-environment-setup) installed.

## Configuration

To configure the Web API project, [Follow Setting Environment Variables](../FoodDesire.AppSettings/README.md)

## Project Structure

The Web API project is structured as follows:

- **Helpers/**: Contains helper classes and configuration files for application setup.
- **Contracts/**: Contains the contract interfaces defining the API service contracts.
- **Services/**: Contains the service classes that implement the business logic for various features.
- **Controllers/**: Contains the API controllers responsible for handling HTTP requests and generating responses.

## How to Run

To run the Web API project, follow these steps:

1. Restore the project dependencies:

    `dotnet restore`

2. Build the project:

    `dotnet build`

3. Run the following command to start the application:

    `dotnet run`

4. The API will be accessible at <https://localhost:port/api/>, where *port* is the configured port number (default: 5001).

## Accessing the Blazor Client App

The Food Desire Web API project also serves the Food Desire Blazor client app. To access the client app, follow these steps:

1. Start the Web API application.
2. Open a web browser and navigate to <https://localhost:port/>, where *port* is the configured port number (default: 5001).
3. The Food Desire Blazor client app will be loaded and accessible, providing a user interface for interacting with the application's features.

## API Documentation

The Food Desire Web API provides Swagger documentation for easy API exploration and testing. To access the API documentation, follow these steps:

1. Start the Web API application.
2. Open a web browser and navigate to <https://localhost:port/api/swagger>, where *port* is the configured port number (default: 5001).
3. The Swagger UI will be displayed, showcasing the available API endpoints, request/response models, and allowing you to test the endpoints interactively.

## Additional Notes

- The Web API project depends on the Food Desire ML project for generating recipe recommendations. Ensure that the ML model is trained and available in the Azure Blob Storage specified in the configuration.
- Custom services can be added or modified in the *AppConfigurator.ConfigureServices* method in the `Helpers/AppConfigurator.cs` file to extend the functionality of the API with the dependency injection.

![Web](../../Images/Web%20(1).png)
![Web](../../Images/Web%20(2).png)
![Web](../../Images/Web%20(3).png)
