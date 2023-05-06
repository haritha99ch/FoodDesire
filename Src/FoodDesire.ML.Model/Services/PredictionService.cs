using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FoodDesire.ML.Model.Services;
public class PredictionService : IPredictionService {
    private readonly BlobServiceClient _blobServiceClient;

    private PredictionEngine<PredictRating, RecipePrediction>? _predictionEngine;
    private readonly MLContext _mlContext;
    private ITransformer? _model;
    private readonly string _blobContainerName = "container-fooddesire-ml-model";
    private readonly string _modelName = "recommender.mdl";

    public PredictionService(BlobServiceClient blobServiceClient) {
        _mlContext = new MLContext(100);
        _blobServiceClient = blobServiceClient;
    }

    public RecipePrediction RecipePrediction(int CustomerId, int RecipeId) {
        RecipePrediction? prediction = _predictionEngine!.Predict(new() { CustomerId = CustomerId, RecipeId = RecipeId });
        return prediction;
    }

    private async Task LoadModel() {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        BlobClient blobClient = containerClient.GetBlobClient(_modelName);
        if (!await blobClient.ExistsAsync())
            throw new FileNotFoundException($"Blob {_modelName} doesn't exist in container {_blobContainerName}.");

        using (var stream = new MemoryStream()) {
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0;
            _model = _mlContext.Model.Load(stream, out _);
        }
        if (_model == null) throw new Exception($"Failed to load Model");

        _predictionEngine = _mlContext.Model.CreatePredictionEngine<PredictRating, RecipePrediction>(_model);
    }

    public async Task<bool> EnsureModelLoaded() {
        if (_predictionEngine != null) return true;
        try {
            await LoadModel();
            return true;
        } catch (Exception) {
            return false;
        }
    }
}
