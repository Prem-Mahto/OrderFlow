using OrderFlow.Domain;

namespace OrderFlow.Application;

public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(int orderId);
}
