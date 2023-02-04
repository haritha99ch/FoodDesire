using FoodDesire.DAL.Context;
using FoodDesire.DAL.Contracts.Repositories;

namespace FoodDesire.Core;
public class PaymentService: IPaymentService {
    private readonly ITrackingRepository<Payment> _paymentRepository;
    private readonly FoodDesireContext _context;

    public PaymentService(
        ITrackingRepository<Payment> paymentRepository,
        FoodDesireContext context
        ) {
        _paymentRepository = paymentRepository;
        _context = context;
    }

    public async Task<Payment> PaymentForOrder(int orderId) {
        Order? order = await _context.Set<Order>()
            .Include(e => e.FoodItems)
            .SingleAsync(e => e.Id == orderId);

        decimal value = decimal.Zero;
        order.FoodItems!.ToList().ForEach(e => {
            value += e.Price;
        });
        Payment payment = new() {
            Order = order,
            Value = value
        };
        payment = await _paymentRepository.Add(payment);
        await SavePayment();
        return payment;
    }

    public async Task<Payment> PaymentForSupply(Supply supply, decimal value) {
        Admin? admin = await _context.Set<Admin>().FirstOrDefaultAsync();
        Payment payment = new() {
            ManagedBy = admin!.Id,
            Supply = supply,
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
