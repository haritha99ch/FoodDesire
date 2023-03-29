using Microsoft.Extensions.DependencyInjection;

namespace FoodDesire.IMS.Core.Services;
public class SettingsPageService : ISettingsPageService {
    private readonly IServiceProvider _serviceProvider;
    private readonly IAuthenticationService _authenticationService;

    public SettingsPageService(IServiceProvider serviceProvider, IAuthenticationService authenticationService) {
        _serviceProvider = serviceProvider;
        _authenticationService = authenticationService;
    }

    public async Task<T> GetEmployeeByEmail<T>(string email) where T : BaseUser => await _serviceProvider.GetService<IUserService<T>>()!.GetByEmail(email);

    public bool SignUserOutFromIMS() {
        return _authenticationService.SignOutMSAL();
    }
}
