ADR-001: Should OrderFlow use Unit of Work
- Status: Rejected
- Decision Date: 19 Oct 2026
- Deciders: Prem Mahto and Codex

Context
- the OrderFlow system is using a in-memory store for persistence.
- No real database transaction.
- One order persistence operation is performed at a time. Very simple and straightforward.
- Only one repository - OrderRepository is present.
- No multiple repository writes that must succeed or fail together.

Decision
- Unit of Work pattern is not needed for the OrderFlow system at this time.
- Repository pattern is sufficient for the current requirements of the OrderFlow system.

Consequences
- Positive
  - Simplicity: The system remains simple and easy to understand without the added complexity of implementing a Unit of Work pattern.
  - Maintainability: The system is easier to maintain and modify without the additional complexity of managing a Unit of Work pattern.
- Negative
  - Limited Scalability: when we switch to a real database and have multiple repositories, we may need to implement a Unit of Work pattern to manage transactions across multiple repositories.
  - Increased Complexity in Managing Transactions: As the system grows, managing transactions across multiple repositories without a Unit of Work pattern may become increasingly complex and error-prone.

Assumptions & Revisit Triggers
- Assumptions
    - The OrderFlow system will remain simple
    - The OrderFlow system will not require multiple repository writes that must succeed or fail together.
- Revisit When
    - The OrderFlow system requires multiple repository writes that must succeed or fail together.
    - The OrderFlow system is switched to a real database.
