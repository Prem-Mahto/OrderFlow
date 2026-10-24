using OrderFlow.Orders;
using OrderFlow.Persistence;
using OrderFlow.Test.xUnit.Mocks;

namespace OrderFlow.Test.xUnit;

public class InMemoryOrderRepositoryTests
{
    [Fact]
    public static void OrderProcessed_WithInMemoryRepository_SavesOrder()
    {
        // Arrange
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerEmail = "customer@example.com",
            Total = 100m,
            PaymentMethod = "CARD",
        };

        var validator = new MockOrderValidator();
       
        var discountFactory = new MockDiscountStrategyFactory();
        var paymentFactory = new MockPaymentFactory();
        var repository = new InMemoryOrderRepository();
        var service = new OrderService(validator,
            discountFactory,
            paymentFactory,
            repository);

        // Act
        service.ProcessOrder(order);

        // Assert
        Assert.Equal(1, validator.CallCount);
        Assert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount);

        var retrievedOrder = repository.GetById(order.Id);
        Assert.Equal(order, retrievedOrder);
        Assert.Equal(order.Id, retrievedOrder.Id);
    }
}
