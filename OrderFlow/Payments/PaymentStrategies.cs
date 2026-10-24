namespace OrderFlow.Payments;

public interface IPaymentStrategy
{
    bool ProcessPayment(decimal orderTotal);
}


public sealed class CardPaymentStrategy : IPaymentStrategy
{
    private readonly IPaymentGateway _paymentGateway;

    public CardPaymentStrategy(IPaymentGateway paymentGateway)
    {
        ArgumentNullException.ThrowIfNull(paymentGateway);
        _paymentGateway = paymentGateway;
    }

    public bool ProcessPayment(decimal orderTotal) => _paymentGateway.ChargeCard(orderTotal);
}

public sealed class PayPalPaymentStrategy : IPaymentStrategy
{
    private readonly IPaymentGateway _paymentGateway;

    public PayPalPaymentStrategy(IPaymentGateway paymentGateway)
    {
        ArgumentNullException.ThrowIfNull(paymentGateway);
        _paymentGateway = paymentGateway;
    }

    public bool ProcessPayment(decimal orderTotal) => _paymentGateway.ChargePayPal(orderTotal);
}

public sealed class UpiPaymentStrategy : IPaymentStrategy
{
    private readonly IPaymentGateway _paymentGateway;

    public UpiPaymentStrategy(IPaymentGateway paymentGateway)
    {
        ArgumentNullException.ThrowIfNull(paymentGateway);
        _paymentGateway = paymentGateway;
    }

    public bool ProcessPayment(decimal orderTotal) => _paymentGateway.ChargeUpi(orderTotal);
}
