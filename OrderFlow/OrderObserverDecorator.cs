namespace OrderFlow;

public class LoggingOrderObserver : IOrderObserver
{
    private readonly IOrderObserver _inner;

    public LoggingOrderObserver(IOrderObserver inner)
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
