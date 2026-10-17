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

The current collaborators are:

- `OrderValidator` validates an incoming order.
- `DiscountCalculator` applies the current discount rules.
- `PaymentProvider` processes the selected payment method.
- `OrderRepository` persists the order.
- `NotificationService` notifies the customer.
- `OrderService` coordinates the order-processing workflow.

The next refactoring pass is payment Strategy. `PaymentProvider` still branches on the payment-method code. Each payment algorithm should become its own strategy; a factory will then select the required payment strategy.

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
5. Next: Replace payment branching with Strategy and select payment strategies through a Factory.
6. Continue with Observer, Decorator, Adapter, Repository, and Unit of Work only where the design warrants them.
7. Expand automated tests and review the resulting architecture.

The broader curriculum and long-term career plan are included in this repository:

- `csharp-to-software-architect-curriculum.md`
- `career-upskilling-master-plan-optimized.md`

## Notes

This is a learning system, not a production payment platform. Payment, persistence, and notification actions currently write to the console so that the design can evolve safely in small, understandable steps.
