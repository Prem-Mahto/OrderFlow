using OrderFlow.Orders;

namespace OrderFlow.Persistence;

public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(int orderId);
}

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
