using OrderFlow.Application;
using OrderFlow.Domain;
using OrderFlow.Infrastructure;

namespace OrderFlow.Presentation;

public class Program
{
    public static void Main()
    {
        var order = new Order(
            1,
            "prem@example.com",
            "CARD",
            "SAVE10",
            [new OrderLine("Sample product", 1000m, 1)]);

        var paymentStrategyFactory = new PaymentStrategyFactory(new ConsolePaymentGateway());

        var service = new OrderService(
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
