using OrderFlow.Application;
using OrderFlow.Domain;
using OrderFlow.Infrastructure;

namespace OrderFlow.Presentation;

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
            new InMemoryOrderRepository(),
            new ConsoleApplicationLogger());

        var legacyEmailAdapter = new LegacyEmailNotificationAdapter(new LegacyEmailClient());
        service.RegisterObserver(new LoggingOrderDecorator(legacyEmailAdapter));

        service.ProcessOrder(order);
        Console.WriteLine("Order processed.");
    }
}
