# OrderFlow v1

OrderFlow is a C#/.NET architecture-learning project. It began as a deliberately messy order-processing console application and was refactored in small, explainable steps during Phase 1.

## Phase 1 status

Phase 1 implementation is complete. It covers Advanced OOP, SOLID, and selected GoF patterns. The learner is completing the final review before beginning Phase 2.

The original `OrderService` God class validated orders, applied discounts, selected and processed payments, sent notifications, and simulated persistence. It now coordinates a workflow through focused collaborators:

- `OrderValidator` validates an incoming order.
- Discount strategies implement discount rules; `DiscountStrategyFactory` maps a discount code to the required strategy.
- Payment strategies implement payment-method behavior; `PaymentStrategyFactory` maps a payment method to the required strategy.
- `IPaymentGateway` separates payment strategies from external charging; `ConsolePaymentGateway` is the current safe implementation.
- `IOrderRepository` abstracts persistence; `InMemoryOrderRepository` is the current implementation.
- `IOrderObserver` receives a successful order-processing event.
- `OrderConfirmationObserver` sends the confirmation notification.
- `LoggingOrderDecorator` adds logging around notification delivery.
- `LegacyEmailNotificationAdapter` adapts the existing email client to the notification boundary.

`OrderService` receives its core collaborators through constructor injection. `Program.cs` is the composition root where concrete implementations are selected and wired together.

## Patterns and trade-offs

| Pattern or principle | Use in OrderFlow |
| --- | --- |
| SRP | Validation, discounting, payment, persistence, and notification have focused responsibilities. |
| DIP and constructor injection | `OrderService` depends on interfaces, keeping workflow policy separate from concrete implementations. |
| Strategy | Discount and payment algorithms vary independently behind interfaces. |
| Factory | Factories select the strategy for a discount code or payment method. A new mapping still requires a focused factory change. |
| Observer | Registered observers are notified only after a successful order is persisted. |
| Decorator | Logging is added around notification delivery without changing the wrapped observer. |
| Adapter | The legacy email client is made usable through the current notification boundary. |
| Repository | The current in-memory repository provides an explicit persistence boundary. |

`UnitOfWorkPracticeService` is an isolated practice exercise. A Unit of Work is not used in the main flow because the current in-memory repository has no real transaction boundary. See ADR-001 for the reasoning.

## Tests

Focused automated tests live in `OrderFlow.Test.xUnit`. They use xUnit and deterministic fakes instead of real payment providers, databases, networks, or email services.

Run the application:

```powershell
dotnet run --project .\OrderFlow\OrderFlow.csproj
```

Run the tests:

```powershell
dotnet test .\OrderFlow.Test.xUnit\OrderFlow.Test.xUnit.csproj
```

Prerequisite: .NET 9 SDK.

## Architecture documentation

- [Phase 1 architecture diagram](orderflow-architecture-Phase-1.html)
- [Architecture Decision Records](OrderFlow/ADR/)

The diagram represents the current Phase 1 code. Update it after material architecture changes rather than using it as a proposed future design.

## Learning sequence

1. Completed: extract responsibilities using SRP.
2. Completed: apply constructor injection and dependency inversion.
3. Completed: use Strategy and Factory for discount rules.
4. Completed: use Strategy and Factory for payment methods, with a payment gateway seam.
5. Completed: introduce Observer for order lifecycle notifications.
6. Completed: apply Decorator and Adapter to the notification requirement.
7. Completed: explore Repository and Unit of Work trade-offs.
8. Completed: migrate focused tests to xUnit.
9. Completed: document the architecture with ADRs and a code-backed diagram.
10. Next: complete the Phase 1 final review, then begin Phase 2 when requested.

## Curriculum context

The broader curriculum and long-term career plan are included in this repository:

- `csharp-to-software-architect-curriculum.md`
- `career-upskilling-master-plan-optimized.md`

This is a learning system, not a production payment platform. Payment, persistence, and notification integrations are intentionally simulated so the architecture can evolve safely and incrementally.
