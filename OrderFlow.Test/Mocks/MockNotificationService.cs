

public class MockNotificationService : INotificationService
{
    public int CallCount { get; private set; }

    public string? LastMessage { get; private set; }

    public string? LastCustomerEmail { get; private set; }

    public void NotifyCustomer(string message, string customerEmail)
    {
        CallCount++;
        LastMessage = message;
        LastCustomerEmail = customerEmail;
    }
}
