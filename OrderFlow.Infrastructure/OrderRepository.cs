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
    public  Order? GetById(int orderId)
    {
        return _orders.FirstOrDefault(o => o.Id == orderId);
    }
}
