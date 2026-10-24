namespace OrderFlow.Persistence;

internal class PracticeOrder
{
    internal int Id { get; set; }
    internal int ItemId { get; set; }
    internal string Name { get; set; } = string.Empty;
    internal decimal Price { get; set; }
    internal int Quantity { get; set; }
}

internal interface IPracticeInventoryRepository
{
    int GetStock(int itemId);
    void ReserveStock(int itemId, int quantity);
    void ReleaseStock(int itemId, int quantity);
}
internal class PracticeInventoryRepository : IPracticeInventoryRepository
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

internal interface IPracticeOrderRepository
{
    void Add(PracticeOrder order);
    void Remove(PracticeOrder order);
    PracticeOrder? GetById(int orderId);
}
internal class PracticeOrderRepository : IPracticeOrderRepository
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

internal class UnitOfWork
{
    internal UnitOfWork(IPracticeInventoryRepository inventoryRepository, IPracticeOrderRepository orderRepository)
    {
        InventoryRepository = inventoryRepository;
        OrderRepository = orderRepository;
    }
    internal IPracticeInventoryRepository InventoryRepository { get; }
    internal IPracticeOrderRepository OrderRepository { get; }

    internal bool SaveOrder(PracticeOrder order)
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

internal class PracticeOrderService
{
    private readonly UnitOfWork _unitOfWork;
    internal PracticeOrderService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    internal bool ProcessOrder(PracticeOrder order)
    {
        return _unitOfWork.SaveOrder(order);
    }
}
