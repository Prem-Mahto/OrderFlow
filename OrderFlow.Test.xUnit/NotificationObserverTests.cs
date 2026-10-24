using OrderFlow.Orders;
using OrderFlow.Test.xUnit.Mocks;

namespace OrderFlow.Test.xUnit;

public class NotificationObserverTests
{
    [Fact]
    public static void OrderProcessed_WithFailingNotification_StillSavesOrder()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerEmail = "customer@example.com",
            Total = 100m,
            PaymentMethod = "CARD",
        };

        var validator = new MockOrderValidator();
        var repository = new MockOrderRepository();
        var discountFactory = new MockDiscountStrategyFactory();
        var paymentFactory = new MockPaymentFactory();
        var failingObserver = new MockFailingOrderObserver();
        var service = new OrderService(validator,
            discountFactory,
            paymentFactory,
            repository);

        service.RegisterObserver(failingObserver);
        // Act
        service.ProcessOrder(order);

        // Assert
        Assert.Equal(1, validator.CallCount);
        Assert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(1, repository.CallCount);
        Assert.Equal(order, repository.LastSavedOrder);
        Assert.Equal(1, failingObserver.CallCount);
    }
}
