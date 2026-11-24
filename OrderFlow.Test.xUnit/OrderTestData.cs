using OrderFlow.Application;
using OrderFlow.Domain;
using OrderFlow.Infrastructure;
using OrderFlow.Inventory;
using OrderFlow.Test.xUnit.Mocks;

namespace OrderFlow.Test.xUnit;

internal static class OrderTestData
{
    public const string SampleSku = "SKU-1";

    public static Order CreateOrder(decimal unitPrice = 100m, string discountCode = "", int quantity = 1) =>
        new(
            1,
            "customer@example.com",
            "CARD",
            discountCode,
            [new OrderLine(SampleSku, "Sample product", unitPrice, quantity)]);

    public static InMemoryInventoryReservationService CreateInventory(int availableQuantity = 10) =>
        new([new InventoryStock(SampleSku, availableQuantity)]);

    public static OrderService CreateOrderService(
        IOrderRepository orderRepository,
        IInventoryReservationService inventoryReservationService,
        IPaymentStrategyFactory paymentStrategyFactory,
        IDiscountStrategyFactory? discountStrategyFactory = null,
        IApplicationLogger? logger = null)
    {
        var eventBus = new InMemoryOrderPlacedEventBus();
        eventBus.Subscribe(new OrderPlacementSaga(orderRepository, inventoryReservationService, paymentStrategyFactory));
        return new OrderService(
            discountStrategyFactory ?? new DiscountStrategyFactory(),
            orderRepository,
            eventBus,
            logger ?? new MockApplicationLogger());
    }
}
