using OrderFlow.Domain;

namespace OrderFlow.Application;

public interface IOrderObserver
{
    void OnOrderProcessed(Order order);
}
