using OrderFlow.Domain;

namespace OrderFlow.Application;

public class OrderService
{
    public OrderService(IDiscountStrategyFactory discountStrategyFactory,
        IOrderRepository orderRepository,
        IOrderPlacedEventBus orderPlacedEventBus,
        IApplicationLogger logger)
    {
        _discountStrategyFactory = discountStrategyFactory;
        _orderRepository = orderRepository;
        _orderPlacedEventBus = orderPlacedEventBus;
        _logger = logger;
    }

    private readonly IDiscountStrategyFactory _discountStrategyFactory;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderPlacedEventBus _orderPlacedEventBus;
    private readonly IApplicationLogger _logger;
    private List<IOrderObserver> _observers = new List<IOrderObserver>();

    public void PlaceOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        var discountStrategy = _discountStrategyFactory.Create(order.DiscountCode);
        order.ApplyDiscount(discountStrategy);

        _orderRepository.Add(order);

        var orderPlaced = new OrderPlaced(
            order.Id,
            order.OrderLines.Select(line => new OrderPlacedLine(line.Sku, line.Quantity)));
        _orderPlacedEventBus.Publish(orderPlaced);

        if (order.State != OrderState.Paid)
        {
            throw new InvalidOperationException("The order-placement workflow did not complete payment.");
        }

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
