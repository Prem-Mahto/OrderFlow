namespace OrderFlow.Test.xUnit;

using OrderFlow.Test.xUnit.Mocks;

public class PaymentStrategyTests
{
    [Fact]
    public static void Create_CardPaymentStrategyAndProcessPayment_ChargesCardThroughGateway()
    {
        // Arrange
        var gateway = new RecordingPaymentGateway();
        var strategy = new CardPaymentStrategy(gateway);

        // Act
        var succeeded = strategy.ProcessPayment(100m);

        // Assert
        Assert.True(succeeded);
        Assert.Equal(1, gateway.CardChargeCallCount);
        Assert.Equal(100m, gateway.LastCardChargeAmount);
    }

    [Fact]
    public static void PaymentStrategyFactory_CreateWithCard_ReturnsCardStrategy()
    {
        // Arrange
        var gateway = new RecordingPaymentGateway();

        var factory = new PaymentStrategyFactory(gateway);
        // Act
        var selectedStrategy = factory.Create("CARD");

        // Assert
        Assert.IsType<CardPaymentStrategy>(selectedStrategy);
    }

    [Fact]
    public static void PaymentStrategyFactory_CreateWithPayPal_ReturnsPayPalStrategy()
    {
        // Arrange
        var gateway = new RecordingPaymentGateway();

        var factory = new PaymentStrategyFactory(gateway);
        // Act
        var selectedStrategy = factory.Create("PAYPAL");

        // Assert
        Assert.IsType<PayPalPaymentStrategy>(selectedStrategy);
    }

    [Fact]
    public static void PaymentStrategyFactory_CreateWithUpi_ReturnsUpiStrategy()
    {
        // Arrange
        var gateway = new RecordingPaymentGateway();

        var factory = new PaymentStrategyFactory(gateway);
        // Act
        var selectedStrategy = factory.Create("UPI");

        // Assert
        Assert.IsType<UpiPaymentStrategy>(selectedStrategy);
    }

    [Fact]
    public static void PaymentStrategyFactory_CreateWithUnknownMethod_ThrowsArgumentException()
    {
        // Arrange
        var gateway = new RecordingPaymentGateway();
        var factory = new PaymentStrategyFactory(gateway);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => factory.Create("UNKNOWN"));
        Assert.Contains("Unsupported payment method", exception.Message);
        Assert.Equal("paymentMethod", exception.ParamName);
    }

    [Fact]
    public static void CardStrategy_ProcessPayment_CallsGatewayWithCorrectAmount()
    {
        // Arrange
        var gateway = new RecordingPaymentGateway();
        var strategy = new CardPaymentStrategy(gateway);
        var testAmount = 250.75m;

        // Act
        strategy.ProcessPayment(testAmount);

        // Assert
        Assert.Equal(1, gateway.CardChargeCallCount);
        Assert.Equal(testAmount, gateway.LastCardChargeAmount);
    }

    [Fact]
    public static void FailedPayment_PreventsRepositoryPersistenceAndObserverNotification()
    {
        // Arrange
        var failingGateway = new FailingPaymentGateway();
        var validator = new OrderValidator();
        var discountFactory = new DiscountStrategyFactory();
        var paymentFactory = new PaymentStrategyFactory(failingGateway);
        var repository = new MockOrderRepository();
        var observer = new MockOrderObserver();

        var orderService = new OrderService(validator, discountFactory, paymentFactory, repository);
        orderService.RegisterObserver(observer);

        var order = new Order
        {
            Id = 1,
            CustomerEmail = "test@example.com",
            Total = 100m,
            DiscountCode = "NONE",
            PaymentMethod = "CARD"
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => orderService.ProcessOrder(order));
        Assert.Equal(0, repository.CallCount);
        Assert.Equal(0, observer.CallCount);
    }
}

internal sealed class RecordingPaymentGateway : IPaymentGateway
{
    public int CardChargeCallCount { get; private set; }

    public decimal LastCardChargeAmount { get; private set; }

    public bool ChargeCard(decimal orderTotal)
    {
        CardChargeCallCount++;
        LastCardChargeAmount = orderTotal;
        return true;
    }

    public bool ChargePayPal(decimal orderTotal) => throw new NotSupportedException();

    public bool ChargeUpi(decimal orderTotal) => throw new NotSupportedException();
}

internal sealed class FailingPaymentGateway : IPaymentGateway
{
    public bool ChargeCard(decimal orderTotal) => false;

    public bool ChargePayPal(decimal orderTotal) => false;

    public bool ChargeUpi(decimal orderTotal) => false;
}
