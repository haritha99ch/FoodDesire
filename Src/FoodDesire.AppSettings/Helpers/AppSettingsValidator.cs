using Microsoft.Extensions.Configuration;

namespace FoodDesire.AppSettings.Helpers;
public static class AppSettingsValidator {
    public static void Validate(IConfigurationBuilder config) {
        IConfigurationRoot? _config = config.Build();
        bool invalidAppSettings = false;
        List<string> errors = new();
        if (_config.GetConnectionString("DefaultConnection") == null) {
            invalidAppSettings = true;
            errors.Add("Missing ConnectionStrings:DefaultConnection");
        }

        if (_config.GetConnectionString("StorageConnection") == null) {
            invalidAppSettings = true;
            errors.Add("Missing ConnectionStrings:StorageConnection");
        }

        if (_config["ClientId"] == null) {
            invalidAppSettings = true;
            errors.Add("Missing ConnectionStrings:ClientId");
        }

        if (_config.GetSection("JWT")["SignInKey"] == null) {
            invalidAppSettings = true;
            errors.Add("Missing JWT:SignInKey");
        }
        _config = null;
        if (!invalidAppSettings) return;
        string message = $"Missing User Secrets. Use 'dotnet user-secrets set' to configure user secrets.\n";
        message = $"{message}{string.Join("\n", errors)}";
        throw new Exception(message);
    }
}
