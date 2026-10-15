using OrderFlow;
public interface IOrderRepository
{
    bool SaveOrder(Order order);
}

public class OrderRepository : IOrderRepository
{
    public bool SaveOrder(Order order)
    {
        Console.WriteLine($"INSERT INTO Orders VALUES ({order.Id}, {order.Total})");
        return true;
    }
}
