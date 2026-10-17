namespace OrderFlow;

#region payment gateway
public interface IPaymentGateway
{
    bool ChargeCard(decimal orderTotal);

    bool ChargePayPal(decimal orderTotal);

    bool ChargeUpi(decimal orderTotal);
}

public sealed class ConsolePaymentGateway : IPaymentGateway
{
    public bool ChargeCard(decimal orderTotal)
    {
        Console.WriteLine($"Charging card for {orderTotal}");
        return true;
    }

    public bool ChargePayPal(decimal orderTotal)
    {
        Console.WriteLine($"Charging PayPal for {orderTotal}");
        return true;
    }

    public bool ChargeUpi(decimal orderTotal)
    {
        Console.WriteLine($"Charging UPI for {orderTotal}");
        return true;
    }
}
#endregion

#region payment strategy
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
#endregion

#region payment strategy factory
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

#endregion
