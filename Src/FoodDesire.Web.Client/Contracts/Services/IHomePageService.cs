namespace FoodDesire.Web.Client.Contracts.Services;
public interface IHomePageService {
    Task<List<Recipe>> GetTop10Recipes();
}
