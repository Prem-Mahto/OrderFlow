using OrderFlow.Application;
using OrderFlow.Inventory;

namespace OrderFlow.Infrastructure;

/// <summary>
/// In-memory Inventory adapter with all-or-nothing reservation and release by order ID.
/// </summary>
public sealed class InMemoryInventoryReservationService : IInventoryReservationService
{
    private readonly object _sync = new();
    private readonly Dictionary<string, InventoryStock> _stockBySku = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<int, Dictionary<string, int>> _reservationsByOrder = [];

    public InMemoryInventoryReservationService(IEnumerable<InventoryStock> initialStock)
    {
        ArgumentNullException.ThrowIfNull(initialStock);
        foreach (var stock in initialStock)
        {
            ArgumentNullException.ThrowIfNull(stock);
            if (!_stockBySku.TryAdd(stock.Sku, new InventoryStock(stock.Sku, stock.AvailableQuantity)))
            {
                throw new ArgumentException($"Duplicate inventory SKU '{stock.Sku}'.", nameof(initialStock));
            }
        }
    }

    public bool TryReserve(int orderId, IReadOnlyList<OrderPlacedLine> lines)
    {
        if (orderId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(orderId), "Order ID must be positive.");
        }

        ArgumentNullException.ThrowIfNull(lines);
        if (lines.Count == 0)
        {
            throw new ArgumentException("At least one inventory line is required.", nameof(lines));
        }

        if (lines.Any(line => line is null))
        {
            throw new ArgumentException("Inventory lines cannot contain null entries.", nameof(lines));
        }

        var requestedQuantities = lines
            .GroupBy(line => line.Sku, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.Sum(line => line.Quantity), StringComparer.OrdinalIgnoreCase);

        lock (_sync)
        {
            if (_reservationsByOrder.TryGetValue(orderId, out var existingReservation))
            {
                if (ReservationsMatch(existingReservation, requestedQuantities))
                {
                    return true;
                }

                throw new InvalidOperationException($"Order {orderId} already has a different inventory reservation.");
            }

            if (requestedQuantities.Any(request =>
                    !_stockBySku.TryGetValue(request.Key, out var stock)
                    || stock.AvailableQuantity < request.Value))
            {
                return false;
            }

            foreach (var request in requestedQuantities)
            {
                _stockBySku[request.Key].TryReserve(request.Value);
            }

            _reservationsByOrder.Add(orderId, requestedQuantities);
            return true;
        }
    }

    public bool Release(int orderId)
    {
        lock (_sync)
        {
            if (!_reservationsByOrder.Remove(orderId, out var reservation))
            {
                return false;
            }

            foreach (var line in reservation)
            {
                _stockBySku[line.Key].Release(line.Value);
            }

            return true;
        }
    }

    public int GetAvailableQuantity(string sku)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);
        lock (_sync)
        {
            return _stockBySku.TryGetValue(sku, out var stock)
                ? stock.AvailableQuantity
                : throw new KeyNotFoundException($"SKU '{sku}' is not present in inventory.");
        }
    }

    private static bool ReservationsMatch(
        IReadOnlyDictionary<string, int> existingReservation,
        IReadOnlyDictionary<string, int> requestedQuantities) =>
        existingReservation.Count == requestedQuantities.Count
        && existingReservation.All(request =>
            requestedQuantities.TryGetValue(request.Key, out var quantity) && quantity == request.Value);
}
