namespace FoodDesire.ML.Model.Services;
public class PredictionService : IPredictionService {
    private PredictionEngine<RecipeReview, RecipePrediction>? _predictionEngine;
    private readonly MLContext _mlContext;
    protected static string _modelPath => Path.Combine(AppContext.BaseDirectory, "recommender.mdl");
    private ITransformer? _model;

    public PredictionService() {
        _mlContext = new MLContext(100);
    }

    public RecipePrediction RecipePrediction(int CustomerId, int RecipeId) {
        RecipePrediction? prediction = _predictionEngine!.Predict(new() { CustomerId = CustomerId, RecipeId = RecipeId });
        return prediction;
    }

    private void LoadModel() {
        if (!File.Exists(_modelPath)) throw new FileNotFoundException($"File {_modelPath} doesn't exist.");

        using (FileStream stream = new FileStream(_modelPath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
            _model = _mlContext.Model.Load(stream, out _);
        }
        if (_model == null) throw new Exception($"Failed to load Model");

        _predictionEngine = _mlContext.Model.CreatePredictionEngine<RecipeReview, RecipePrediction>(_model);
    }

    public bool EnsureModelLoaded() {
        try {
            LoadModel();
            return true;
        } catch (Exception) {
            return false;
        }
    }
}
