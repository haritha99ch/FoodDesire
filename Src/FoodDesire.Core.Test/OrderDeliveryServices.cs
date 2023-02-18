namespace FoodDesire.Core.Test;
[TestFixture]
public class OrderDeliveryServices{
    private readonly IOrderDeliveryService _orderDeliveryServices;
    private readonly IOrderService _orderService;
    private readonly FoodDesireContext _context;
    public OrderDeliveryServices(){
        ApplicationHostHelper.ConfigureHost("OrderDeliveryServices");

        _orderDeliveryServices = ApplicationHostHelper.GetService<IOrderDeliveryService>();
        _orderService = ApplicationHostHelper.GetService<IOrderService>();
        _context = ApplicationHostHelper.GetService<FoodDesireContext>();
    }

    [OneTimeSetUp]
    public async Task Setup(){
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    [OneTimeTearDown]
    public async Task TearDown(){
        await _context.Database.EnsureDeletedAsync();
        ApplicationHostHelper.TearDownHost();
    }

}
