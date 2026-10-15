using OrderFlow;

public class OrderService
{
    public void ProcessOrder(Order order)
    {
        var validator = new OrderValidator();
        var discountCalculator = new DiscountCalculator();
        var paymentProvider = new PaymentProvider();
        var notificationService = new NotificationService();
        var orderRepository = new OrderRepository();

        validator.Validate(order);

        order.Total = discountCalculator.ApplyDiscount(order.DiscountCode, order.Total);

        if (!paymentProvider.ProcessPayment(order.PaymentMethod, order.Total))
        {
            throw new InvalidOperationException("Payment processing failed.");
        }

        if (!orderRepository.SaveOrder(order))
        {
            throw new InvalidOperationException("Order could not be saved.");
        }

        notificationService.NotifyCustomer("Your order has been processed.", order.CustomerEmail);

        Console.WriteLine("Order processed.");
    }
}
