using OrderFlow.Application;
using OrderFlow.Infrastructure;
using OrderFlow.Inventory;

namespace OrderFlow.Test.xUnit;

public class InventoryReservationTests
{
    [Fact]
    public void TryReserve_WithEnoughStock_DecreasesAvailableQuantityAndReleaseRestoresIt()
    {
        var inventory = OrderTestData.CreateInventory(8);
        var lines = new[] { new OrderPlacedLine(OrderTestData.SampleSku, 3) };

        Assert.True(inventory.TryReserve(42, lines));
        Assert.Equal(5, inventory.GetAvailableQuantity(OrderTestData.SampleSku));

        Assert.True(inventory.Release(42));
        Assert.Equal(8, inventory.GetAvailableQuantity(OrderTestData.SampleSku));
        Assert.False(inventory.Release(42));
    }

    [Fact]
    public void TryReserve_WithInsufficientLine_ReservesNoneOfTheOrder()
    {
        var inventory = new InMemoryInventoryReservationService(
        [
            new InventoryStock("SKU-1", 10),
            new InventoryStock("SKU-2", 1)
        ]);
        var lines = new[]
        {
            new OrderPlacedLine("SKU-1", 2),
            new OrderPlacedLine("SKU-2", 2)
        };

        Assert.False(inventory.TryReserve(42, lines));
        Assert.Equal(10, inventory.GetAvailableQuantity("SKU-1"));
        Assert.Equal(1, inventory.GetAvailableQuantity("SKU-2"));
    }

    [Fact]
    public void TryReserve_WithDuplicateOrderAndSameLines_IsIdempotent()
    {
        var inventory = OrderTestData.CreateInventory(8);
        var lines = new[] { new OrderPlacedLine(OrderTestData.SampleSku, 3) };

        Assert.True(inventory.TryReserve(42, lines));
        Assert.True(inventory.TryReserve(42, lines));

        Assert.Equal(5, inventory.GetAvailableQuantity(OrderTestData.SampleSku));
    }

    [Fact]
    public void InventoryStock_CannotReleaseMoreThanReserved()
    {
        var stock = new InventoryStock("SKU-1", 5);
        Assert.True(stock.TryReserve(2));

        Assert.Throws<InvalidOperationException>(() => stock.Release(3));
        stock.Release(2);

        Assert.Equal(5, stock.AvailableQuantity);
        Assert.Equal(0, stock.ReservedQuantity);
    }
}
