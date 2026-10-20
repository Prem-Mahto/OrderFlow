using OrderFlow.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Test;

internal class InMemoryOrderRepositoryTests
{
    public static void ProcessedOrderRetrievedById_Test()
    {
        // Arrange
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerEmail = "customer@example.com",
            Total = 100m,
            PaymentMethod = "CARD",
        };

        var validator = new MockOrderValidator();
       
        var discountFactory = new MockDiscountStrategyFactory();
        var paymentFactory = new MockPaymentFactory();
        var repository = new InMemoryOrderRepository();
        var service = new OrderService(validator,
            discountFactory,
            paymentFactory,
            repository);

        // Act
        service.ProcessOrder(order);

        // Assert
        TestAssert.Equal(1, validator.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, discountFactory.LastCreatedStrategy?.CallCount, "The order should be validated once.");
        TestAssert.Equal(1, paymentFactory.LastCreatedStrategy?.CallCount, "The payment should be processed once.");

        var retrievedOrder = repository.GetById(order.Id);
        TestAssert.Same(order, retrievedOrder, "The retrieved order should be the same as the processed order.");
    }
}
