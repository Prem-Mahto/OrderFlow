using OrderFlow.Application;
using OrderFlow.Domain;

namespace OrderFlow.Infrastructure;

public class LoggingOrderDecorator : IOrderObserver
{
    private readonly IOrderObserver _inner;

    public LoggingOrderDecorator(IOrderObserver inner)
    {
        _inner = inner;
    }
    public void OnOrderProcessed(Order order)
    {
        Console.WriteLine($"Logging before inner observer for order: {order.Id}");
        _inner.OnOrderProcessed(order);
        Console.WriteLine($"Logging after inner observer for order: {order.Id}");

    }
}
