using OrderFlow;

public class MockOrderRepository : IOrderRepository
{
    public int CallCount { get; private set; }

    public Order? LastSavedOrder { get; private set; }

    public bool SaveOrder(Order order)
    {
        CallCount++;
        LastSavedOrder = order;
        return true;
    }
}
