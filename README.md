# OrderFlow

OrderFlow is a C#/.NET architecture-learning project. Phase 1 covered SOLID, object-oriented design, selected patterns, deterministic tests, ADRs, and a current-state diagram. Phase 2 is now in progress; the first pass introduced separate projects for the system layers.

## Current project boundaries

| Project | Responsibility |
| --- | --- |
| `OrderFlow.Presentation` | Console entry point and composition root. |
| `OrderFlow.Application` | Order-processing workflow, payment strategies, application-owned ports, and the isolated Unit of Work practice. |
| `OrderFlow.Domain` | The `Order` aggregate and its `OrderLine` entities, order lifecycle invariants, and discount policies. |
| `OrderFlow.Infrastructure` | In-memory persistence, console payment gateway, notification adapters, and console logging. |
| `OrderFlow.Test.xUnit` | Deterministic tests; references the application, domain, and infrastructure projects directly. |

The direct project references are:

```text
Presentation → Application, Domain, Infrastructure
Infrastructure → Application, Domain
Application → Domain
Domain → no outer project
Tests → Application, Domain, Infrastructure (test-time only)
```

The Presentation composition root wires the concrete Infrastructure implementations. Application owns the interfaces for persistence, payment, observers, and application logging. Infrastructure implements those interfaces. This is the current dependency direction; future architecture comparisons may refine it.

## Run and verify

Run the console application:

```powershell
dotnet run --project .\OrderFlow.Presentation\OrderFlow.Presentation.csproj
```

Build the Presentation application:

```powershell
dotnet build .\OrderFlow.Presentation\OrderFlow.Presentation.csproj
```

Build the full solution:

```powershell
dotnet build .\OrderFlow.slnx
```

Run the tests:

```powershell
dotnet test .\OrderFlow.Test.xUnit\OrderFlow.Test.xUnit.csproj
```

Projects target .NET 10. The repository's `global.json` selects Microsoft.Testing.Platform for `dotnet test`.

## Architecture documentation

- [Phase backlog](BACKLOG.md)
- [Phase 2 architecture overview](architecture/Phase%202%20pass%201%20architecture%20diagram.html)
- [Phase 2 dependency direction](architecture/Phase%202-%20pass%201%20dependency%20direction.html)
- [Phase 2 architecture style comparison](architecture/phase-2-architecture-style-comparison.md)
- [Order aggregate lifecycle](architecture/order-aggregate-lifecycle.md)
- [Phase 1 architecture diagram](architecture/orderflow-architecture-Phase-1.html)
- [Architecture Decision Records](ADR/)

The Phase 2 overview illustrates the main order-processing components. The dependency-direction diagram shows direct project references. Both should be updated when the implementation changes materially.

## Phase 2 progress

- Pass 1: separate Presentation, Application, Domain, and Infrastructure projects; direct dependency direction is inward.
- Pass 2: compare Layered, Clean, Onion, and Hexagonal Architecture and record the chosen organization and dependency rule in ADR-004.
- Pass 3: model `Order` as an aggregate root containing `OrderLine` entities, with order invariants enforced by the aggregate. Application selects the discount policy and coordinates payment; the aggregate applies the selected policy and owns the resulting total. A declined payment cancels the pending order through the aggregate operation. The lifecycle supports `Pending`, `Paid`, `Cancelled`, `Shipped`, and `Delivered` states.
- User-initiated cancellation is intentionally out of scope; revisit it in a later exercise. The aggregate's `Cancel` operation currently handles payment failure while the order is pending. Cancelling a paid order would require payment compensation.
- The old external `OrderValidator` was removed because its email and positive-total checks now belong to aggregate construction, and total is derived from validated lines.
- Next: define the Ordering and Inventory bounded contexts, then introduce an in-memory `OrderPlaced` event between them.
- CQRS and Event Sourcing are decision topics; implement them only if OrderFlow demonstrates a need.

The broader learning plans are `csharp-to-software-architect-curriculum.md` and `career-upskilling-master-plan-optimized.md`. Payment, persistence, and notification integrations remain simulated; no real credentials, providers, or cloud services are required.
