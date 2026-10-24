using OrderFlow.Discount;
using OrderFlow.Orders;
using OrderFlow.Test.xUnit.Mocks;

namespace OrderFlow.Test.xUnit;

public class DiscountStrategyTests
{
    [Fact]
    public static void Create_WithSave10Code_ReturnsStrategyThatAppliesTenPercentDiscount()
    {
        // Arrange
        var factory = new DiscountStrategyFactory();

        // Act
        var strategy = factory.Create("SAVE10");
        var discountedTotal = strategy.ApplyDiscount(100m);

        // Assert
        Assert.Equal(90m, discountedTotal);        
    }

    [Fact]
    public static void VIPDiscountStrategy_ApplyDiscount_Test()
    {
        //Arrange
        var orderTotal = 100m;
        var discountStrategy = new VipDiscountStrategy();

        //Act
        var discountedTotal = discountStrategy.ApplyDiscount(orderTotal);

        //Assert
        Assert.Equal(70m, discountedTotal);
    }

    [Fact]
    public static void OrderProcessed_WithSave10Code_AppliesSave10DiscountStrategy()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerEmail = "customer@example.com",
            Total = 100m,
            PaymentMethod = "CARD",
            DiscountCode = "SAVE10"
        };

        var validator = new MockOrderValidator();
        var repository = new MockOrderRepository();
        var service = new OrderService(validator,
            new DiscountStrategyFactory(),
            new MockPaymentFactory(),
            repository);

        // Act
        service.ProcessOrder(order);

        // Assert
        Assert.Equal(1, validator.CallCount);
        Assert.Equal(1, repository.CallCount);
        Assert.Equal(order, repository.LastSavedOrder);
        Assert.Equal(90m, repository.LastSavedOrder?.Total);
    }

    [Fact]
    public static void OrderProcessed_WithUnknownDiscountCode_AppliesNoDiscount()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerEmail = "customer@example.com",
            Total = 100m,
            PaymentMethod = "CARD",
            DiscountCode = "UNKNOWN"
        };

        var validator = new MockOrderValidator();
        var repository = new MockOrderRepository();
        var service = new OrderService(validator,
            new DiscountStrategyFactory(),
            new MockPaymentFactory(),
            repository);

        // Act
        service.ProcessOrder(order);

        // Assert
        Assert.Equal(1, validator.CallCount);
        Assert.Equal(1, repository.CallCount);
        Assert.Equal(order, repository.LastSavedOrder);
        Assert.Equal(100m, repository.LastSavedOrder?.Total);
    }
}
