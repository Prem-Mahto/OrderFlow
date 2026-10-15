using OrderFlow;

public class Program
{
    public static void Main()
    {
        var order = new Order
        {
            Id = 1,
            CustomerEmail = "prem@example.com",
            Total = 1000,
            PaymentMethod = "CARD",
            DiscountCode = "SAVE10"
        };

        var service = new OrderService();

        service.ProcessOrder(order);
    }
}
