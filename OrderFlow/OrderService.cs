using OrderFlow;

public class OrderService
{
    public OrderService(IOrderValidator validator,
        IDiscountStrategyFactory discountStrategyFactory,
        IPaymentProvider paymentProvider,
        INotificationService notificationService,
        IOrderRepository orderRepository)
    {
        _validator = validator;
        _discountStrategyFactory = discountStrategyFactory;
        _paymentProvider = paymentProvider;
        _notificationService = notificationService;
        _orderRepository = orderRepository;
    }

    private readonly IOrderValidator _validator;
    private readonly IDiscountStrategyFactory _discountStrategyFactory;
    private readonly IPaymentProvider _paymentProvider;
    private readonly INotificationService _notificationService;
    private readonly IOrderRepository _orderRepository;

    public void ProcessOrder(Order order)
    {
        _validator.Validate(order);

        var discountStrategy = _discountStrategyFactory.Create(order.DiscountCode);

        order.Total = discountStrategy.ApplyDiscount(order.Total);

        if (!_paymentProvider.ProcessPayment(order.PaymentMethod, order.Total))
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
