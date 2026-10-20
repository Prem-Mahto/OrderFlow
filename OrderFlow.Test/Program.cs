namespace OrderFlow.Test;

internal class Program
{
    static void Main(string[] args)
    {
        OrderServiceTests.SAVE10_Test();
        Console.WriteLine("PASS: SAVE10 order processing");
        OrderServiceTests.VIPDiscountStrategy_Test();
        Console.WriteLine("PASS: VIP discount strategy");
        DiscountStrategyFactoryTests.Create_WithSave10Code_ReturnsStrategyThatAppliesTenPercentDiscount();
        Console.WriteLine("PASS: SAVE10 discount strategy factory");
        PaymentStrategyTests.CardPaymentStrategy_ChargesCardThroughGateway();
        Console.WriteLine("PASS: card payment strategy");
        PaymentStrategyTests.PaymentStrategyFactory_CreateWithCard_ReturnsConfiguredCardStrategy();
        Console.WriteLine("PASS: CARD payment strategy factory");
        NotificationObserverTests.FailedNotification_test();
        Console.WriteLine("PASS: failed notification observer does not break the process");
        LoggingDecoratorTests.LoggingDecorator_test();
        Console.WriteLine("PASS: logging decorator test");
        LegacyEmailNotificationAdapterTests.AdapterCallsLegacyEmailClient_test();
        Console.WriteLine("PASS: legacy email notification adapter calls legacy email client");
        InMemoryOrderRepositoryTests.ProcessedOrderRetrievedById_Test();
        Console.WriteLine("PASS: in-memory order repository retrieves processed order by ID");

        UnitOfWorkPracticeTests.SuccessfulOrder_Test();
        Console.WriteLine("PASS: Unit of work successful order");

        UnitOfWorkPracticeTests.InsufficientStock_Test();
        Console.WriteLine("PASS: Unit of work rollback for insufficient stock");
    }
}
