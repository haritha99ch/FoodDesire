namespace FoodDesire.Models.Helpers;
public class PaymentModelHelper {
    public static void SetSupply(Supply supply, ref Supply _supply, ref PaymentType paymentType) {
        _supply = supply;
        if(supply == null)
            return;
        paymentType = PaymentType.Supply;
    }
    public static void SetOrder(Order order, ref Order _order, ref PaymentType paymentType) {
        _order = order;
        if(order == null)
            return;
        paymentType = PaymentType.Order;
    }
    public static void SetEmployee(int? employeeId, ref int? _employeeId, ref PaymentType paymentType) {
        _employeeId = (int)employeeId!;
        paymentType = PaymentType.Salary;
    }
}
