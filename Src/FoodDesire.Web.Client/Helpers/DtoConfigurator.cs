using AutoMapper;

namespace FoodDesire.Web.Client.Helpers;
public class DtoConfigurator {
    public static void Configure(IMapperConfigurationExpression config) {
        config.CreateMap<FoodItemIngredientDetail, FoodItemIngredient>();
    }
}
