namespace OrderFlow.Application;

public interface IInventoryReservationService
{
    bool TryReserve(int orderId, IReadOnlyList<OrderPlacedLine> lines);

    bool Release(int orderId);
}
