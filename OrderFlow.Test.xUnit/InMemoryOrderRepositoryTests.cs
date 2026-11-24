using OrderFlow.Application;
using OrderFlow.Domain;
using OrderFlow.Infrastructure;
using OrderFlow.Test.xUnit.Mocks;

namespace OrderFlow.Test.xUnit;
public class InMemoryOrderRepositoryTests
{
    [Fact]
    public static void OrderProcessed_WithInMemoryRepository_SavesOrder()
    {
        // Arrange
        var order = OrderTestData.CreateOrder();
       
        var discountFactory = new MockDiscountStrategyFactory();
        var paymentFactory = new MockPaymentFactory();
        var repository = new InMemoryOrderRepository();
        var service = new OrderService(
            discountFactory,
            paymentFactory,
            repository,
            new MockApplicationLogger());

        // Act
        service.ProcessOrder(order);

        // Assert
        Assert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount);
        Assert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount);

        var retrievedOrder = repository.GetById(order.Id);
        Assert.Equal(order, retrievedOrder);
        Assert.Equal(order.Id, retrievedOrder.Id);
    }
}
