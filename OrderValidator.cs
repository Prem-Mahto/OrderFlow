using OrderFlow;

public class OrderValidator
{
    public void Validate(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (string.IsNullOrWhiteSpace(order.CustomerEmail))
        {
            throw new ArgumentException("Customer email is required.", nameof(order));
        }

        if (order.Total <= 0)
        {
            throw new ArgumentException("Order total must be greater than zero.", nameof(order));
        }
    }
}
