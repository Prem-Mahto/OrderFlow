namespace OrderFlow.Payments;

public interface IPaymentStrategyFactory
{
    IPaymentStrategy Create(string paymentMethod);
}

public sealed class PaymentStrategyFactory : IPaymentStrategyFactory
{
    private readonly IPaymentGateway _paymentGateway;

    public PaymentStrategyFactory(IPaymentGateway paymentGateway)
    {
        ArgumentNullException.ThrowIfNull(paymentGateway);
        _paymentGateway = paymentGateway;
    }
    public IPaymentStrategy Create(string paymentMethod)
    {
        return paymentMethod switch
        {
            "CARD" => new CardPaymentStrategy(_paymentGateway),
            "PAYPAL" => new PayPalPaymentStrategy(_paymentGateway),
            "UPI" => new UpiPaymentStrategy(_paymentGateway),
            _ => throw new ArgumentException("Unsupported payment method.", nameof(paymentMethod)),
        };
    }
}
