namespace FoodDesire.Core.Contracts.Services;
public interface IPaymentService {
    Task<Supply> PaymentForSupply(Supply supply, decimal value);
    Task<Payment> SalaryForEmployee(Payment payment);
    Task<Payment> PaymentForOrder(int orderId);
    Task<Payment> PaymentForMaintenance(decimal value, string description);
    Task<List<Payment>> GetIncome();
    Task<List<Payment>> GetExpenses();
    Task SavePayment();
}
