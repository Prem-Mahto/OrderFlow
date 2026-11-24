namespace OrderFlow.Application;

/// <summary>
/// Integration message carrying only the data Inventory needs to reserve stock.
/// </summary>
public sealed class OrderPlaced
{
    private readonly IReadOnlyList<OrderPlacedLine> _lines;

    public int OrderId { get; }

    public IReadOnlyList<OrderPlacedLine> Lines => _lines;

    public OrderPlaced(int orderId, IEnumerable<OrderPlacedLine> lines)
    {
        if (orderId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(orderId), "Order ID must be positive.");
        }

        ArgumentNullException.ThrowIfNull(lines);
        var lineSnapshot = lines.ToList();
        if (lineSnapshot.Count == 0)
        {
            throw new ArgumentException("An order-placed message must contain at least one line.", nameof(lines));
        }

        if (lineSnapshot.Any(line => line is null))
        {
            throw new ArgumentException("Order-placed lines cannot contain null entries.", nameof(lines));
        }

        OrderId = orderId;
        _lines = lineSnapshot.AsReadOnly();
    }
}

public sealed record OrderPlacedLine
{
    public string Sku { get; }

    public int Quantity { get; }

    public OrderPlacedLine(string sku, int quantity)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("SKU is required.", nameof(sku));
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }

        Sku = sku;
        Quantity = quantity;
    }
}
