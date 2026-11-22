using OrderFlow.Domain;

namespace OrderFlow.Application;

public class OrderService
{
    public OrderService(IOrderValidator validator,
        IDiscountStrategyFactory discountStrategyFactory,
        IPaymentStrategyFactory paymentStrategyFactory,
        IOrderRepository orderRepository,
        IApplicationLogger logger)
    {
        _validator = validator;
        _discountStrategyFactory = discountStrategyFactory;
        _paymentStrategyFactory = paymentStrategyFactory;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    private readonly IOrderValidator _validator;
    private readonly IDiscountStrategyFactory _discountStrategyFactory;
    private readonly IPaymentStrategyFactory _paymentStrategyFactory;
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicationLogger _logger;
    private List<IOrderObserver> _observers = new List<IOrderObserver>();

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

        _orderRepository.Add(order);

        NotifyObservers(order);

    }

    public void RegisterObserver(IOrderObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IOrderObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers(Order order)
    {
        foreach (var observer in _observers)
        {
            try
            {
                observer.OnOrderProcessed(order);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Observer {observer.GetType().Name} failed to process order: {ex.Message}");
            }
        }
    }

}
