using OrderFlow.Domain;

namespace OrderFlow.Test.xUnit;

public class OrderAggregateTests
{
    [Fact]
    public void Constructor_WithNoLines_RejectsOrder()
    {
        Assert.Throws<ArgumentException>(() => new Order(1, "customer@example.com", "CARD", "", []));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WithInvalidId_RejectsOrder(int id)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Order(id, "customer@example.com", "CARD", "", [new OrderLine("BOOK-1", "Book", 10m, 1)]));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithMissingCustomerEmail_RejectsOrder(string customerEmail)
    {
        Assert.Throws<ArgumentException>(() =>
            new Order(1, customerEmail, "CARD", "", [new OrderLine("BOOK-1", "Book", 10m, 1)]));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithMissingPaymentMethod_RejectsOrder(string paymentMethod)
    {
        Assert.Throws<ArgumentException>(() =>
            new Order(1, "customer@example.com", paymentMethod, "", [new OrderLine("BOOK-1", "Book", 10m, 1)]));
    }

    [Fact]
    public void Constructor_CopiesOrderLinesAndCalculatesSubtotal()
    {
        var lines = new List<OrderLine> { new("BOOK-1", "Book", 10m, 2) };
        var order = new Order(1, "customer@example.com", "CARD", "", lines);
        lines.Add(new OrderLine("PEN-1", "Pen", 2m, 1));

        Assert.Single(order.OrderLines);
        Assert.Equal(20m, order.Subtotal);
        Assert.Equal(20m, order.Total);
        Assert.Throws<NotSupportedException>(() => ((IList<OrderLine>)order.OrderLines).Add(new("PEN-1", "Pen", 2m, 1)));
    }

    [Fact]
    public void ApplyDiscount_WithValidDiscount_ChangesTotalOnlyOnce()
    {
        var order = OrderTestData.CreateOrder();

        order.ApplyDiscount(new Save10DiscountStrategy());

        Assert.Equal(100m, order.Subtotal);
        Assert.Equal(90m, order.Total);
        Assert.Throws<InvalidOperationException>(() => order.ApplyDiscount(new Save20DiscountStrategy()));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(101)]
    public void ApplyDiscount_WhenStrategyReturnsInvalidTotal_RejectsDiscount(decimal discountedTotal)
    {
        var order = OrderTestData.CreateOrder();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            order.ApplyDiscount(new FixedDiscountStrategy(discountedTotal)));
    }

    [Fact]
    public void MarkPaid_TransitionsPendingOrderToPaid()
    {
        var order = OrderTestData.CreateOrder();

        order.MarkPaid();

        Assert.Equal(OrderState.Paid, order.State);
    }

    [Fact]
    public void Cancel_TransitionsPendingOrderToCancelled()
    {
        var pendingOrder = OrderTestData.CreateOrder();
        var paidOrder = OrderTestData.CreateOrder();
        paidOrder.MarkPaid();

        pendingOrder.Cancel();

        Assert.Equal(OrderState.Cancelled, pendingOrder.State);
        Assert.Throws<InvalidOperationException>(() => paidOrder.Cancel());
    }

    [Fact]
    public void Ship_RequiresPaidOrder_AndDeliverRequiresShippedOrder()
    {
        var order = OrderTestData.CreateOrder();

        Assert.Throws<InvalidOperationException>(() => order.Ship());
        order.MarkPaid();
        order.Ship();
        Assert.Equal(OrderState.Shipped, order.State);

        order.Deliver();
        Assert.Equal(OrderState.Delivered, order.State);
    }

    [Fact]
    public void TerminalOrders_CannotBeCancelledOrShippedAgain()
    {
        var shippedOrder = OrderTestData.CreateOrder();
        shippedOrder.MarkPaid();
        shippedOrder.Ship();

        Assert.Throws<InvalidOperationException>(() => shippedOrder.Cancel());
        Assert.Throws<InvalidOperationException>(() => shippedOrder.Ship());

        shippedOrder.Deliver();
        Assert.Throws<InvalidOperationException>(() => shippedOrder.Cancel());
        Assert.Throws<InvalidOperationException>(() => shippedOrder.Deliver());
    }

    private sealed class FixedDiscountStrategy(decimal discountedTotal) : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal orderTotal) => discountedTotal;
    }
}
