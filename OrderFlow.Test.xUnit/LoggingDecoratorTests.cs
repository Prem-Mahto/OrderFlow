using OrderFlow.Application;
using OrderFlow.Domain;
using OrderFlow.Infrastructure;
using OrderFlow.Test.xUnit.Mocks;

namespace OrderFlow.Test.xUnit;
public class LoggingDecoratorTests
{
    [Fact]
    public static void ProcessOrder_WithLoggingDecorator_LogsOrderProcessing()
    {
        // Arrange
        var order = OrderTestData.CreateOrder();
        var repository = new MockOrderRepository();
        var discountFactory = new MockDiscountStrategyFactory();
        var paymentFactory = new MockPaymentFactory();
        var orderObserver = new MockOrderObserver();

        var service = OrderTestData.CreateOrderService(
            repository,
            OrderTestData.CreateInventory(),
            paymentFactory,
            discountFactory,
            new MockApplicationLogger());

        service.RegisterObserver(new LoggingOrderDecorator(orderObserver));
        // Act
        service.PlaceOrder(order);

        // Assert
        Assert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(1, repository.CallCount);
        Assert.Equal(1, repository.UpdateCallCount);

        Assert.Equal(1, orderObserver.CallCount);
        Assert.Equal(order, orderObserver.LastSavedOrder);
    }
}
