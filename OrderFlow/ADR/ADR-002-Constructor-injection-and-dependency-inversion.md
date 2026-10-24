ADR-002: Constructor injection and dependency inversion
- Status: Accepted
- Decision Date: 20 Oct 2026
- Deciders: Prem Mahto and Codex

Context
- The OrderService class is currently initialising all its dependencies directly within ProcessOrder method
- OrderService is tightly coupled to its dependencies
- Making it difficult to test and maintain order service
- Breaks the Single Responsibility Principle, as OrderService is responsible for both order processing and managing its dependencies.
- Breaks the Dependency Inversion Principle, as OrderService is dependent on concrete implementations of its dependencies rather than abstractions.

Decision
- OrderService will receive its dependencies through constructor injection
- Interfeces will be introduced for all dependencies to follow the Dependency Inversion Principle

Consequences
- Positive
  - Testability: OrderService can be easily tested by providing mock implementations of its dependencies.
  - Maintainability: OrderService is easier to maintain and modify without the additional complexity of managing its dependencies directly.
  - Flexibility: OrderService can work with any implementation of its dependencies, making it more flexible and adaptable to change.
- Negative
  - Increased Complexity: constructor injection may increase the complexity of the codebase
  - More Boilerplate Code: introducing interfaces and constructor injection may result in more boilerplate code, which can make the codebase harder to read and understand.
  - Learning Curve: Developers may need to learn about constructor injection and dependency inversion to understand the new design.
