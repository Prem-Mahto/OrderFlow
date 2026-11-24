# OrderFlow

OrderFlow is a C#/.NET architecture-learning project. Phase 1 covered SOLID, object-oriented design, selected patterns, deterministic tests, ADRs, and a current-state diagram. Phase 2 is now in progress; it introduced layered projects, an Ordering aggregate, and a separate Inventory bounded-context project while keeping the application in one process.

## Current project boundaries

| Project | Responsibility |
| --- | --- |
| `OrderFlow.Presentation` | Console entry point and composition root. |
| `OrderFlow.Application` | Order-processing workflow, payment strategies, application-owned ports, and the isolated Unit of Work practice. |
| `OrderFlow.Domain` | Ordering context: the `Order` aggregate and its `OrderLine` entities, order lifecycle invariants, and discount policies. |
| `OrderFlow.Inventory` | Inventory context: stock by SKU and reservation invariants. It is a library in the same process, not a separate service. |
| `OrderFlow.Infrastructure` | In-memory order persistence, event bus, Inventory reservation adapter, console payment gateway, notification adapters, and console logging. |
| `OrderFlow.Test.xUnit` | Deterministic tests; references application, Ordering, Inventory, and infrastructure projects directly. |

The direct project references are:

```text
Presentation → Application, Domain, Infrastructure, Inventory
Infrastructure → Application, Domain, Inventory
Application → Domain
Domain (Ordering) → no outer project
Inventory → no outer project
Tests → Application, Domain, Inventory, Infrastructure (test-time only)
```

The Presentation composition root wires the concrete Infrastructure implementations. Application owns the interfaces for persistence, payment, observers, logging, Inventory reservation, and the in-memory event bus. Infrastructure implements them and translates the `OrderPlaced` message into Inventory operations. `OrderFlow.Domain` contains the Ordering model; `OrderFlow.Inventory` owns stock by SKU. The contexts exchange an order ID and SKU/quantity lines, not each other's domain objects.

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
- [Ordering and Inventory context map and Saga](architecture/phase-2-bounded-contexts-and-saga.md)
- [Phase 1 architecture diagram](architecture/orderflow-architecture-Phase-1.html)
- [Architecture Decision Records](ADR/)

The Pass 1 HTML overview and dependency diagram reflect an earlier four-layer state and need refresh. The Markdown context/Saga document contains the current project-reference graph and in-process flow. Refresh the HTML diagrams after the current source changes are committed so their evidence points to a verifiable revision.

## Phase 2 progress

- Pass 1: separate Presentation, Application, Domain, and Infrastructure projects; direct dependency direction is inward.
- Pass 2: compare Layered, Clean, Onion, and Hexagonal Architecture and record the chosen organization and dependency rule in ADR-004.
- Pass 3: model `Order` as an aggregate root containing `OrderLine` entities, with order invariants enforced by the aggregate. Application selects the discount policy and coordinates payment; the aggregate applies the selected policy and owns the resulting total. A declined payment cancels the pending order through the aggregate operation. The lifecycle supports `Pending`, `Paid`, `Cancelled`, `Shipped`, and `Delivered` states.
- User-initiated cancellation is intentionally out of scope; revisit it in a later exercise. The aggregate's `Cancel` operation currently handles payment failure while the order is pending. Cancelling a paid order would require payment compensation.
- The old external `OrderValidator` was removed because its email and positive-total checks now belong to aggregate construction, and total is derived from validated lines.
- Pass 4: Ordering remains in `OrderFlow.Domain`; Inventory is a separate `OrderFlow.Inventory` domain project. The synchronous in-memory `OrderPlaced` event carries order ID and SKU/quantity only. An Application Saga reserves all requested stock, charges payment, and compensates by cancelling on reservation failure or releasing stock and cancelling on payment failure.
- The event bus and both contexts still run in one process. The in-memory event is not durable. A bounded payment retry window and reservation expiry are deferred in [the backlog](BACKLOG.md).
- Next: evaluate CQRS, Event Sourcing, and monolith options through current OrderFlow needs.
- CQRS and Event Sourcing are decision topics; implement them only if OrderFlow demonstrates a need.

The broader learning plans are `csharp-to-software-architect-curriculum.md` and `career-upskilling-master-plan-optimized.md`. Payment, persistence, and notification integrations remain simulated; no real credentials, providers, or cloud services are required.
