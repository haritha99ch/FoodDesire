namespace FoodDesire.ML.Model.Contracts.Services;
internal interface IRecommendationService {
    Task ConfigurePredictionEngine();
    void SaveModel();
}
