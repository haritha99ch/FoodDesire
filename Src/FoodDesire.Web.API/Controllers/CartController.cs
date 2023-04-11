namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase {
    private readonly ICartControllerService _chartControllerService;

    public CartController(ICartControllerService orderControllerService) {
        _chartControllerService = orderControllerService;
    }

    [HttpGet(nameof(Index))]
    public async Task<ActionResult<Order>> Index() {
        //TODO: Get CustomerId By authentication
        return Ok(await _chartControllerService.GetPendingOrderAsync(1));
    }

    [HttpGet(nameof(orderId))]
    public async Task<ActionResult<Order>> Details(int orderId) {
        return Ok(await _chartControllerService.GetOrderAsync(orderId));
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order) {
        return Ok(await _chartControllerService.CreateOrderAsync(order));
    }

    [HttpPatch($"{nameof(orderId)}/{nameof(Pay)}")]
    public async Task<ActionResult<Order>> Pay(int orderId) {
        //TODO: Authenticate request
        return Ok(await _chartControllerService.PayForOrderAsync(orderId));
    }

    [HttpDelete($"{nameof(orderId)}/{nameof(Cancel)}")]
    public async Task<ActionResult<bool>> Cancel(int orderId) {
        //TODO: Authenticate request
        //TODO: Configure Payment API
        return Ok(await _chartControllerService.CancelOrderAsync(orderId));
    }
}
