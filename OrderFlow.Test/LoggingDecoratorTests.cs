using OrderFlow.Test.Mocks;

namespace OrderFlow.Test;

public class LoggingDecoratorTests
{
    public static void LoggingDecorator_test()
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
        var orderObserver = new MockOrderObserver();

        var service = new OrderService(validator,
            discountFactory,
            paymentFactory,
            repository);

        service.RegisterObserver(new LoggingOrderObserver(orderObserver));
        // Act
        service.ProcessOrder(order);

        // Assert
        TestAssert.Equal(1, validator.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount, "The payment should be processed once.");
        TestAssert.Equal(1, repository.CallCount, "The order should be saved once.");

        TestAssert.Equal(1, orderObserver.CallCount, "The order observer should be notified once.");
        TestAssert.Same(order, orderObserver.LastSavedOrder, "The inner observer should receive same order.");

    }
}
