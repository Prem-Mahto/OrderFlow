ADR-003: Strategy and Factory for discount and payment variation
- Status: Accepted
- Decision Date: 21 Oct 2026
- Deciders: Prem Mahto and Codex

Context
- The OrderService class is currently using if and else logic to determine which discount and payment strategy to use.
- Code is difficult to maintain and extend, as new strategies require modifying existing conditional logic.
- Making it difficult to test and maintain OrderService and Discount & Payment strategies, as they can't be unit tested in isolation.
- Breaks the Single Responsibility Principle, as OrderService is responsible for both order processing and strategy selection.
- Breaks the Open/Closed Principle, as adding new strategies requires modifying existing code.

Decision
- Introduce Strategy pattern for discount and payment variation.
- Introduce Factory pattern to create instances of discount and payment strategies based on the order's discount code and payment method.

Consequences
- Positive
  - Testability: Discount and Payment strategies can be easily tested in isolation, without the need to test OrderService.
  - Maintainability: OrderService is easier to maintain and modify without the additional complexity of managing strategy selection directly.
  - Flexibility: New discount and payment strategies can be added without modifying existing code, making it more flexible and adaptable to change.
  - Separation of Concerns: OrderService is now only responsible for order processing, while strategy selection is delegated to the Factory classes.
- Negative
  - Increased Complexity: Introducing Strategy and Factory patterns may increase the complexity of the codebase.
  - More Boilerplate Code: Introducing interfaces and Factory classes may result in more boilerplate code, which can make the codebase harder to read and understand.
  - Learning Curve: Developers may need to learn about Strategy and Factory patterns to understand the new design.
