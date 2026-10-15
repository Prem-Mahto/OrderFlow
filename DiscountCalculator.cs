public class DiscountCalculator
{
    public decimal ApplyDiscount(string discountCode, decimal orderTotal)
    {
        return discountCode switch
        {
            "SAVE10" => orderTotal * 0.90m,
            "SAVE20" => orderTotal * 0.80m,
            "VIP" => orderTotal * 0.70m,
            _ => orderTotal
        };
    }
}
