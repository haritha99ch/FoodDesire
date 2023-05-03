namespace FoodDesire.Core.Contracts.Services;
public interface IRecipeReviewService {
    Task<List<RecipeReview>> GetReviewsReviewedByCustomer(int customerId);
}
