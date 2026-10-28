using OrderFlow.Persistence;

namespace OrderFlow.Test.xUnit;
public class UnitOfWorkPracticeTests
{
    [Fact]
    public void OrderProcessed_WithSufficientStock_ReducesStockAndAddsOrder()
    {
        var unitOfWork = new UnitOfWork(new PracticeInventoryRepository(), new PracticeOrderRepository());

        var service = new PracticeOrderService(unitOfWork);

        var order = new PracticeOrder
        {
            Id = 1,
            ItemId = 1,
            Name = "Sample Item2",
            Price = 10.00m,
            Quantity = 5
        };

        var initialStock = unitOfWork.InventoryRepository.GetStock(order.ItemId);
        service.ProcessOrder(order);
        var finalStock = unitOfWork.InventoryRepository.GetStock(order.ItemId);

        Assert.Equal(initialStock - order.Quantity, finalStock);
        var retrievedOrder = unitOfWork.OrderRepository.GetById(order.Id);
        Assert.Equal(order, retrievedOrder);
    }

    [Fact]
    public void OrderProcessed_WithInsufficientStock_DoesNotReduceStockAndDoesNotAddOrder()
    {
        var unitOfWork = new UnitOfWork(new PracticeInventoryRepository(), new PracticeOrderRepository());
        var service = new PracticeOrderService(unitOfWork);

        var order = new PracticeOrder
        {
            Id = 2,
            ItemId = 2,
            Name = "Sample Item",
            Price = 100.00m,
            Quantity = 10
        };

        var initialStock = unitOfWork.InventoryRepository.GetStock(order.ItemId);
        service.ProcessOrder(order);
        var finalStock = unitOfWork.InventoryRepository.GetStock(order.ItemId);

        Assert.Equal(initialStock, finalStock);
        var retrievedOrder = unitOfWork.OrderRepository.GetById(order.Id);
        Assert.Null(retrievedOrder);
    }
}
