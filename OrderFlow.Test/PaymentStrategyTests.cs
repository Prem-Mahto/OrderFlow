using OrderFlow;

namespace OrderFlow.Test;

public class PaymentStrategyTests
{
    public static void CardPaymentStrategy_ChargesCardThroughGateway()
    {
        // Arrange
        var gateway = new RecordingPaymentGateway();
        var strategy = new CardPaymentStrategy(gateway);

        // Act
        var succeeded = strategy.ProcessPayment(100m);

        // Assert
        TestAssert.Equal(true, succeeded, "The card payment result should come from the gateway.");
        TestAssert.Equal(1, gateway.CardChargeCallCount, "The card gateway should be called once.");
        TestAssert.Equal(100m, gateway.LastCardChargeAmount, "The card gateway should receive the order total.");
    }

    public static void PaymentStrategyFactory_CreateWithCard_ReturnsConfiguredCardStrategy()
    {
        // Arrange
        var gateway = new RecordingPaymentGateway();

        var factory = new PaymentStrategyFactory(gateway);
        // Act
        var selectedStrategy = factory.Create("CARD");

        // Assert
        TestAssert.IsType<CardPaymentStrategy>(selectedStrategy, "CARD should create a card payment strategy.");
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

