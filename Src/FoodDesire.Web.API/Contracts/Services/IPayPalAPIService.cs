namespace FoodDesire.Web.API.Contracts.Services;
public interface IPayPalAPIService {
    Task<string> CreatePayPalOrder(Order order);
}
