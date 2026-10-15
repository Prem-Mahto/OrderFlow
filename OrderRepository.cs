using OrderFlow;

public class OrderRepository
{
    public bool SaveOrder(Order order)
    {
        Console.WriteLine($"INSERT INTO Orders VALUES ({order.Id}, {order.Total})");
        return true;
    }
}
