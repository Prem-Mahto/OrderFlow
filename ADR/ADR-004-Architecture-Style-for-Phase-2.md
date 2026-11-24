# ADR-004: Architecture style and dependency direction for Phase 2

- Status: Accepted
- Decision date: 24 Nov 2026
- Deciders: Prem Mahto and Codex

## Context

Phase 2 requires explicit Presentation, Application, Domain, and Infrastructure boundaries. The learner chose to start with Layered Architecture because that organization is familiar. The resulting project references already point inward: Application references Domain; Infrastructure references Application and Domain; Presentation references all three; Domain references no outer project.

The architecture styles overlap but emphasize different concerns. Layered Architecture groups responsibilities. Clean Architecture emphasizes inward source dependencies. Onion Architecture places the domain at the center. Hexagonal Architecture describes the core through ports and adapters.

## Decision

Use a layered project organization to make responsibilities visible, and keep the current inward project-reference direction as the dependency rule. Application owns the ports required by its workflows; Infrastructure implements those ports. Domain remains independent of Application, Infrastructure, and Presentation. Presentation is the composition root.

Describe the current design as a layered monolith with inward dependencies and ports/adapters at its technical boundaries. Do not describe the projects as independently deployable services.

## Consequences

### Positive

- Project references make the dependency direction visible to the compiler.
- Domain policy is insulated from persistence, payment, notification, and console implementations.
- Infrastructure can implement Application-owned contracts without Application referencing Infrastructure.
- The familiar layered layout provides a practical starting point for comparing the other styles.

### Costs and limits

- More projects add build and navigation overhead compared with the original single project.
- Presentation references multiple projects because the composition root must instantiate concrete implementations and create the current `Order` example.
- The current `Order` model is still mutable; the project boundary does not itself enforce aggregate invariants.
- This remains a single-process application. The projects do not provide deployment, data, or failure isolation.

## Alternatives considered

- **Traditional top-down layered references:** simpler to explain initially, but would let Application compile against Infrastructure. Rejected as the lasting rule because the existing application ports provide a small, concrete way to preserve inward dependencies.
- **Treat Clean, Onion, and Hexagonal as mutually exclusive choices:** rejected because each emphasizes a different view of boundaries and the current design can use their compatible ideas without adding more projects.
- **Separate microservices:** rejected because the current exercise has no independent deployment, scaling, or operational need that justifies network and distributed-consistency costs.

## Revisit when

- A later domain or integration requirement shows that the current project boundaries cause friction.
- A bounded context demonstrates a concrete need for independent deployment or data ownership.
- The solution grows enough that the current four project responsibilities no longer provide clear cohesion.
