namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase {
    private readonly ICartControllerService _chartControllerService;

    public CartController(ICartControllerService orderControllerService) {
        _chartControllerService = orderControllerService;
    }

    [HttpGet("index")]
    public async Task<Order> Index() {
        //TODO: Get CustomerId By authentication
        return await _chartControllerService.GetPendingOrderAsync(1);
    }

    [HttpGet("{orderId}")]
    public async Task<Order> Details(int orderId) {
        return await _chartControllerService.GetOrderAsync(orderId);
    }

    [HttpPost]
    public async Task<Order> CreateOrder(Order order) {
        return await _chartControllerService.CreateOrderAsync(order);
    }

    [HttpPatch("{orderId}/pay")]
    public async Task<Order> PayForOrder(int orderId) {
        //TODO: Authenticate request
        return await _chartControllerService.PayForOrderAsync(orderId);
    }

    [HttpDelete("{orderId}/cancel")]
    public async Task<bool> CancelOrder(int orderId) {
        //TODO: Authenticate request
        //TODO: Configure Payment API
        return await _chartControllerService.CancelOrderAsync(orderId);
    }
}
