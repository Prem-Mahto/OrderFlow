using OrderFlow.Test.Mocks;

namespace OrderFlow.Test;

public class NotificationObserverTests
{
    public static void FailedNotification_test()
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
        TestAssert.Equal(1, validator.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount, "The payment should be processed once.");
        TestAssert.Equal(1, repository.CallCount, "The order should be saved once.");
        TestAssert.Same(order, repository.LastSavedOrder, "The processed order should be saved.");
        TestAssert.Equal(1, failingObserver.CallCount, "The failing observer should be notified once.");
    }
}
