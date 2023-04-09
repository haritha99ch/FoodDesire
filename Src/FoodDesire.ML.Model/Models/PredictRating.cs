using Microsoft.ML.Data;

namespace FoodDesire.ML.Model;
internal class PredictRating {
    [LoadColumn(0)]
    public int CustomerId { get; set; }
    [LoadColumn(1)]
    public int RecipeId { get; set; }
    [LoadColumn(2)]
    public float Rating { get; set; }
}
