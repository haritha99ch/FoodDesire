namespace FoodDesire.Core.Services;
public class PaymentService : IPaymentService {
    private readonly ITrackingRepository<Payment> _paymentRepository;
    private readonly ITrackingRepository<Supply> _supplyRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Admin> _adminRepository;

    public PaymentService(
        ITrackingRepository<Payment> paymentRepository,
        ITrackingRepository<Supply> supplyRepository,
        IRepository<Order> orderRepository,
        IRepository<Admin> adminRepository
        ) {
        _paymentRepository = paymentRepository;
        _supplyRepository = supplyRepository;
        _orderRepository = orderRepository;
        _adminRepository = adminRepository;
    }

    public async Task<Payment> PaymentForOrder(int orderId) {
        Order? order = await _orderRepository.GetByID(orderId);
        order.Payment = new Payment() {
            OrderId = orderId,
            Value = (order.Delivery == null) ? order.Price : order.Price + order.Delivery.Fee,
            PaymentType = PaymentType.Order
        };
        order = await _orderRepository.Update(order);
        return order.Payment!;
    }

    public async Task<Payment> PaymentForSupply(Supply supply, decimal value) {
        List<Admin> admins = await _adminRepository.GetAll();
        Admin? admin = admins.FirstOrDefault();
        if (admin == null) throw new Exception("No admin found");
        supply.Payment = new Payment() {
            ManagedBy = admin.Id,
            Value = value,
            PaymentType = PaymentType.Supply
        };
        supply = await _supplyRepository.Update(supply);
        await _supplyRepository.SaveChanges();
        return supply.Payment!;
    }

    public async Task<Payment> SalaryForEmployee(Payment payment) {
        List<Admin> admins = await _adminRepository.GetAll();
        Admin? admin = admins.FirstOrDefault();
        if (admin == null) throw new Exception("No admin found");
        payment.ManagedBy = admin.Id;
        payment.PaymentType = PaymentType.Salary;
        Payment newPayment = await _paymentRepository.Add(payment);
        await SavePayment();
        return newPayment;
    }

    public async Task<Payment> PaymentForMaintenance(Payment payment) {
        List<Admin> admins = await _adminRepository.GetAll();
        Admin? admin = admins.FirstOrDefault();
        if (admin == null) throw new Exception("No admin found");
        payment.ManagedBy = admin.Id;
        payment.PaymentType = PaymentType.Maintenance;
        Payment newPayment = await _paymentRepository.Add(payment);
        await SavePayment();
        return newPayment;
    }

    public async Task SavePayment() {
        await _paymentRepository.SaveChanges();
    }
}
