namespace FoodDesire.ML.Model.Contracts.Services;
public interface IPredictionService {
    RecipePrediction RecipePrediction(int CustomerId, int RecipeId);
    bool EnsureModelLoaded();
}
