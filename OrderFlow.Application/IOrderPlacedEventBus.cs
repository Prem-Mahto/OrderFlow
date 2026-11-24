namespace OrderFlow.Application;

public interface IOrderPlacedEventHandler
{
    void Handle(OrderPlaced orderPlaced);
}

public interface IOrderPlacedEventBus
{
    void Subscribe(IOrderPlacedEventHandler handler);

    void Publish(OrderPlaced orderPlaced);
}
