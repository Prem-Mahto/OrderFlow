using OrderFlow.Orders;

namespace OrderFlow.Notifications;

public interface IOrderObserver
{
    void OnOrderProcessed(Order order);
}

public class OrderConfirmationObserver : IOrderObserver
{
    public void OnOrderProcessed(Order order)
    {
        var message = "Your order has been processed.";
        Console.WriteLine($"Sending email to {order.CustomerEmail}: {message}");
    }
}
