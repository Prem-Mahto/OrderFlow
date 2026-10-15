public interface IPaymentProvider
{
    bool ProcessPayment(string paymentMethod, decimal orderTotal);
}

public class PaymentProvider : IPaymentProvider
{
    public bool ProcessPayment(string paymentMethod, decimal orderTotal)
    {
        switch (paymentMethod)
        {
            case "CARD":
                Console.WriteLine($"Charging card for {orderTotal}");
                return true;
            case "PAYPAL":
                Console.WriteLine($"Charging PayPal for {orderTotal}");
                return true;
            case "UPI":
                Console.WriteLine($"Charging UPI for {orderTotal}");
                return true;
            default:
                throw new ArgumentException("Unsupported payment method.", nameof(paymentMethod));
        }
    }
}
