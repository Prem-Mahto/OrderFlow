# OrderFlow v1

OrderFlow is a C#/.NET learning project that starts with a deliberately simple order-processing console application and evolves through small architecture and design exercises.

## Current checkpoint

**Phase 0: Mindset & Self-Assessment** (`Phase-0-initial-code` branch).

This phase focuses on architecture as decisions and trade-offs, quality attributes, and stakeholder needs. The hands-on exercise is to write a retroactive Architecture Decision Record (ADR) for a real decision, using **Context → Options Considered → Decision → Consequences**. See Phase 0 in `csharp-to-software-architect-curriculum.md` for the prompts.

## Starting application

The current code is the initial baseline for later refactoring:

- `Program.Main` creates a sample `Order` and calls `OrderService`.
- `OrderService` currently performs validation, applies discount rules, selects a payment method, prints a notification, and prints a SQL-like persistence statement in one method.
- Payment, notification, and persistence are console messages. The project does not connect to a payment provider, email service, or database.

This intentionally concentrated design gives the later Single Responsibility Principle exercise a concrete starting point.

## Run locally

Prerequisite: .NET 9 SDK.

```powershell
dotnet run
```

## Learning progression

1. **Phase 0:** Practice architectural trade-off thinking and complete a retroactive ADR.
2. **Phase 1:** Extract order-processing responsibilities using the Single Responsibility Principle.
3. Apply constructor injection and Dependency Inversion Principle to `OrderService`.
4. Use Strategy for payment-method and discount-rule variation.
5. Introduce other patterns, tests, and architecture documentation as the exercises call for them.

The broader curriculum and long-term career plan are included here:

- `csharp-to-software-architect-curriculum.md`
- `career-upskilling-master-plan-optimized.md`

## Notes

This is a learning system, not a production payment platform. Its current payment, notification, and persistence behavior is simulated with console output.
