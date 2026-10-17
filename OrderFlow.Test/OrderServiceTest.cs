using OrderFlow;

namespace OrderFlow.Test;

public class OrderServiceTests
{
    public static void VIPDiscountStrategy_Test()
    {
        //Arrange
        var orderTotal = 100m;
        var discountStrategy = new VipDiscountStrategy();

        //Act
        var discountedTotal = discountStrategy.ApplyDiscount(orderTotal);

        //Assert
        TestAssert.Equal(70m, discountedTotal, "VIP discount should reduce the order total by 30%.");
    }


    public static void SAVE10_Test()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerEmail = "customer@example.com",
            Total = 100m,
            PaymentMethod = "CARD",
            DiscountCode = "SAVE10"
        };

        var validator = new MockOrderValidator();
        var paymentProvider = new MockPaymentProvider();
        var notificationService = new MockNotificationService();
        var repository = new MockOrderRepository();

        var service = new OrderService(validator,           
            new DiscountStrategyFactory(),
            paymentProvider,
            notificationService,
            repository);

        // Act
        service.ProcessOrder(order);

        // Assert
        TestAssert.Equal(90m, order.Total, "SAVE10 should reduce the order total by 10%.");
        TestAssert.Equal(1, validator.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, paymentProvider.CallCount, "The payment should be processed once.");
        TestAssert.Equal("CARD", paymentProvider.LastPaymentMethod, "The selected payment method should be used.");
        TestAssert.Equal(90m, paymentProvider.LastOrderTotal, "Payment should use the discounted total.");
        TestAssert.Equal(1, repository.CallCount, "The order should be saved once.");
        TestAssert.Same(order, repository.LastSavedOrder, "The processed order should be saved.");
        TestAssert.Equal(1, notificationService.CallCount, "The customer should be notified once.");
        TestAssert.Equal(order.CustomerEmail, notificationService.LastCustomerEmail, "The notification should target the customer.");
    }
}
