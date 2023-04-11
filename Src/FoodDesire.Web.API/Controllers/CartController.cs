namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]"), Authorize]
public class CartController : ControllerBase {
    private readonly ICartControllerService _chartControllerService;

    public CartController(ICartControllerService orderControllerService) {
        _chartControllerService = orderControllerService;
    }

    [HttpGet(nameof(Index))]
    public async Task<ActionResult<Order>> Index() {
        string? userId = GetUserId();
        return userId == null
            ? BadRequest("Could not find user!")
            : Ok(await _chartControllerService.GetPendingOrderAsync(int.Parse(userId)));
    }

    [HttpGet(nameof(orderId))]
    public async Task<ActionResult<Order>> Details(int orderId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order order = await _chartControllerService.GetOrderAsync(orderId);
        if (order.CustomerId != int.Parse(userId)) return BadRequest("You are not authorized to view this order!");

        return Ok(await _chartControllerService.GetOrderAsync(orderId));
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order) {
        return Ok(await _chartControllerService.CreateOrderAsync(order));
    }

    [HttpPatch($"{nameof(orderId)}/{nameof(Pay)}")]
    public async Task<ActionResult<Order>> Pay(int orderId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order order = await _chartControllerService.GetOrderAsync(orderId);
        if (order.CustomerId != int.Parse(userId)) return BadRequest("You are not authorized to pay this order!");

        //TODO: implement payment gateway
        //TODO: Implement email service
        return Ok(await _chartControllerService.PayForOrderAsync(orderId));
    }

    [HttpDelete($"{nameof(orderId)}/{nameof(Cancel)}")]
    public async Task<ActionResult<bool>> Cancel(int orderId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order order = await _chartControllerService.GetOrderAsync(orderId);
        if (order.CustomerId != int.Parse(userId)) return BadRequest("You are not authorized to cancel this order!");

        return Ok(await _chartControllerService.CancelOrderAsync(orderId));
    }

    [HttpPatch($"{nameof(RemoveFoodItem)}/{nameof(foodItemId)}")]
    public async Task<ActionResult<bool>> RemoveFoodItem(int foodItemId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order? order = await _chartControllerService.GetPendingOrderAsync(int.Parse(userId));
        if (order == null) return BadRequest("Could not find order!");

        FoodItem? foodItem = order.FoodItems?.FirstOrDefault(e => e.Id == foodItemId);
        if (foodItem == null) return BadRequest("Could not find food item!");

        bool foodItemDelete = await _chartControllerService.RemoveFoodItem(foodItemId);
        return foodItemDelete ? Ok(foodItemDelete) : BadRequest(foodItemDelete);
    }

    private string? GetUserId() {
        return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
