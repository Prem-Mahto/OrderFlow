using OrderFlow.Application;
using OrderFlow.Domain;

namespace OrderFlow.Infrastructure;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new List<Order>();
    public void Add(Order order)
    {
        _orders.Add(order);
    }

    public void Update(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        var index = _orders.FindIndex(existingOrder => existingOrder.Id == order.Id);
        if (index < 0)
        {
            throw new InvalidOperationException($"Order {order.Id} does not exist in the in-memory repository.");
        }

        _orders[index] = order;
    }

    public  Order? GetById(int orderId)
    {
        return _orders.FirstOrDefault(o => o.Id == orderId);
    }
}
