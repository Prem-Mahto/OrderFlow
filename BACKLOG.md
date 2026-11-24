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

- **Status:** Active
- **Note:** No backlog items recorded yet. Keep substantive aggregate design and implementation in the active learning work until explicitly deferred.

### Later Phase 2 passes

- **Status:** Not started
- **Note:** Record deferred tasks here under their specific pass when they arise. Planned learning includes Ordering and Inventory bounded contexts, an in-memory `OrderPlaced` event, an orchestration Saga with inventory-failure compensation, and evidence-based evaluation of CQRS, Event Sourcing, and monolith options.
