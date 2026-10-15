

public class MockPaymentProvider : IPaymentProvider
{
    public int CallCount { get; private set; }

    public string? LastPaymentMethod { get; private set; }

    public decimal LastOrderTotal { get; private set; }

    public bool ProcessPayment(string paymentMethod, decimal orderTotal)
    {
        CallCount++;
        LastPaymentMethod = paymentMethod;
        LastOrderTotal = orderTotal;
        return true;
    }
}
