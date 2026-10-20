
using OrderFlow;
using OrderFlow.Practice;
using OrderFlow.Test;


internal class UnitOfWorkPracticeTests
{
    public static void SuccessfulOrder_Test()
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

        TestAssert.Equal(initialStock - order.Quantity, finalStock, "stock is reduced");
        var retrievedOrder = unitOfWork.OrderRepository.GetById(order.Id);
        TestAssert.Same(order, retrievedOrder, "The retrieved order should be the same as the processed order.");
    }

    public static void InsufficientStock_Test()
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

        TestAssert.Equal(initialStock, finalStock, "stock should not be reduced");
        var retrievedOrder = unitOfWork.OrderRepository.GetById(order.Id);
        TestAssert.Null(retrievedOrder, "The order should not be in repository.");
    }
}
