namespace OrderFlow.Practice;

public class PracticeOrder
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public interface IPracticeInventoryRepository
{
    int GetStock(int itemId);
    void ReserveStock(int itemId, int quantity);
    void ReleaseStock(int itemId, int quantity);
}
public class PracticeInventoryRepository : IPracticeInventoryRepository
{
    private readonly Dictionary<int, int> _inventory = new Dictionary<int, int>() { { 1, 10 }, { 2, 5 }, { 3, 0 }, { 4, 1 } };

    public int GetStock(int itemId)
    {
        return _inventory.TryGetValue(itemId, out var stock) ? stock : 0;
    }

    public void ReserveStock(int itemId, int quantity)
    {
        if (!_inventory.ContainsKey(itemId))
        {
            throw new InvalidOperationException("Item not found in inventory.");
        }
        if (_inventory[itemId] < quantity)
        {
            throw new InvalidOperationException("Insufficient stock.");
        }
        _inventory[itemId] -= quantity;
    }
    public void ReleaseStock(int itemId, int quantity)
    {
        _inventory[itemId] += quantity;
    }
}

public interface IPracticeOrderRepository
{
    void Add(PracticeOrder order);
    public void Remove(PracticeOrder order);
    public PracticeOrder? GetById(int orderId);
}
public class PracticeOrderRepository : IPracticeOrderRepository
{
    private readonly List<PracticeOrder> _orders = new List<PracticeOrder>();
    public void Add(PracticeOrder order)
    {
        _orders.Add(order);
    }
    public void Remove(PracticeOrder order)
    {
        _orders.Remove(order);
    }
    public PracticeOrder? GetById(int orderId)
    {
        return _orders.FirstOrDefault(o => o.Id == orderId);
    }
}

public class UnitOfWork
{
    public UnitOfWork(IPracticeInventoryRepository inventoryRepository, IPracticeOrderRepository orderRepository)
    {
        InventoryRepository = inventoryRepository;
        OrderRepository = orderRepository;
    }
    public IPracticeInventoryRepository InventoryRepository { get; }
    public IPracticeOrderRepository OrderRepository { get; }

    public bool SaveOrder(PracticeOrder order)
    {
        bool orderAdded = false;
        bool stockReserved = false;
        try
        {
            OrderRepository.Add(order);
            orderAdded = true;

            InventoryRepository.ReserveStock(order.ItemId, order.Quantity);
            stockReserved = true;

            return true;
        }

        catch
        {
            Console.WriteLine($"Order {order.Id} failed");
            if (orderAdded) { OrderRepository.Remove(order); }
            if (stockReserved) { InventoryRepository.ReleaseStock(order.ItemId, order.Quantity); }
            return false;
        }
    }
}

public class PracticeOrderService
{
    private readonly UnitOfWork _unitOfWork;
    public PracticeOrderService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public bool ProcessOrder(PracticeOrder order)
    {
        return _unitOfWork.SaveOrder(order);
    }
}



