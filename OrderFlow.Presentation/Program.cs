using OrderFlow.Application;
using OrderFlow.Domain;
using OrderFlow.Infrastructure;
using OrderFlow.Inventory;

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
            [new OrderLine("SKU-1", "Sample product", 1000m, 1)]);

        var paymentStrategyFactory = new PaymentStrategyFactory(new ConsolePaymentGateway());
        var orderRepository = new InMemoryOrderRepository();
        var inventoryReservationService = new InMemoryInventoryReservationService(
            [new InventoryStock("SKU-1", 10)]);
        var eventBus = new InMemoryOrderPlacedEventBus();
        eventBus.Subscribe(new OrderPlacementSaga(orderRepository, inventoryReservationService, paymentStrategyFactory));

        var service = new OrderService(
            new DiscountStrategyFactory(),
            orderRepository,
            eventBus,
            new ConsoleApplicationLogger());

        var legacyEmailAdapter = new LegacyEmailNotificationAdapter(new LegacyEmailClient());
        service.RegisterObserver(new LoggingOrderDecorator(legacyEmailAdapter));

        service.PlaceOrder(order);
        Console.WriteLine($"Order {order.Id} finished in state {order.State}.");
    }
}
