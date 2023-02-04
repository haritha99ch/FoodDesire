namespace FoodDesire.Core.Contracts.Services;
public interface ICustomerOrderService
{
    Task<List<Order>> GetAllOrdersForCustomerById(int customerId);
    Task<List<Order>> GetAllOrdersToDeliverForCustomerById(int customerId); //Orders that completed yet to be delivered
    Task<List<Order>> GetAllPendingOrdersForCustomerById(int customerId); //Orders that not yet completed

}
