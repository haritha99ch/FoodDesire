namespace FoodDesire.Core.Test;
public class UserServicesHosting {
    private UserServices _testRunner;
    public UserServicesHosting() {
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddDbContext<FoodDesireContext>(options => {
                    DbContextHelper.ConfigureDbContextOptions("UserServices", options);
                });
                DAL.Configure.ConfigureAllForTesting(services);
                Core.Configure.ConfigureAllForTesting(services);
                services.AddTransient<UserServices>();
            });
        var host = hostBuilder.Build();
        _testRunner = host.Services.GetRequiredService<UserServices>();
    }

    [Test, Order(1)]
    public async Task CreateAdmin() => await _testRunner.CreateAdmin();

    [Test, Order(2)]
    public async Task LoginAsAdmin() => await _testRunner.LoginAsAdmin();

    [Test, Order(3)]
    public async Task UpdateAdmin() => await _testRunner.UpdateAdmin();

    [Test, Order(4)]
    public async Task CreateSupplier() => await _testRunner.CreateSupplier();

    [Test, Order(5)]
    public async Task LoginAsSupplier() => await _testRunner.LoginAsSupplier();

    [Test, Order(6)]
    public async Task UpdateSupplier() => await _testRunner.UpdateSupplier();

    [Test, Order(7)]
    public async Task CreateChef() => await _testRunner.CreateChef();

    [Test, Order(8)]
    public async Task LoginAsChef() => await _testRunner.LoginAsChef();

    [Test, Order(9)]
    public async Task UpdateChef() => await _testRunner.UpdateChef();

    [Test, Order(10)]
    public async Task CreateDeliverer() => await _testRunner.CreateDeliverer();

    [Test, Order(11)]
    public async Task LoginAsDeliverer() => await _testRunner.LoginAsDeliverer();

    [Test, Order(12)]
    public async Task UpdateDeliverer() => await _testRunner.UpdateDeliverer();
}
