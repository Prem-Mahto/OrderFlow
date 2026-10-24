namespace OrderFlow.Discount;

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

