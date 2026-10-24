# OrderFlow v1

OrderFlow is a C#/.NET learning project that evolves from a deliberately messy order-processing console application into a portfolio system for software-architecture practice.

The project follows an incremental refactoring approach: each change addresses a concrete design problem before introducing another pattern or architectural layer.

## Current checkpoint

Phase 1: Advanced OOP, SOLID, and design patterns.

Completed learning passes:

- SRP: the original `OrderService` God class was separated into focused collaborators.
- DI/DIP: `OrderService` receives its collaborators through constructor injection and depends on interfaces.
- Strategy: individual discount algorithms are represented by independent discount strategies and verified with a focused test.
- Factory: `DiscountStrategyFactory` selects the correct discount strategy from the order's discount code.
- Payment Strategy and Factory: payment algorithms are independent strategies selected by `PaymentStrategyFactory`.
- Payment gateway seam: strategies delegate to `IPaymentGateway`, keeping external payment calls replaceable and safe to test.

The current collaborators are:

- `OrderValidator` validates an incoming order.
- `DiscountStrategyFactory` selects a discount strategy.
- `PaymentStrategyFactory` selects a payment strategy.
- `ConsolePaymentGateway` simulates external payment processing.
- `OrderRepository` persists the order.
- `NotificationService` notifies the customer.
- `OrderService` coordinates the order-processing workflow.

The next refactoring pass is Observer. Order processing currently calls notification delivery directly; the notification concern should become a subscriber to an order lifecycle event.

## Run locally

Prerequisite: .NET 9 SDK.

```powershell
dotnet run
```

## Learning sequence

1. Completed: Extract responsibilities using SRP.
2. Completed: Apply constructor injection and dependency inversion.
3. Completed for discounts: Replace branching algorithms with Strategy.
4. Completed for discounts: Introduce Factory for selecting a discount strategy.
5. Completed: Replace payment branching with Strategy and select payment strategies through a Factory.
6. Completed: Introduce Observer for order lifecycle notifications.
7. Completed: Continue with Decorator, Adapter, Repository, and Unit of Work only where the design warrants them.
8. Completed: Expand automated tests using XUnit
9. review the resulting architecture.

The broader curriculum and long-term career plan are included in this repository:

- `csharp-to-software-architect-curriculum.md`
- `career-upskilling-master-plan-optimized.md`

## Notes

This is a learning system, not a production payment platform. Payment, persistence, and notification actions currently write to the console so that the design can evolve safely in small, understandable steps.
