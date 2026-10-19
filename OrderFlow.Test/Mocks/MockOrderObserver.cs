namespace OrderFlow.Test.Mocks;

public class MockFailingOrderObserver : IOrderObserver
{
    public int CallCount { get; private set; }
    public Order? LastSavedOrder { get; private set; }
    public void OnOrderProcessed(Order order)
    {
        CallCount++;
        LastSavedOrder = order;
        throw new InvalidOperationException("Notification provider is unavailable.");
    }
}
