namespace FoodDesire.Web.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase {
    private readonly IOrderControllerService _orderControllerService;

    public OrderController(IOrderControllerService orderControllerService) {
        _orderControllerService = orderControllerService;
    }

    [HttpGet(nameof(Index))]
    public async Task<ActionResult<List<Order>>> Index() {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        return Ok(await _orderControllerService.GetOrdersAsync(int.Parse(userId)));
    }

    [HttpGet]
    public async Task<ActionResult<Order>> Detail(int orderId) {
        string? userId = GetUserId();
        if (userId == null) return BadRequest("Could not find user!");

        Order order = await _orderControllerService.GetOrderByIdAsync(orderId);
        if (order == null) return BadRequest("Order not found");
        if (order.CustomerId != int.Parse(userId)) return Unauthorized("You are not authorized to view this");

        return Ok(order);
    }

    private string? GetUserId() => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
}
