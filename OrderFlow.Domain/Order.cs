using System.Collections.ObjectModel;

namespace OrderFlow.Domain;

/// <summary>
/// Aggregate root for an order and the lines that determine its subtotal.
/// </summary>
public sealed class Order
{
    private readonly ReadOnlyCollection<OrderLine> _orderLines;
    private decimal? _discountedTotal;

    public int Id { get; }

    public string CustomerEmail { get; }

    public decimal Total => _discountedTotal ?? Subtotal;

    public decimal Subtotal => _orderLines.Sum(line => line.LineTotal);

    public string PaymentMethod { get; }

    public string DiscountCode { get; }

    public IReadOnlyList<OrderLine> OrderLines => _orderLines;

    public OrderState State { get; private set; } = OrderState.Pending;

    public Order(
        int id,
        string customerEmail,
        string paymentMethod,
        string discountCode,
        IEnumerable<OrderLine> orderLines)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Order ID must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(customerEmail))
        {
            throw new ArgumentException("Customer email is required.", nameof(customerEmail));
        }

        if (string.IsNullOrWhiteSpace(paymentMethod))
        {
            throw new ArgumentException("Payment method is required.", nameof(paymentMethod));
        }

        ArgumentNullException.ThrowIfNull(orderLines);

        var lines = orderLines.ToList();
        if (lines.Count == 0)
        {
            throw new ArgumentException("An order must contain at least one line.", nameof(orderLines));
        }

        if (lines.Any(line => line is null))
        {
            throw new ArgumentException("Order lines cannot contain null entries.", nameof(orderLines));
        }

        Id = id;
        CustomerEmail = customerEmail;
        PaymentMethod = paymentMethod;
        DiscountCode = discountCode ?? string.Empty;
        _orderLines = lines.AsReadOnly();
    }

    public void ApplyDiscount(IDiscountStrategy discountStrategy)
    {
        EnsureState(OrderState.Pending);
        ArgumentNullException.ThrowIfNull(discountStrategy);

        if (_discountedTotal.HasValue)
        {
            throw new InvalidOperationException("A discount has already been applied to this order.");
        }

        var discountedTotal = discountStrategy.ApplyDiscount(Subtotal);

        if (discountedTotal <= 0 || discountedTotal > Subtotal)
        {
            throw new ArgumentOutOfRangeException(
                nameof(discountStrategy),
                "Discounted total must be greater than zero and cannot exceed the order subtotal.");
        }

        _discountedTotal = discountedTotal;
    }

    public void MarkPaid()
    {
        EnsureState(OrderState.Pending);
        State = OrderState.Paid;
    }

    public void Cancel()
    {
        EnsureState(OrderState.Pending);
        State = OrderState.Cancelled;
    }

    public void Ship()
    {
        EnsureState(OrderState.Paid);
        State = OrderState.Shipped;
    }

    public void Deliver()
    {
        EnsureState(OrderState.Shipped);
        State = OrderState.Delivered;
    }

    private void EnsureState(OrderState expectedState)
    {
        if (State != expectedState)
        {
            throw new InvalidOperationException(
                $"This operation requires order state {expectedState}, but the order is {State}.");
        }
    }
}

public enum OrderState
{
    Pending,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}
