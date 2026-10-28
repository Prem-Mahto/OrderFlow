namespace OrderFlow.Discount;

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
