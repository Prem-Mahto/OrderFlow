# OrderFlow Backlog

This file tracks intentionally deferred work by phase and pass. Items belong here when they are postponed or identified as useful but are not part of the active learning task. Update status when an item is resumed or completed.

## Status key

- **Backlogged** — intentionally deferred; not part of current work.
- **Ready** — can be picked up when its prerequisites are met.
- **Done** — completed; retain briefly for traceability, then move the item to project history or remove it.

## Phase 2 — Architectural Styles, DDD, and CQRS

### Pass 2 — Architectural styles and dependency direction

#### Task 6 — Revisit dependency direction in implementation

- **Status:** Backlogged
- **Reason deferred:** The current project references and source already establish inward compile-time dependencies. Application owns the ports it needs; Infrastructure implements them. There is no Application-to-Infrastructure dependency to reverse, so no code change is needed for this task.
- **When to resume:** If a later change introduces an Application dependency on an Infrastructure type, or if the learner wants a focused exercise comparing a deliberately outward dependency with the current port-owned-by-Application design.
- **Possible learning work when resumed:** Document the current dependency direction with evidence from project references and namespaces; optionally create and revert a small isolated example to demonstrate the cost of an outward dependency. Avoid adding lasting abstraction solely to make this task appear complete.
- **Acceptance criteria:** The learner can explain the current compile-time dependency graph, why Application-owned ports point dependencies inward, and the maintenance cost of abstractions that do not protect a meaningful boundary.

### Pass 3 — Tactical DDD: Order aggregate

- **Status:** Done
- **Completed:** `Order` is an aggregate root containing validated `OrderLine` entities; totals are derived and discounting and lifecycle transitions are controlled by the aggregate. Application coordinates payment and cancels the order after a declined payment.
- **Deferred:** User-initiated cancellation remains out of scope and is documented in the README for a later exercise.
- **Documentation follow-up:** Refresh the existing Archify Phase 2 overview after these working-tree changes are committed, so its repository evidence can point to a verifiable source revision.

### Pass 4 — Bounded contexts, integration event, and Saga

- **Status:** Done
- **Completed:** Ordering remains in `OrderFlow.Domain`; Inventory has its own `OrderFlow.Inventory` domain project. A synchronous in-memory `OrderPlaced` message carries order ID and SKU/quantity lines. The Application Saga reserves all lines atomically, charges payment, cancels the order when reservation fails, and releases the reservation if payment fails.
- **Trade-off:** These are modules in one process, not independently deployed services. The in-memory event bus is synchronous and not durable.

### Later Phase 2 — CQRS, Event Sourcing, and monolith decisions

- **Status:** Not started
- **Note:** Evaluate these concepts against concrete OrderFlow needs; do not add them without a demonstrated reason.

#### Future follow-up — Payment retry window and inventory reservation expiry

- **Status:** Backlogged
- **Reason deferred:** The current Saga exercise will use the simpler policy: payment failure immediately fails/cancels the order and releases its successful inventory reservation.
- **Future behavior to explore:** Allow a bounded payment retry period while inventory remains reserved; if the order is not paid before the policy deadline, fail the order and release the reservation.
- **Acceptance criteria when resumed:** Define the retry window and allowed attempts, model the order and reservation states needed for the policy, release inventory only when the reservation expires or another valid compensation occurs, and test the time-dependent rules deterministically without real delays.

#### Architecture diagram evidence refresh

- **Status:** Backlogged
- **Reason deferred:** The existing Pass 1 overview and dependency diagram predate the Inventory project and Saga. Refresh both after the current implementation is committed so the overview and project graph accurately cite the Ordering and Inventory contexts and current references.
