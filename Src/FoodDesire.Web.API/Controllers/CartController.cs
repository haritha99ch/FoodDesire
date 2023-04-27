namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]"), Authorize]
public class CartController : ControllerBase {
    private readonly ICartControllerService _cartControllerService;

    public CartController(ICartControllerService orderControllerService) {
        _cartControllerService = orderControllerService;
    }

    [HttpGet(nameof(Index))]
    public async Task<ActionResult<Order>> Index() {
        string? userId = GetUserId();
        return userId == null
            ? BadRequest("Could not find user!")
            : Ok(await _cartControllerService.GetPendingOrderAsync(int.Parse(userId)));
    }

    [HttpGet(nameof(Details))]
    public async Task<ActionResult<Order>> Details(int orderId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order order = await _cartControllerService.GetOrderAsync(orderId);
        if (order.CustomerId != int.Parse(userId)) return BadRequest("You are not authorized to view this order!");

        return Ok(await _cartControllerService.GetOrderAsync(orderId));
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order) {
        return Ok(await _cartControllerService.CreateOrderAsync(order));
    }

    [HttpPatch(nameof(Pay))]
    public async Task<ActionResult<Order>> Pay(int orderId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order order = await _cartControllerService.GetOrderAsync(orderId);
        if (order.CustomerId != int.Parse(userId)) return BadRequest("You are not authorized to pay this order!");

        //TODO: implement payment gateway
        //TODO: Implement email service
        return Ok(await _cartControllerService.PayForOrderAsync(orderId));
    }

    [HttpPatch(nameof(Update))]
    public async Task<ActionResult<Order>> Update(Order order) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        if (order.CustomerId != int.Parse(userId)) return BadRequest("You are not authorized to pay this order!");

        return Ok(await _cartControllerService.UpdateOrderAsync(order));
    }

    [HttpDelete(nameof(Cancel))]
    public async Task<ActionResult<bool>> Cancel(int orderId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order order = await _cartControllerService.GetOrderAsync(orderId);
        if (order.CustomerId != int.Parse(userId)) return BadRequest("You are not authorized to cancel this order!");

        return Ok(await _cartControllerService.CancelOrderAsync(orderId));
    }

    [HttpGet(nameof(GetFoodItems))]
    public async Task<ActionResult<List<FoodItem>>> GetFoodItems(int orderId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order order = await _cartControllerService.GetOrderAsync(orderId);
        if (order.CustomerId != int.Parse(userId)) return BadRequest("You are not authorized to cancel this order!");

        return Ok(await _cartControllerService.GetAllFoodItemsForOrder(orderId));
    }

    [HttpDelete(nameof(RemoveFoodItem))]
    public async Task<ActionResult<bool>> RemoveFoodItem(int foodItemId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order? order = await _cartControllerService.GetPendingOrderAsync(int.Parse(userId));
        if (order == null) return BadRequest("Could not find order!");


        FoodItem? foodItem = await _cartControllerService.GetFoodItemByIdAsync(foodItemId);
        if (foodItem == null) return BadRequest("Could not find food item!");

        if (foodItem.OrderId != order.Id) return BadRequest("You are not authorized to remove this food item!");

        bool foodItemDelete = await _cartControllerService.RemoveFoodItem(foodItemId);
        return foodItemDelete ? Ok(foodItemDelete) : BadRequest(foodItemDelete);
    }

    private string? GetUserId() {
        return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
