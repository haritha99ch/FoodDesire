namespace FoodDesire.Web.Client.Contracts.Services;
public interface IHomePageService {
    Task<List<RecipeListItem>> GetTop10Recipes();
}
