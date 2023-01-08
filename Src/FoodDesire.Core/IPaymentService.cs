namespace FoodDesire.Core;
public interface IPaymentService {
    Task<Payment> PaymentForSupply(Supply supply, decimal value);
    Task<Payment> SalaryForEmployee(Payment payment);
    Task SavePayment();
}
