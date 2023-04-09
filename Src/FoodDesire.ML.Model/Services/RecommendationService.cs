namespace FoodDesire.ML.Model;
internal class RecommendationService : IRecommendationService {
    private readonly IRepository<RecipeReview> _recipeRepository;
    private readonly IMapper _mapper;

    private readonly MLContext _mlContext;
    private IDataView? _data;
    private PredictionEngine<RecipeReview, RecipePrediction>? _predictionEngine;
    private ITransformer? _model;
    private TrainTestData _splitData;
    protected static string _modelPath => Path.Combine(AppContext.BaseDirectory, "recommender.mdl");

    public RecommendationService(IMapper mapper, IRepository<RecipeReview> recipeRepository) {
        _mlContext = new MLContext();
        _recipeRepository = recipeRepository;
        _mapper = mapper;
    }

    public async Task ConfigurePredictionEngine() {
        List<RecipeReview> recipeRatings = await _recipeRepository.GetAll();
        if (!recipeRatings.Any()) throw new Exception("No ratings found");

        List<PredictRating> ratings = new();
        recipeRatings.ForEach(e => ratings.Add(_mapper.Map<PredictRating>(e)));
        _data = _mlContext.Data.LoadFromEnumerable(ratings);

        _splitData = _mlContext.Data.TrainTestSplit(_data);

        var pipeline =
            _mlContext.Transforms.Conversion.MapValueToKey("CustomerId")
            .Append(_mlContext.Transforms.Conversion.MapValueToKey("RecipeId"))
            .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
                new MatrixFactorizationTrainer.Options {
                    MatrixColumnIndexColumnName = "CustomerId",
                    MatrixRowIndexColumnName = "RecipeId",
                    LabelColumnName = "Rating"
                }));
        _model = pipeline.Fit(_splitData.TrainSet);

        var predictions = _model.Transform(_splitData.TestSet);
        var metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "Rating");
        Console.WriteLine(metrics);

        _predictionEngine = _mlContext.Model.CreatePredictionEngine<RecipeReview, RecipePrediction>(_model);

        //TODO: Evaluate model
    }

    public void SaveModel() {
        _mlContext.Model.Save(_model, _splitData.TrainSet.Schema, _modelPath);
    }
}

