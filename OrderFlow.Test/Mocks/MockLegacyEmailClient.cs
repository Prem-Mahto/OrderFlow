namespace OrderFlow.Test.Mocks;

public class MockLegacyEmailClient : ILegacyEmailClient
{
    public int CallCount { get; private set; }
    public string Recipient { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }

    public void SendEmail(string recipient, string subject, string body)
    {
        CallCount++;
        Recipient = recipient;
        Subject = subject;
        Body = body;
        // Simulate sending an email using a legacy system
        Console.WriteLine($"Mock sending email to {recipient} with subject '{subject}' and body '{body}'");
    }
}
