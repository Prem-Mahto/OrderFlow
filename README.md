# OrderFlow v1

OrderFlow is a C#/.NET learning project that evolves from a deliberately messy order-processing console application into a portfolio system for software-architecture practice.

The project follows an incremental refactoring approach: each change addresses a concrete design problem before introducing another pattern or architectural layer.

## Current checkpoint

Phase 1: Advanced OOP, SOLID, and design patterns.

The first refactoring pass has separated the original `OrderService` God class into focused collaborators:

- `OrderValidator` validates an incoming order.
- `DiscountCalculator` applies the current discount rules.
- `PaymentProvider` processes the selected payment method.
- `OrderRepository` persists the order.
- `NotificationService` notifies the customer.
- `OrderService` coordinates the order-processing workflow.

The next refactoring pass is dependency injection and dependency inversion. The current implementation intentionally still constructs its collaborators inside `OrderService`, making that coupling visible before it is addressed.

## Run locally

Prerequisite: .NET 9 SDK.

```powershell
dotnet run
```

## Learning sequence

1. Extract responsibilities using SRP. *(Current checkpoint)*
2. Apply constructor injection and dependency inversion.
3. Replace payment and discount branching with Strategy.
4. Introduce Factory, Observer, Decorator, Adapter, Repository, and Unit of Work only where the design warrants them.
5. Add focused automated tests and review the resulting architecture.

The broader curriculum and long-term career plan are included in this repository:

- `csharp-to-software-architect-curriculum.md`
- `career-upskilling-master-plan-optimized.md`

## Notes

This is a learning system, not a production payment platform. Payment, persistence, and notification actions currently write to the console so that the design can evolve safely in small, understandable steps.
