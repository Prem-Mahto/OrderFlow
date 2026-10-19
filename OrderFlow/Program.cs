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

        var paymentStrategyFactory = new PaymentStrategyFactory(new ConsolePaymentGateway());

        var service = new OrderService(new OrderValidator(),
            new DiscountStrategyFactory(),
            paymentStrategyFactory,
            new OrderRepository());

        service.RegisterObserver(new OrderConfirmationObserver());

        service.ProcessOrder(order);
    }
}
