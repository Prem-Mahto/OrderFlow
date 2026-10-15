public interface INotificationService
{
    void NotifyCustomer(string message, string customerEmail);
}

public class NotificationService : INotificationService
{
    public void NotifyCustomer(string message, string customerEmail)
    {
        Console.WriteLine($"Sending email to {customerEmail}: {message}");
    }
}
