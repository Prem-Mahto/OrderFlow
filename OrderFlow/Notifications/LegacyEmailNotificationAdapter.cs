using OrderFlow.Orders;

namespace OrderFlow.Notifications;

public class LegacyEmailNotificationAdapter : IOrderObserver
{
    private readonly ILegacyEmailClient _legacyEmailClient;

    public LegacyEmailNotificationAdapter(ILegacyEmailClient legacyEmailClient)
    {
        _legacyEmailClient = legacyEmailClient;
    }
    public void OnOrderProcessed(Order order)
    {
        // Use the legacy email client to send an email notification
        _legacyEmailClient.SendEmail(order.CustomerEmail, "Order Confirmation", "Your order has been processed.");
    }
}

public interface ILegacyEmailClient
{
    void SendEmail(string recipient, string subject, string body);
}

public class LegacyEmailClient :ILegacyEmailClient
{
    public void SendEmail(string recipient, string subject, string body)
    {
        // Simulate sending an email using a legacy system
        Console.WriteLine($"Sending email to {recipient} with subject '{subject}' and body '{body}'");
    }
}
