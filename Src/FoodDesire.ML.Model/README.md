# Food Desire Recommendation system

The Food Desire ML project is responsible for generating personalized recommendations for customers based on their preferences and previous interactions. It uses machine learning techniques to train a model that predicts recipe ratings and provides recommendations to enhance the food ordering experience.

## Prerequisites

Before running the ML project, make sure you have the [prerequisites](../../README.md#development-environment-setup) installed.

## Configuration

Azure Blob Storage Configuration
The ML project requires a properly configured Azure Blob Storage connection string to store and retrieve the trained ML model. To set up the connection string securely, follow these steps: [Follow Setting Environment Variables](../FoodDesire.AppSettings/README.md)

## Project Structure

The ML project is structured as follows:

- **PredictionService.cs**: Implements the prediction service responsible for loading the ML model, making predictions, and ensuring the model is loaded before making predictions.
- **RecommendationService.cs**: Implements the recommendation service responsible for training the ML model, evaluating its performance, and saving the trained model.

## How to Use

Run the project to train and host the ML Model.

`FoodDesire/Src/FoodDesire.ML.Model >`
`dotnet run`

This will save the trained model to the Azure Blob Storage container specified in the connection string.

## Usage

The trained ML model can be used in the web application to provide personalized recipe recommendations to customers.

## Additional Notes

The ML project assumes the availability of a data set that provides recipe ratings.
