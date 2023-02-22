namespace FoodDesire.Core.Contracts.Services;
public interface IPaymentService {
    Task<Payment> PaymentForSupply(Supply supply, decimal value);
    Task<Payment> SalaryForEmployee(Payment payment);
    Task<Payment> PaymentForOrder(int orderId);
    Task<Payment> PaymentForMaintenance(Payment payment);
    Task SavePayment();
}
