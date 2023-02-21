using Microsoft.EntityFrameworkCore.Storage;

namespace FoodDesire.Core.Services;
public class PaymentService : IPaymentService {
    private readonly ITrackingRepository<Payment> _paymentRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Admin> _adminRepository;

    public PaymentService(
        ITrackingRepository<Payment> paymentRepository,
        IRepository<Order> orderRepository,
        IRepository<Admin> adminRepository
        ) {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _adminRepository = adminRepository;
    }

    public async Task<Payment> PaymentForOrder(int orderId) {
        Order? order = await _orderRepository.GetByID(orderId);
        order.Payment = new Payment() {
            OrderId = orderId,
            Value = order.Price + order.Delivery!.Fee,
            PaymentType = PaymentType.Order
        };
        order = await _orderRepository.Update(order);
        return order.Payment!;
    }

    public async Task<Payment> PaymentForSupply(Supply supply, decimal value) {
        List<Admin> admins = await _adminRepository.GetAll();
        Admin? admin = admins.FirstOrDefault();
        if (admin == null) throw new Exception("No admin found");
        Payment payment = new() {
            ManagedBy = admin!.Id,
            Supply = supply,
            Value = value,
            PaymentType = PaymentType.Supply
        };
        payment = await _paymentRepository.Add(payment);
        await SavePayment();
        return payment;
    }

    public async Task<Payment> SalaryForEmployee(Payment payment) {
        Payment newPayment = await _paymentRepository.Add(payment);
        await SavePayment();
        return newPayment;
    }

    public async Task SavePayment() {
        await _paymentRepository.SaveChanges();
    }
}
