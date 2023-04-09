using AutoMapper;

namespace FoodDesire.ML.Model.Helpers;
public class DtoConfigurator {
    public static void Configure(IMapperConfigurationExpression config) {
        config.CreateMap<RecipeRating, PredictRating>();
    }
}
