using OrderFlow.Test.Mocks;

namespace OrderFlow.Test;

public class LegacyEmailNotificationAdapterTests
{
    public static void AdapterCallsLegacyEmailClient_test()
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
        var mockLegacyEmailClient = new MockLegacyEmailClient();

        var service = new OrderService(validator,
            discountFactory,
            paymentFactory,
            repository);

        service.RegisterObserver(new LegacyEmailNotificationAdapter(mockLegacyEmailClient));
        // Act
        service.ProcessOrder(order);

        // Assert
        TestAssert.Equal(1, validator.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount, "The payment should be processed once.");
        TestAssert.Equal(1, repository.CallCount, "The order should be saved once.");
        TestAssert.Same(order, repository.LastSavedOrder, "The processed order should be saved.");

        TestAssert.Equal(1, mockLegacyEmailClient.CallCount, "The legacy email client should be called once.");
        TestAssert.Equal(order.CustomerEmail, mockLegacyEmailClient.Recipient, "The email recipient should be customer email.");

        TestAssert.Equal("Order Confirmation", mockLegacyEmailClient.Subject, "The email subject should be correct.");
        TestAssert.Equal("Your order has been processed.", mockLegacyEmailClient.Body, "The email body should be correct.");
    }
}
