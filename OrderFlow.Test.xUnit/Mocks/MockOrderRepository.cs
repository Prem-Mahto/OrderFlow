using OrderFlow.Application;
using OrderFlow.Domain;

namespace OrderFlow.Test.xUnit.Mocks;
public class MockOrderRepository : IOrderRepository
{
    public int CallCount { get; private set; }

    public int UpdateCallCount { get; private set; }

    public Order? LastSavedOrder { get; private set; }

    public void Add(Order order)
    {
        CallCount++;
        LastSavedOrder = order;
    }

    public void Update(Order order)
    {
        UpdateCallCount++;
        LastSavedOrder = order;
    }

    public Order? GetById(int orderId)
    {
        return LastSavedOrder?.Id == orderId ? LastSavedOrder : null;
    }
}
