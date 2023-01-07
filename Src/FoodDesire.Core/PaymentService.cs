namespace FoodDesire.Core;
public class PaymentService: IPaymentService {
    private readonly IRepository<Payment> _paymentRepository;
    private readonly FoodDesireContext _context;

    public PaymentService(
        IRepository<Payment> paymentRepository,
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
}
