public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal orderTotal);
}

public sealed class NoDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal orderTotal) => orderTotal;
}

public sealed class Save10DiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal orderTotal) => orderTotal * 0.90m;
}

public sealed class Save20DiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal orderTotal) => orderTotal * 0.80m;
}

public sealed class VipDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal orderTotal) => orderTotal * 0.70m;
}

public interface IDiscountStrategyFactory
{
    IDiscountStrategy Create(string discountCode);
}

public sealed class DiscountStrategyFactory : IDiscountStrategyFactory
{
    public IDiscountStrategy Create(string discountCode)
    {
        return discountCode switch
        {
            "SAVE10" => new Save10DiscountStrategy(),
            "SAVE20" => new Save20DiscountStrategy(),
            "VIP" => new VipDiscountStrategy(),
            _ => new NoDiscountStrategy()
        };
    }
}
