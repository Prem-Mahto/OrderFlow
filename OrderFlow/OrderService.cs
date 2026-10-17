using OrderFlow;

public class OrderService
{
    public OrderService(IOrderValidator validator,
        IDiscountStrategyFactory discountStrategyFactory,
        IPaymentStrategyFactory paymentStrategyFactory,
        INotificationService notificationService,
        IOrderRepository orderRepository)
    {
        _validator = validator;
        _discountStrategyFactory = discountStrategyFactory;
        _paymentStrategyFactory = paymentStrategyFactory;
        _notificationService = notificationService;
        _orderRepository = orderRepository;
    }

    private readonly IOrderValidator _validator;
    private readonly IDiscountStrategyFactory _discountStrategyFactory;
    private readonly IPaymentStrategyFactory _paymentStrategyFactory;
    private readonly INotificationService _notificationService;
    private readonly IOrderRepository _orderRepository;

    public void ProcessOrder(Order order)
    {
        _validator.Validate(order);

        var discountStrategy = _discountStrategyFactory.Create(order.DiscountCode);

        order.Total = discountStrategy.ApplyDiscount(order.Total);

        var paymentStrategy = _paymentStrategyFactory.Create(order.PaymentMethod);
        if (!paymentStrategy.ProcessPayment(order.Total))
        {
            throw new InvalidOperationException("Payment processing failed.");
        }

        if (!_orderRepository.SaveOrder(order))
        {
            throw new InvalidOperationException("Order could not be saved.");
        }

        _notificationService.NotifyCustomer("Your order has been processed.", order.CustomerEmail);

        Console.WriteLine("Order processed.");
    }
}
