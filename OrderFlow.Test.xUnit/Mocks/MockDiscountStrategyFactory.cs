namespace OrderFlow.Test.xUnit.Mocks;


public class MockDiscountStrategy : IDiscountStrategy
{
    public int CallCount { get; private set; }
    public decimal ApplyDiscount(decimal orderTotal)
    {
        CallCount++;
        return orderTotal;
    }
}

public class MockDiscountStrategyFactory : IDiscountStrategyFactory
{
    public string DiscountCode { get; set; } = string.Empty;
    public MockDiscountStrategy? LastCreatedStrategy { get; private set; }
    public IDiscountStrategy Create(string discountCode)
    {
        DiscountCode = discountCode;
        LastCreatedStrategy = new MockDiscountStrategy();
        return LastCreatedStrategy;
    }
}
