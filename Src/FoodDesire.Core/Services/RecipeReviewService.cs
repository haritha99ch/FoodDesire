namespace FoodDesire.Core.Services;
public class RecipeReviewService : IRecipeReviewService {
    private readonly IRepository<RecipeReview> _recipeReviewRepository;

    public RecipeReviewService(IRepository<RecipeReview> recipeReviewRepository) {
        _recipeReviewRepository = recipeReviewRepository;
    }

    public async Task<List<RecipeReview>> GetReviewsReviewedByCustomer(int customerId) {
        Expression<Func<RecipeReview, bool>> filterExpression = e => e.Id == customerId;

        IQueryable<RecipeReview> filter(IQueryable<RecipeReview> e) => e.Where(filterExpression);

        List<RecipeReview>? reviews = await _recipeReviewRepository.Get(filter);

        return reviews;
    }
}
