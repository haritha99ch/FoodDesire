namespace FoodDesire.IMS.Core.Contracts.Services;
public interface ISettingsPageService<T> where T : BaseUser {
    Task<T> GetUser(string email);
}
