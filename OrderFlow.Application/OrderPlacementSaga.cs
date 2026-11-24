using OrderFlow.Domain;

namespace OrderFlow.Application;

/// <summary>
/// Coordinates the in-process order placement workflow across Ordering and Inventory.
/// </summary>
public sealed class OrderPlacementSaga : IOrderPlacedEventHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryReservationService _inventoryReservationService;
    private readonly IPaymentStrategyFactory _paymentStrategyFactory;

    public OrderPlacementSaga(
        IOrderRepository orderRepository,
        IInventoryReservationService inventoryReservationService,
        IPaymentStrategyFactory paymentStrategyFactory)
    {
        ArgumentNullException.ThrowIfNull(orderRepository);
        ArgumentNullException.ThrowIfNull(inventoryReservationService);
        ArgumentNullException.ThrowIfNull(paymentStrategyFactory);

        _orderRepository = orderRepository;
        _inventoryReservationService = inventoryReservationService;
        _paymentStrategyFactory = paymentStrategyFactory;
    }

    public void Handle(OrderPlaced orderPlaced)
    {
        ArgumentNullException.ThrowIfNull(orderPlaced);

        var order = _orderRepository.GetById(orderPlaced.OrderId)
            ?? throw new InvalidOperationException($"Order {orderPlaced.OrderId} was not found.");

        // A repeated in-memory event must not reserve or charge an already processed order again.
        if (order.State != OrderState.Pending)
        {
            return;
        }

        if (!_inventoryReservationService.TryReserve(order.Id, orderPlaced.Lines))
        {
            order.Cancel();
            _orderRepository.Update(order);
            throw new InvalidOperationException("Inventory reservation failed; the order was cancelled.");
        }

        bool paymentSucceeded;
        try
        {
            var paymentStrategy = _paymentStrategyFactory.Create(order.PaymentMethod);
            paymentSucceeded = paymentStrategy.ProcessPayment(order.Total);
        }
        catch
        {
            CompensatePaymentFailure(order);
            throw;
        }

        if (!paymentSucceeded)
        {
            CompensatePaymentFailure(order);
            throw new InvalidOperationException("Payment processing failed; inventory was released and the order was cancelled.");
        }

        order.MarkPaid();
        _orderRepository.Update(order);
    }

    private void CompensatePaymentFailure(Order order)
    {
        _inventoryReservationService.Release(order.Id);
        order.Cancel();
        _orderRepository.Update(order);
    }
}
