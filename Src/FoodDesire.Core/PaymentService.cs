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

    public async Task<Payment> PaymentForSupply(Supply supply, decimal value) {
        Admin? admin = await _context.Set<Admin>().FirstOrDefaultAsync();
        Payment payment = new() {
            ManagedBy = admin!.Id,
            Supply = supply,
        };
        payment = await _paymentRepository.Add(payment);
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
