using OrderFlow.Application;

namespace OrderFlow.Infrastructure;

public sealed class InMemoryOrderPlacedEventBus : IOrderPlacedEventBus
{
    private readonly List<IOrderPlacedEventHandler> _handlers = [];

    public void Subscribe(IOrderPlacedEventHandler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        if (!_handlers.Contains(handler))
        {
            _handlers.Add(handler);
        }
    }

    public void Publish(OrderPlaced orderPlaced)
    {
        ArgumentNullException.ThrowIfNull(orderPlaced);
        foreach (var handler in _handlers.ToArray())
        {
            handler.Handle(orderPlaced);
        }
    }
}
