namespace OrderFlow.Domain;

public class OrderLine
{
    public string Sku { get; private set; } = "";

    public string ProductName { get; private set; } = "";

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public decimal LineTotal => UnitPrice * Quantity;

    public OrderLine(string sku, string productName, decimal unitPrice, int quantity)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("SKU is required.", nameof(sku));
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("Product name is required.", nameof(productName));
        }

        if (unitPrice <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be greater than zero.");
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        Sku = sku;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}
