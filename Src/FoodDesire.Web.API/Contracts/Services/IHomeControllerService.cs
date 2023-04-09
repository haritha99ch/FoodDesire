namespace FoodDesire.Web.API.Contracts.Services;
public interface IHomeControllerService {
    Task<IEnumerable<Recipe>> GetTop10Recipes();
    Task<IEnumerable<Recipe>> GetPredictedRecipes(int customerId);
}
