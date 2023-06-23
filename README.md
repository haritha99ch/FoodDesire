# Food Desire: An Online Food Ordering System

Food Desire is an online food ordering system developed by Haritha Rathnayake at the University of Plymouth. The project aims to enhance the food ordering experience by allowing customers to customize their meals and receive personalized recommendations. The system also includes an inventory management system for restaurants to manage ingredients and recipes.

![Poster](Images/Food%20Desire%20Poster.png)

## Features

- Online food ordering
- Meal customization
- Personalized recommendations
- Inventory management for restaurants

## Technologies

The technologies used in this project include C#, .NET, ML.NET, ASP.NET Core, Blazor, WinUi3, and Windows App SDK.

## Development Environment Setup

To set up the development environment for this project, you will need to have the following prerequisites installed:

1. .NET 7.
2. Visual Studio.
    - When installing Visual Studio, make sure to select the following workloads:
      - Desktop development with .NET.
      - Universal Windows Platform development.
      - ASP.NET and web development.
      - Windows App SDK.
3. SQL Server.
4. Git installed on your computer.

### Initial Setup

1. Clone the project.

    `git clone https://github.com/haritha99ch/FoodDesire.git`

2. Change directory to Src.

    `cd FoodDesire/Src`

3. [Set Environment variables](Src/FoodDesire.AppSettings/README.md#setting-environment-variables).
4. [Configure Database using Entity Framework Core.](Src/FoodDesire.DAL/README.md#database-migrations)
5. Build the solution.

    `dotnet build`

6. Run Tests.

    `dotnet test`

7. [Train ML Model](Src/FoodDesire.ML.Model/README.md) (Optional, Dataset required).

    `dotnet run --project ./FoodDesire.ML.Model`

8. Go through Domain Layers [Models](Src/FoodDesire.Models/README.md), [DAL](Src/FoodDesire.DAL/README.md), [Core](Src/FoodDesire.Core/README.md) to understand its structure and functionality.
9. Go through the [Web Application](Src/FoodDesire.Web.API/README.md) to understand its structure and functionality.
10. Go through the [Inventory Management System](Src/FoodDesire.IMS/README.md) to understand its structure and functionality.

## DevOps Pipeline

Automated development process of the system.

1. Releases.
    - Releases are automated using GitHub Actions. Go through [this guide](https://github.com/MicrosoftDocs/windows-dev-docs/blob/docs/hub/apps/package-and-deploy/ci-for-winui3.md) and [workflow file](.github/workflows/production-winui3-fooddesire-dotnet-desktop.yml).
    - Installing.
        - Before installing the MSIX package, it is important to ensure that the certificate is installed in the system. This will allow the package to be trusted and installed    correctly. [Installing a test certificate directly from an MSIX package.](https://www.advancedinstaller.com/install-test-certificate-from-msix.html)
        - It is important to note that the IMS app requires the use of [.NET user secrets](Src/FoodDesire.AppSettings/README.md#setting-environment-variables) on the client system. This ensures that only authorized individuals can access and use the app.

2. Hosting.
    The web app is hosted on Azure App Service. GitHub Actions is used to automate the deployment process. Go through [this guide](https://learn.microsoft.com/en-us/dotnet/devops/github-actions-overview) and [workflow file](.github/workflows/production-web_app-fooddesire-web-sea-dev-001.yml).

Each of these steps is followed by automated testing to ensure that the app is functioning correctly. Go through [this guide](https://learn.microsoft.com/en-us/dotnet/devops/dotnet-test-github-action) and [workflow file](.github/workflows/dotnet-desktop.yml).
