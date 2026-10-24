using OrderFlow.Orders;
using OrderFlow.Persistence;
namespace OrderFlow.Test.xUnit.Mocks;

public class MockOrderRepository : IOrderRepository
{
    public int CallCount { get; private set; }

    public Order? LastSavedOrder { get; private set; }

    public void Add(Order order)
    {
        CallCount++;
        LastSavedOrder = order;
    }

    public Order? GetById(int orderId)
    {
        throw new NotImplementedException();
    }
}
