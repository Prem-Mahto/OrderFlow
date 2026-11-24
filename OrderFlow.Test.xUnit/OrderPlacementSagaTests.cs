using OrderFlow.Application;
using OrderFlow.Domain;
using OrderFlow.Infrastructure;
using OrderFlow.Test.xUnit.Mocks;

namespace OrderFlow.Test.xUnit;

public class OrderPlacementSagaTests
{
    [Fact]
    public void PlaceOrder_WithAvailableInventory_ReservesStockChargesPaymentAndMarksPaid()
    {
        var order = OrderTestData.CreateOrder(unitPrice: 20m, quantity: 2);
        var repository = new MockOrderRepository();
        var inventory = OrderTestData.CreateInventory(5);
        var paymentFactory = new MockPaymentFactory();
        var service = OrderTestData.CreateOrderService(repository, inventory, paymentFactory);

        service.PlaceOrder(order);

        Assert.Equal(OrderState.Paid, order.State);
        Assert.Equal(1, repository.CallCount);
        Assert.Equal(1, repository.UpdateCallCount);
        Assert.Equal(40m, paymentFactory.LastCreatedStrategy?.LastOrderTotal);
        Assert.Equal(3, inventory.GetAvailableQuantity(OrderTestData.SampleSku));
    }

    [Fact]
    public void PlaceOrder_WhenInventoryCannotReserve_CancelsOrderWithoutChargingOrChangingStock()
    {
        var order = OrderTestData.CreateOrder(quantity: 2);
        var repository = new MockOrderRepository();
        var inventory = OrderTestData.CreateInventory(1);
        var paymentFactory = new MockPaymentFactory();
        var service = OrderTestData.CreateOrderService(repository, inventory, paymentFactory);

        var exception = Assert.Throws<InvalidOperationException>(() => service.PlaceOrder(order));

        Assert.Contains("Inventory reservation failed", exception.Message);
        Assert.Equal(OrderState.Cancelled, order.State);
        Assert.Null(paymentFactory.LastCreatedStrategy);
        Assert.Equal(1, inventory.GetAvailableQuantity(OrderTestData.SampleSku));
        Assert.Equal(1, repository.CallCount);
        Assert.Equal(1, repository.UpdateCallCount);
    }

    [Fact]
    public void PlaceOrder_WhenPaymentFails_ReleasesReservationAndCancelsOrder()
    {
        var order = OrderTestData.CreateOrder(quantity: 2);
        var repository = new MockOrderRepository();
        var inventory = OrderTestData.CreateInventory(5);
        var paymentFactory = new PaymentStrategyFactory(new FailingPaymentGateway());
        var service = OrderTestData.CreateOrderService(repository, inventory, paymentFactory);

        var exception = Assert.Throws<InvalidOperationException>(() => service.PlaceOrder(order));

        Assert.Contains("inventory was released", exception.Message);
        Assert.Equal(OrderState.Cancelled, order.State);
        Assert.Equal(5, inventory.GetAvailableQuantity(OrderTestData.SampleSku));
        Assert.Equal(1, repository.CallCount);
        Assert.Equal(1, repository.UpdateCallCount);
    }

    [Fact]
    public void Handle_WhenOrderPlacedIsRepeated_DoesNotChargeOrReserveAgain()
    {
        var order = OrderTestData.CreateOrder(quantity: 2);
        var repository = new MockOrderRepository();
        var inventory = OrderTestData.CreateInventory(5);
        var paymentFactory = new MockPaymentFactory();
        var eventBus = new InMemoryOrderPlacedEventBus();
        eventBus.Subscribe(new OrderPlacementSaga(repository, inventory, paymentFactory));
        var service = new OrderService(new DiscountStrategyFactory(), repository, eventBus, new MockApplicationLogger());

        service.PlaceOrder(order);
        eventBus.Publish(new OrderPlaced(
            order.Id,
            order.OrderLines.Select(line => new OrderPlacedLine(line.Sku, line.Quantity))));

        Assert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(3, inventory.GetAvailableQuantity(OrderTestData.SampleSku));
    }

    private sealed class FailingPaymentGateway : IPaymentGateway
    {
        public bool ChargeCard(decimal orderTotal) => false;

        public bool ChargePayPal(decimal orderTotal) => false;

        public bool ChargeUpi(decimal orderTotal) => false;
    }
}
