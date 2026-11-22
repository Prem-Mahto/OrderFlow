using OrderFlow.Application;

namespace OrderFlow.Infrastructure;

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
