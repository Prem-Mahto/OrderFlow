namespace OrderFlow.Inventory;

/// <summary>
/// Inventory context aggregate for the quantity available under one SKU.
/// </summary>
public sealed class InventoryStock
{
    public string Sku { get; }

    public int AvailableQuantity { get; private set; }

    public int ReservedQuantity { get; private set; }

    public InventoryStock(string sku, int availableQuantity)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("SKU is required.", nameof(sku));
        }

        if (availableQuantity < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(availableQuantity),
                "Available quantity cannot be negative.");
        }

        Sku = sku;
        AvailableQuantity = availableQuantity;
    }

    public bool TryReserve(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Reservation quantity must be positive.");
        }

        if (AvailableQuantity < quantity)
        {
            return false;
        }

        AvailableQuantity -= quantity;
        ReservedQuantity += quantity;
        return true;
    }

    public void Release(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Released quantity must be positive.");
        }

        if (ReservedQuantity < quantity)
        {
            throw new InvalidOperationException("Cannot release more stock than is reserved.");
        }

        AvailableQuantity += quantity;
        ReservedQuantity -= quantity;
    }
}
