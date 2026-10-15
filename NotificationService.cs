public class NotificationService
{
    public void NotifyCustomer(string message, string customerEmail)
    {
        Console.WriteLine($"Sending email to {customerEmail}: {message}");
    }
}
