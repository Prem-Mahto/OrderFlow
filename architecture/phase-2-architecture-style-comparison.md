# Phase 2 — Architecture style comparison for OrderFlow

## Why compare these styles?

Phase 1 separated responsibilities among classes. Phase 2 adds project boundaries so the compiler can help prevent changes in infrastructure from spreading into business policy. These architecture styles overlap, but they emphasize different ways to organize and protect those boundaries.

## Comparison

| Style | Main organizing idea | Typical dependency rule | What it helps us notice in OrderFlow |
| --- | --- | --- | --- |
| Layered | Group code by responsibility, such as presentation, application/business logic, and data access. | Often top-down: presentation calls business logic, which calls data access. A layer can use dependency inversion, but the style name alone does not guarantee it. | Separating the console entry point, order workflow, business concepts, and technical implementations. |
| Clean Architecture | Keep policy toward the center and technical details at the edges. | Source dependencies point inward. The application defines ports; infrastructure implements them. | `OrderService` should not need a compile-time reference to the repository or payment adapter. |
| Onion Architecture | Put the domain model at the center, surrounded by domain/application services and then infrastructure. | Outer rings depend on inner rings; the domain has no dependency on outer rings. | The `Order` model and its rules should remain independent from persistence and console code. |
| Hexagonal Architecture (Ports and Adapters) | Describe the application core by the interactions it exposes and needs. | The core owns ports; adapters at the boundary implement or call those ports. | Payment, persistence, notification, and console integrations are replaceable adapters around application-owned contracts. |

Clean, Onion, and Hexagonal Architecture are related approaches, not perfectly interchangeable names. Clean emphasizes the dependency rule, Onion emphasizes the domain-centered rings, and Hexagonal emphasizes ports and adapters. Layered Architecture describes a useful responsibility layout; it can be implemented with either top-down references or an inward dependency rule.

## Current OrderFlow structure

OrderFlow uses four .NET projects and remains one console process:

```text
Presentation → Application, Domain, Infrastructure
Infrastructure → Application, Domain
Application → Domain
Domain → no outer project
Tests → Application, Domain, Infrastructure (test-time only)
```

This is a **layered project organization with inward compile-time dependencies**. The dependency direction follows the central rule in Clean and Onion Architecture. Application-owned interfaces act as ports, and Infrastructure implements them in a way that also uses Domain types. The runtime composition root in Presentation references the concrete projects so it can wire them together.

The styles overlap here by design: the folder/project layout teaches layers first; inward references protect policy; ports and adapters explain how the technical edges connect. This does not make OrderFlow four services. All projects still run together in one process and deploy as one application.

## Current limits

Pass 3 has made `Order` an aggregate root with validated `OrderLine` entities, read-only line access, computed subtotal, and controlled lifecycle transitions. Application selects a discount policy and coordinates payment, persistence, and notifications; the aggregate applies the selected domain policy and protects its own valid state. This is a tactical DDD step, not a claim that the model is a complete domain model. The architecture decision does not require CQRS, Event Sourcing, a database, or a message broker.
