using OrderFlow.Domain;

namespace OrderFlow.Application;

public interface IOrderRepository
{
    void Add(Order order);

    void Update(Order order);

    Order? GetById(int orderId);
}
