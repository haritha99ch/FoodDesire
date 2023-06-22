# Food Desire Inventory Management System

FoodDesire IMS is a WinUI 3 project created with the WinUi3 Template Studio. It is part of the FoodDesire project, which includes an inventory management system for restaurants interested in the project. This system allows restaurants to manage ingredients, recipes, orders, and deliveries. Restaurant owners can also use the system to manage their employees.

## Prerequisites

Before running the WinUI project, make sure you have the [prerequisites](../../README.md#development-environment-setup) installed.

## Configuration

To configure the WinUI project, [Follow Setting Environment Variables](../FoodDesire.AppSettings/README.md).

## Project Structure

The core of the WinUI project is structured as follows:

- **Views/**: Contains the XAML views representing the application's user interface.
- **ViewModels/**: Contains the view models responsible for handling view logic and communicating with the server-side API.
- **Helpers/**: Contains helper classes and configuration files for application setup.
- **Contracts/**: Contains the contract interfaces defining the API service contracts.
- **Services/**: Contains service classes for communicating with the server-side API and handling client-side logic.

## How to Run

To run the WinUi3 project, follow these steps:

1. Open the solution in Visual Studio.
2. Restore the NuGet packages by right-clicking on the solution in the Solution Explorer and selecting “Restore NuGet Packages”.
3. Build the solution by selecting “Build Solution” from the “Build” menu.
4. Press F5 or select “Start Debugging” from the “Debug” menu to run the project.

## Additional Notes

- To add new pages to the project, you can use the Windows Template Studio. In Visual Studio, right-click on the project in the Solution Explorer and select "Add -> New Item". In the "Add New Item" dialog, select "Windows Template Studio" and follow the prompts to add a new page to your project.
- To define new services for your project, you can create a new class that implements the desired functionality and register it with the dependency injection container in the `Helpers/AppConfigurator.cs`
