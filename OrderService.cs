using OrderFlow;

public class OrderService
{
    public void ProcessOrder(Order order)
    {
        // Validation
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        if (string.IsNullOrWhiteSpace(order.CustomerEmail))
        {
            throw new Exception("Customer email is required.");
        }

        if (order.Total <= 0)
        {
            throw new Exception("Order total must be greater than zero.");
        }

        // Discount
        if (order.DiscountCode == "SAVE10")
        {
            order.Total *= 0.90m;
        }
        else if (order.DiscountCode == "SAVE20")
        {
            order.Total *= 0.80m;
        }
        else if (order.DiscountCode == "VIP")
        {
            order.Total *= 0.70m;
        }

        // Payment
        if (order.PaymentMethod == "CARD")
        {
            Console.WriteLine(
                $"Charging card for {order.Total}");
        }
        else if (order.PaymentMethod == "PAYPAL")
        {
            Console.WriteLine(
                $"Charging PayPal for {order.Total}");
        }
        else if (order.PaymentMethod == "UPI")
        {
            Console.WriteLine(
                $"Charging UPI for {order.Total}");
        }
        else
        {
            throw new Exception("Unsupported payment method.");
        }

        // Notification
        Console.WriteLine(
            $"Sending email to {order.CustomerEmail}");

        // Persistence
        Console.WriteLine(
            $"INSERT INTO Orders VALUES ({order.Id}, {order.Total})");

        Console.WriteLine("Order processed.");
    }
}
