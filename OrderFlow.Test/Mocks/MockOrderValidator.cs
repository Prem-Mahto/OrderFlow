using OrderFlow;


public class MockOrderValidator : IOrderValidator
{
    public int CallCount { get; private set; }

    public void Validate(Order order)
    {
        CallCount++;
    }
}
