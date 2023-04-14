namespace FoodDesire.Web.Client.Contracts.Services;
public interface IAuthenticationService {
    Task<string> GetAccessTokenAsync();
    Task SetAccessTokenAsync(string token);
    Task<bool> UnsetAccessTokenAsync();
    Task<bool> IsAuthenticated();
}
