namespace OrderFlow.Test;

public class DiscountStrategyFactoryTests
{
    public static void Create_WithSave10Code_ReturnsStrategyThatAppliesTenPercentDiscount()
    {
        // Arrange
        var factory = new DiscountStrategyFactory();

        // Act
        var strategy = factory.Create("SAVE10");
        var discountedTotal = strategy.ApplyDiscount(100m);

        // Assert
        TestAssert.Equal(90m, discountedTotal, "SAVE10 should select the 10% discount strategy.");
    }
}
