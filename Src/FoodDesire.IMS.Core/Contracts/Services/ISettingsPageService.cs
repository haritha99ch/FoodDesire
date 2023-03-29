namespace FoodDesire.IMS.Core.Contracts.Services;
public interface ISettingsPageService {
    Task<T> GetEmployeeByEmail<T>(string email) where T : BaseUser;
    bool SignUserOutFromIMS();
}
