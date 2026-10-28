using OrderFlow.Payments;

namespace OrderFlow.Test.xUnit.Mocks;

internal class MockPaymentFactory :IPaymentStrategyFactory
{
    public MockPaymentStrategy? LastCreatedStrategy { get; private set; }
    public IPaymentStrategy Create(string paymentMethod)
    {
        var strategy = new MockPaymentStrategy(paymentMethod);
        LastCreatedStrategy = strategy;
        return strategy;
    }
}

public class MockPaymentStrategy : IPaymentStrategy
{
    public int CallCount { get; private set; }

    public string? LastPaymentMethod { get; private set; }

    public decimal LastOrderTotal { get; private set; }

    public MockPaymentStrategy(string paymentMethod)
    {
        LastPaymentMethod = paymentMethod;
    }

    public bool ProcessPayment(decimal orderTotal)
    {
        CallCount++;
        LastOrderTotal = orderTotal;
        return true; // Always succeed for testing purposes
    }
}
