using OrderFlow.Domain;

namespace OrderFlow.Test.xUnit;

internal static class OrderTestData
{
    public static Order CreateOrder(decimal unitPrice = 100m, string discountCode = "") =>
        new(
            1,
            "customer@example.com",
            "CARD",
            discountCode,
            [new OrderLine("Sample product", unitPrice, 1)]);
}
