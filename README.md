# OrderFlow

OrderFlow is a C#/.NET architecture-learning project. Phase 1 covered SOLID, object-oriented design, selected patterns, deterministic tests, ADRs, and a current-state diagram. Phase 2 is now in progress; the first pass introduced separate projects for the system layers.

## Current project boundaries

| Project | Responsibility |
| --- | --- |
| `OrderFlow.Presentation` | Console entry point and composition root. |
| `OrderFlow.Application` | Order-processing workflow, payment strategies, application-owned ports, and the isolated Unit of Work practice. |
| `OrderFlow.Domain` | Order concepts, current order validation, and discount policies. Aggregate invariants are a later Phase 2 task. |
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

- [Phase 2 architecture overview](architecture/Phase%202%20pass%201%20architecture%20diagram.html)
- [Phase 2 dependency direction](architecture/Phase%202-%20pass%201%20dependency%20direction.html)
- [Phase 1 architecture diagram](architecture/orderflow-architecture-Phase-1.html)
- [Architecture Decision Records](OrderFlow/ADR/)

The Phase 2 overview illustrates the main order-processing components. The dependency-direction diagram shows direct project references. Both should be updated when the implementation changes materially.

## Phase 2 progress

- Pass 1: separate Presentation, Application, Domain, and Infrastructure projects; direct dependency direction is inward.
- Next: compare Layered, Clean, Onion, and Hexagonal Architecture, then continue with the aggregate, Ordering/Inventory contexts, and Saga exercises.
- CQRS and Event Sourcing are decision topics; implement them only if OrderFlow demonstrates a need.

The broader learning plans are `csharp-to-software-architect-curriculum.md` and `career-upskilling-master-plan-optimized.md`. Payment, persistence, and notification integrations remain simulated; no real credentials, providers, or cloud services are required.
