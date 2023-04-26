using AutoMapper;

namespace FoodDesire.Web.Client.Helpers;
public class DtoConfigurator {
    public static void Configure(IMapperConfigurationExpression config) {
        config.CreateMap<FoodItemIngredientDetail, FoodItemIngredient>();
        config.CreateMap<Image, ImageB64>();
        config.CreateMap<Recipe, RecipeDetail>();
        config.CreateMap<FoodItem, FoodItemListDetail>();

    }
}
