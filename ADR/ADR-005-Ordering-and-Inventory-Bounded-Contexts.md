# ADR-005: Ordering and Inventory bounded contexts in a modular monolith

- Status: Accepted
- Decision date: 24 Nov 2026
- Deciders: Prem Mahto and Codex

## Context

Phase 2 requires OrderFlow to model Ordering and Inventory as bounded contexts, communicate through an in-memory `OrderPlaced` event, and demonstrate an orchestration Saga for placing an order, reserving stock, and charging payment. OrderFlow is a learning application with no demonstrated need for independent deployment or durable messaging.

Ordering records the customer's purchase, including the SKU, product name, unit price, and quantity. Inventory owns available and reserved quantities by SKU. The contexts need an integration contract that does not expose either context's domain entities to the other.

## Decision

- Keep Ordering in the existing `OrderFlow.Domain` project and model Inventory in the separate `OrderFlow.Inventory` project.
- Keep both contexts in one process as modules of a modular monolith; do not add an Inventory service, network protocol, broker, or separate database in this exercise.
- Define `OrderPlaced` in Application's integration contract with the order ID and SKU/quantity lines only.
- Let Application's `OrderPlacementSaga` handle that event, call the Application-owned Inventory reservation port, then invoke the selected payment strategy.
- Implement the event bus and in-memory Inventory adapter in Infrastructure.
- Reserve all lines atomically. If reservation fails, cancel the pending order; no Inventory release is needed because no reservation was made. If payment fails after reservation, release the reservation recorded for that order and cancel it.
- Use immediate payment failure for this pass. Defer a bounded retry period and reservation expiry to a later exercise.

## Consequences

### Positive

- Ordering and Inventory can use different models and language without a shared `Product` domain object.
- The event contract exposes only the information Inventory needs, keeping purchase price and product display data inside Ordering.
- Project references make the Inventory domain boundary visible while retaining the existing layered application structure.
- The Saga demonstrates compensation without requiring a message broker or real payment provider.
- Atomic all-line reservation avoids partially reducing stock when any requested SKU is unavailable.

### Costs and limits

- The Inventory project adds build and navigation overhead to a small application.
- The synchronous in-memory event bus is not durable and cannot survive process failure; it does not demonstrate production messaging guarantees.
- The in-memory repository and reservation adapter do not coordinate a real distributed transaction. Compensation is application logic, not a database rollback.
- Payment-provider ambiguity, concurrent duplicate Saga execution, retries, reservation expiry, shipping, and payment refunds are not fully modeled.

## Alternatives considered

- **Keep Ordering and Inventory in one domain model:** simpler now, but would hide differences in ownership and terminology and make it easy to share an overly broad Product type.
- **Deploy Inventory as a microservice:** clearer operational isolation, but would introduce network failures, durable delivery, independent data management, and deployment overhead without a current need.
- **Query available quantity and decrement later:** rejected because the result can become stale before the decrement. The reservation operation is the authoritative availability decision.

## Revisit when

- Inventory needs independent deployment, scaling, data ownership, or release cadence.
- In-memory event loss or synchronous coupling becomes a demonstrated problem.
- The payment retry policy or shipping workflow needs to extend reservation state and compensation behavior.
