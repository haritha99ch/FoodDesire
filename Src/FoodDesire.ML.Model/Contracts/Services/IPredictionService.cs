namespace FoodDesire.ML.Model.Contracts.Services;
public interface IPredictionService {
    RecipePrediction RecipePrediction(int CustomerId, int RecipeId);
    Task<bool> EnsureModelLoaded();
}
