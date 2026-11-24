using OrderFlow.Application;
using OrderFlow.Domain;
using OrderFlow.Infrastructure;
using OrderFlow.Test.xUnit.Mocks;

namespace OrderFlow.Test.xUnit;
public class LegacyEmailNotificationAdapterTests
{
    [Fact]
    public static void AdapterCallsLegacyEmailClient_test()
    {
        // Arrange
        var order = OrderTestData.CreateOrder();
        var repository = new MockOrderRepository();
        var discountFactory = new MockDiscountStrategyFactory();
        var paymentFactory = new MockPaymentFactory();
        var mockLegacyEmailClient = new MockLegacyEmailClient();

        var service = new OrderService(
            discountFactory,
            paymentFactory,
            repository,
            new MockApplicationLogger());

        service.RegisterObserver(new LegacyEmailNotificationAdapter(mockLegacyEmailClient));
        // Act
        service.ProcessOrder(order);

        // Assert
        Assert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(1, repository.CallCount);
        Assert.Equal(order, repository.LastSavedOrder);

        Assert.Equal(1, mockLegacyEmailClient.CallCount);
        Assert.Equal(order.CustomerEmail, mockLegacyEmailClient.Recipient);

        Assert.Equal("Order Confirmation", mockLegacyEmailClient.Subject);
        Assert.Equal("Your order has been processed.", mockLegacyEmailClient.Body);
    }
}
