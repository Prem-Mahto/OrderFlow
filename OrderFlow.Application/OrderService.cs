using OrderFlow.Domain;

namespace OrderFlow.Application;

public class OrderService
{
    public OrderService(IDiscountStrategyFactory discountStrategyFactory,
        IPaymentStrategyFactory paymentStrategyFactory,
        IOrderRepository orderRepository,
        IApplicationLogger logger)
    {
        _discountStrategyFactory = discountStrategyFactory;
        _paymentStrategyFactory = paymentStrategyFactory;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    private readonly IDiscountStrategyFactory _discountStrategyFactory;
    private readonly IPaymentStrategyFactory _paymentStrategyFactory;
    private readonly IOrderRepository _orderRepository;
    private readonly IApplicationLogger _logger;
    private List<IOrderObserver> _observers = new List<IOrderObserver>();

    public void ProcessOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        var discountStrategy = _discountStrategyFactory.Create(order.DiscountCode);
        order.ApplyDiscount(discountStrategy);

        var paymentStrategy = _paymentStrategyFactory.Create(order.PaymentMethod);
        if (!paymentStrategy.ProcessPayment(order.Total))
        {
            order.Cancel();
            throw new InvalidOperationException("Payment processing failed.");
        }

        order.MarkPaid();

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
