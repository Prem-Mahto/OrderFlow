# From C# Developer to Software Architect
### A 14-Month Curriculum (10 YOE C#/.NET → Software Architect)

**Starting point:** 10 years C#/.NET, strong web app experience, solid SQL, 1 year Vue.js 2 (MVC), some jQuery/JS, basic Azure.
**Destination:** Ready to operate as a Software/Solutions Architect — able to design distributed systems, defend trade-offs, and lead technical decisions.

---

## How to Use This Document

This plan is self-contained and portable — you can hand any single phase to **any AI assistant** (Claude, ChatGPT, etc.) and ask it to mentor you through it. A good way to start a phase:

> "Act as a senior software architect and mentor. Teach me the concepts in this phase using C#/.NET and Azure examples, help me scaffold the hands-on project described, then quiz me with the theory and practical questions before I move on."

**Assumptions:** ~8–10 hours/week (a few weekday evenings + one weekend block), since you have a full-time job. If your real weekly capacity is different, **scale the number of weeks per phase up or down proportionally — don't skip the order.** Total: ~58 weeks / ~13–14 months / ~500–580 hours.

**One running project, growing in complexity:** Most phases evolve the *same* system — nicknamed **"OrderFlow"** (an order/inventory e-commerce backend) — from a single messy console app into a documented, secured, observable, AI-augmented, cloud-deployed distributed system by the capstone. This mirrors how real architecture skill is built: not by juggling disconnected toy apps, but by watching one system's design get harder to defend as requirements grow, and continuously improving your justification. A couple of phases (system-design drills, mock interviews) intentionally use *different* systems, since real architecture interviews test breadth across unfamiliar problems too.

---

## Roadmap at a Glance

| # | Phase | Duration | Builds toward |
|---|-------|----------|----------------|
| 0 | Mindset & Self-Assessment | 1 wk | Thinking like an architect |
| 1 | Advanced OOP, SOLID & Design Patterns | 4 wks | Clean, extensible code |
| 2 | Architectural Styles & DDD/CQRS | 6 wks | Structuring systems, not just classes |
| 3 | System Design Fundamentals | 6 wks | Scale, availability, data |
| 4 | Containers & Kubernetes | 4 wks | Running distributed systems |
| 5 | Cloud Architecture on Azure | 7 wks | Real cloud deployment |
| 6 | APIs, Integration & Messaging | 3 wks | Service-to-service contracts |
| 7 | Security & Identity | 3 wks | Trustworthy systems |
| 8 | DevOps, IaC & Observability | 4 wks | Operable systems |
| 9 | AI, Agents & MCP for Architects | 5 wks | AI-augmented architecture |
| 10 | Frontend & Full-Stack Awareness | 2 wks | Whole-stack fluency |
| 11 | Documentation & Communication | 3 wks | Influencing decisions |
| 12 | Capstone Project | 6 wks | Portfolio proof |
| 13 | Certification & Job-Search Prep | 4 wks | Landing the role |

---

## Phase 0 — Mindset & Self-Assessment
**Duration:** 1 week (~4 hrs)
**Why it matters:** Architecture is a different job, not "senior developer with extra meetings." You need to recognize that shift before the technical content will stick.

**Topics**
- What architects actually do day-to-day (solution vs. enterprise vs. technical/software architect)
- Architecture as a set of *decisions with trade-offs*, not a diagram
- Quality attributes ("-ilities"): scalability, availability, maintainability, security, performance, testability, cost

**Key Concepts**
- Trade-off thinking (every decision helps one attribute and hurts another)
- Stakeholder-driven design vs. developer-driven design
- "There is no best architecture, only the right trade-off for this context"

**Hands-on Practice**
- **Exercise: Retroactive ADR.** Pick one real decision you made on a past project (a library choice, a schema decision, anything). Write it up as a formal Architecture Decision Record (Context → Options Considered → Decision → Consequences). Goal: practice separating *what you decided* from *why*, which is the core architect habit.

**Theory Questions**
1. What's the practical difference between a senior developer and a software architect?
2. Name five quality attributes and give a realistic example where two of them conflict.
3. Why is "it depends" often the correct start of an architectural answer?

**Practical Questions**
1. Write the retroactive ADR described above (aim for half a page).
2. List three decisions on a recent project that were made *without* being written down — what risk did that create?

---

## Phase 1 — Advanced OOP, SOLID & Design Patterns
**Duration:** 4 weeks (~9 hrs/week)
**Why it matters:** You can't design systems if the building blocks (classes, modules) are tangled. This is the last "pure coding" phase before things zoom out.

**Topics**
- SOLID principles in depth
- Gang-of-Four patterns: Strategy, Factory, Observer, Decorator, Adapter, Repository, Unit of Work
- Composition over inheritance
- Code smells, refactoring technique
- Dependency Injection vs. Dependency Inversion (they're not the same thing)

**Key Concepts**
- Coupling vs. cohesion
- Interfaces as contracts/seams for testability
- Why "clever" code and "maintainable" code are often opposites

**Hands-on Project — "OrderFlow v1" (console/class library, C#)**
Start deliberately messy, then refactor in stages:
1. Build a small order-processing app: create an order, apply a discount, process payment, send a notification, persist the order — all crammed into one `OrderService` God class with `if/else` chains.
2. Refactor in passes: extract interfaces, apply Strategy for discount rules, Factory for payment providers, Observer for notifications, Repository + Unit of Work for persistence.
3. Add unit tests as you refactor — a class that's hard to test is your signal something still needs refactoring.
*You're learning:* how SOLID violations physically feel in code, and how each GoF pattern is really just SOLID applied to a specific shape of problem.

**Theory Questions**
1. Explain each SOLID letter with a concrete C# example.
2. What problem does Dependency Injection solve that Dependency Inversion doesn't (and vice versa)?
3. When does Composition over inheritance actually hurt (i.e., when is inheritance still fine)?
4. What's the difference between a code smell and a bug?

**Practical Questions**
1. Take a 100-line God class (ask your AI assistant to generate a suitably bad one) and refactor it, explaining each change.
2. Implement a shipping-cost calculator using the Strategy pattern with at least 3 strategies.
3. Write a unit test that would fail before your refactor and pass after — what did that prove?

---

## Phase 2 — Architectural Styles, DDD & CQRS
**Duration:** 6 weeks (~9 hrs/week)
**Why it matters:** This is the actual jump from "developer" to "architect" thinking — structuring a *system*, not a class.

**Topics**
- Layered/N-tier vs. Clean/Onion/Hexagonal Architecture
- Domain-Driven Design: bounded contexts, ubiquitous language, aggregates, entities, value objects
- CQRS (Command Query Responsibility Segregation) and when it's overkill
- Event Sourcing basics
- Monolith vs. Modular Monolith vs. Microservices — honest trade-offs
- Saga pattern for cross-service transactions (choreography vs. orchestration)

**Key Concepts**
- Bounded context as the unit of decomposition (not "one service per table")
- Eventual consistency and why distributed systems can't just use one big transaction
- Command/query separation as a design lens, independent of whether you use full CQRS

**Hands-on Project — "OrderFlow v2"**
1. Restructure v1 into Clean Architecture: Domain, Application, Infrastructure, and API/Presentation layers, dependencies pointing inward.
2. Apply DDD tactically: model `Order` as an aggregate root, `OrderLine` as a value object, enforce invariants (e.g., can't ship a cancelled order) inside the aggregate, not in a service.
3. Split into two bounded contexts — **Ordering** and **Inventory** — as separate projects/services communicating via an in-memory event bus (e.g., "OrderPlaced" event decrements stock).
4. Implement a Saga (orchestration style) for "Place Order → Reserve Inventory → Charge Payment," including a compensating action if reservation fails.
*You're learning:* how to draw the lines between services in a way that reduces coupling instead of just relocating a monolith's mess into more processes.

**Theory Questions**
1. What's the real difference between layered architecture and Clean/Hexagonal Architecture?
2. Define "bounded context" and explain why it's not the same as "microservice."
3. When is CQRS worth the complexity, and when is it just ceremony?
4. Explain the Saga pattern with a non-trivial example (e.g., travel booking with flight + hotel).

**Practical Questions**
1. Sketch bounded contexts for a hypothetical e-commerce platform (Ordering, Inventory, Shipping, Payments, Catalog) — where are the boundaries and why?
2. Implement the orchestration-based Saga described above, including the compensating transaction.
3. Identify one place in your v1 code where an aggregate invariant was previously enforced in the wrong layer.

---

## Phase 3 — System Design Fundamentals
**Duration:** 6 weeks (~9 hrs/week)
**Why it matters:** This is the "whiteboard interview" skillset — and the reasoning behind almost every cloud/infra decision later in the plan.

**Topics**
- Vertical vs. horizontal scaling; load balancing
- Caching: client-side, CDN, Redis, cache-aside vs. write-through
- Database scaling: replication, sharding/partitioning
- CAP theorem and consistency models (strong vs. eventual)
- Message queues & pub/sub fundamentals
- Rate limiting, idempotency, back-pressure
- Circuit breaker & bulkhead patterns
- Choosing relational vs. NoSQL (document/key-value/graph) for a given access pattern

**Key Concepts**
- Latency vs. throughput
- Availability expressed as "nines" and what an SLA actually commits to
- Capacity estimation ("back-of-the-envelope" math): requests/sec, storage growth, read:write ratio

**Hands-on Project — "URL Shortener at Scale" (separate from OrderFlow)**
Design and partially build a URL shortener with these requirements: 10M writes/day, sub-100ms reads, high availability.
1. Do the capacity-estimation math first (storage/year, peak QPS).
2. Produce a high-level architecture diagram + data model + caching strategy.
3. Implement the core read/write path in ASP.NET Core + Redis (cache) + SQL or Cosmos DB (store).
4. Separately, implement a token-bucket **rate-limiting middleware** for an API in C#.
*You're learning:* to reason about scale with numbers instead of vibes, and to translate that reasoning into a concrete storage + caching design.

**Theory Questions**
1. Explain CAP theorem using a real system as the example (not the textbook diagram).
2. When would you deliberately choose eventual consistency over strong consistency?
3. What does the Circuit Breaker pattern protect against, and what does it *not* fix?
4. Cache-aside vs. write-through — when does each make sense?

**Practical Questions**
1. Do capacity estimation for a chat app supporting 1M concurrent users — messages/sec, storage/month.
2. Implement cache-aside with Redis in .NET for a read-heavy endpoint.
3. Implement and load-test your rate limiter — what happens at 2x the limit?

---

## Phase 4 — Containers & Kubernetes
**Duration:** 4 weeks (~8 hrs/week)
**Why it matters:** Distributed systems need a runtime story. This is the "how does it actually run" phase before you touch cloud-specific services.

**Topics**
- Docker fundamentals: images, containers, Dockerfile, multi-stage builds
- Docker Compose for local multi-service dev
- Kubernetes core objects: Pods, Deployments, Services, ConfigMaps/Secrets, Ingress
- Helm basics
- Azure Kubernetes Service (AKS) overview

**Key Concepts**
- Container vs. VM
- Image layering and why multi-stage builds shrink image size
- Self-healing, scaling, and service discovery as Kubernetes' core value proposition
- When Kubernetes is overkill (small teams, few services) vs. justified

**Hands-on Project — "OrderFlow v3"**
1. Write multi-stage Dockerfiles for the Ordering and Inventory services from Phase 2.
2. Run them together via Docker Compose, including Redis and SQL containers, with environment-based config.
3. Deploy the same set to a local cluster (Minikube/Kind) or AKS using Deployments + Services, packaged as a Helm chart.
*You're learning:* the gap between "runs on my machine" and "runs reliably as independently-deployable services."

**Theory Questions**
1. What's the actual difference between Docker and Kubernetes (they solve different problems)?
2. Deployment vs. Service in Kubernetes — what does each manage?
3. Give a real scenario where you would advise *against* Kubernetes.

**Practical Questions**
1. Write a multi-stage Dockerfile for a .NET 8 API that minimizes final image size.
2. Write a Helm chart (or plain manifests) deploying both services with ConfigMap-driven configuration.
3. Kill a pod manually and observe Kubernetes' self-healing — what happened and how fast?

---

## Phase 5 — Cloud Architecture on Azure
**Duration:** 7 weeks (~9 hrs/week)
**Why it matters:** This is where "basic Azure experience" becomes real cloud architecture judgment — the part of the job you'll be doing weekly.

**Topics**
- Compute options: App Service, AKS, Azure Functions (serverless), Container Apps — trade-offs
- Networking basics: VNet, NSG, Application Gateway, Front Door
- Data: Azure SQL, Cosmos DB, Blob Storage
- Messaging: Service Bus, Event Grid, Event Hubs — when to use which
- Azure API Management
- Azure Well-Architected Framework (5 pillars)
- Multi-region/DR basics, autoscaling, availability zones

**Key Concepts**
- PaaS vs. IaaS vs. serverless trade-offs (cost, control, operational burden)
- Event-driven serverless patterns
- Reading and applying the Well-Architected Framework as a review checklist, not a slogan

**Hands-on Project — "OrderFlow v4"**
1. Deploy Ordering + Inventory to Azure (App Service or Container Apps).
2. Move data to Azure SQL/Cosmos DB.
3. Replace the in-memory event bus from Phase 2 with **Azure Service Bus** (topics/subscriptions).
4. Put Azure API Management in front of the public API.
5. Wire in Application Insights.
6. Write a Well-Architected Framework self-review of your design across all 5 pillars, listing at least one honest weakness per pillar.
*You're learning:* how architectural decisions map onto real, billable, operable Azure services — and how to audit your own design against a standard framework.

**Theory Questions**
1. When would you choose Azure Functions over App Service or Container Apps?
2. Service Bus vs. Event Grid vs. Event Hubs — what is each actually built for?
3. Name the 5 pillars of the Well-Architected Framework and one question each pillar forces you to ask.

**Practical Questions**
1. Deploy a .NET API to Azure App Service through a pipeline (manual click-deploy first is fine, but note it — you'll automate it in Phase 8).
2. Set up a Service Bus topic/subscription and wire Ordering → Inventory through it.
3. Estimate monthly cost with the Azure Pricing Calculator, then find one change that meaningfully lowers cost without hurting availability.

---

## Phase 6 — APIs, Integration & Messaging
**Duration:** 3 weeks (~8 hrs/week)
**Why it matters:** Every distributed system lives or dies on its contracts between services.

**Topics**
- REST API design (versioning, pagination, HATEOAS-lite)
- GraphQL and gRPC — what problems each actually solves
- Sync vs. async communication trade-offs
- API Gateway pattern; Backend-for-Frontend (BFF)
- Contract-first design with OpenAPI
- Outbox pattern; dead-letter queues; event schema versioning

**Key Concepts**
- Idempotent APIs (why retries need them)
- Reliable event publishing without losing messages on failure
- Backward-compatible API evolution

**Hands-on Project — "OrderFlow v5"**
1. Add a gRPC endpoint for the internal, latency-sensitive Ordering→Inventory stock check.
2. Write an OpenAPI spec first, then implement the public REST Ordering API from it.
3. Implement the **Transactional Outbox** pattern so "Order created" events are never lost even if the message broker call fails mid-transaction.
*You're learning:* the difference between "it works in the happy path" and "it survives partial failure," which is most of what separates architecture from feature coding.

**Theory Questions**
1. gRPC vs. REST vs. GraphQL — when is each the right call?
2. What specific failure does the Outbox pattern prevent?
3. How do you version a public API without breaking existing clients?

**Practical Questions**
1. Write the OpenAPI spec for your Ordering API.
2. Implement the Outbox pattern with a background dispatcher.
3. Simulate a Service Bus outage mid-order — does an event get lost? Fix it if so.

---

## Phase 7 — Security & Identity
**Duration:** 3 weeks (~8 hrs/week)
**Why it matters:** An architect who can't reason about security by design is a liability, not an asset.

**Topics**
- Authentication vs. authorization
- OAuth2 & OpenID Connect flows; JWTs
- Azure Entra ID (Azure AD) for service-to-service and user auth
- Secrets management (Azure Key Vault)
- OWASP Top 10 awareness
- Zero Trust principles; defense in depth; least privilege

**Key Concepts**
- Claims-based identity
- Token validation (signature, audience, expiry)
- Why "it's behind our firewall" is not a security architecture

**Hands-on Project — "OrderFlow v6"**
1. Secure the Ordering API with Entra ID: client-credentials flow for service-to-service, auth-code flow for a client app.
2. Move all secrets/connection strings into Azure Key Vault.
3. Run an OWASP Top 10 pass against your own API and fix at least 3 real findings.
*You're learning:* to treat security as an architectural property that's designed in, not a checklist bolted on at the end.

**Theory Questions**
1. OAuth2 vs. OpenID Connect — what does each actually add?
2. What's inside a JWT, and what exactly must you validate before trusting it?
3. What does "Zero Trust" mean in practice for a service-to-service call?

**Practical Questions**
1. Implement JWT bearer auth in ASP.NET Core.
2. Integrate Key Vault so no secret exists in source control or plain config.
3. Document the 3 OWASP issues you found and how you fixed each.

---

## Phase 8 — DevOps, IaC & Observability
**Duration:** 4 weeks (~8 hrs/week)
**Why it matters:** An architecture that can't be safely deployed, monitored, and rolled back isn't finished — this is the "operability" pillar made concrete.

**Topics**
- CI/CD pipelines (Azure DevOps or GitHub Actions)
- Infrastructure as Code (Bicep or Terraform)
- Blue-green vs. canary deployments
- Structured logging; distributed tracing (OpenTelemetry)
- Monitoring/alerting (Application Insights; Grafana/Prometheus concepts)
- SRE basics: SLIs, SLOs, error budgets

**Key Concepts**
- Pipeline-as-code and immutable infrastructure
- Observability = logs + metrics + traces, not just "check the dashboard"
- Golden signals (latency, traffic, errors, saturation)

**Hands-on Project — "OrderFlow v7"**
1. Write a CI/CD pipeline that builds, tests, and deploys Ordering on every push.
2. Provision the Azure resources from Phase 5 (App Service/Container App, Service Bus, SQL) via Bicep or Terraform instead of the portal.
3. Add OpenTelemetry tracing across Ordering → Inventory so a single request's full path is visible in Application Insights.
4. Set an alert that fires when the error rate crosses a threshold.
*You're learning:* to make your own architecture observable and safely changeable — the property that determines whether a system survives 2am incidents.

**Theory Questions**
1. Blue-green vs. canary deployment — what risk does each reduce, and what does each cost?
2. Define SLI, SLO, and error budget, and how they relate.
3. Why prefer IaC over manual provisioning, beyond "it's faster the second time"?

**Practical Questions**
1. Write the Bicep/Terraform template for your Azure resources.
2. Trigger a deploy through your pipeline and verify a rollback path exists.
3. Trace one request end-to-end across both services in Application Insights.

---

## Phase 9 — AI, Agents & MCP for Architects
**Duration:** 5 weeks (~8 hrs/week)
**Why it matters:** Increasingly, "does this system need an AI feature, and how do we integrate it safely" is an architectural question you'll be asked to own.

**Topics**
- LLM fundamentals at the black-box level: tokens, context windows, hallucination, non-determinism
- Prompt engineering basics
- Retrieval-Augmented Generation (RAG): embeddings, vector stores, chunking
- AI agent architecture: planning loops, tool-calling, when an agent beats a plain API call
- Model Context Protocol (MCP): what it is (a standard way for AI models/clients to discover and call tools/data sources exposed by a server) and why it exists
- Integrating LLMs via Azure OpenAI Service
- Responsible AI for architects: cost control, latency budgets, data privacy, prompt-injection risk

**Key Concepts**
- Semantic search vs. keyword search
- Where the "trust boundary" sits when an LLM calls into your system (or your system calls an LLM)
- Architectural risks unique to AI features: runaway cost, unpredictable latency, hallucinated output reaching users

**Hands-on Project — "OrderFlow v8"**
1. Build a small **RAG-based support assistant**: embed your order/product data, store embeddings in a vector store, retrieve relevant context per question, and call an LLM (e.g., Azure OpenAI) to answer customer questions about their orders.
2. Build a minimal **MCP server** that exposes 2–3 read-only tools from OrderFlow (e.g., "get order status," "list recent orders") so any MCP-compatible AI client can call into your system safely.
3. Document the architecture: where the trust boundary is, what data can leave the system, and what happens if the LLM call times out or returns garbage.
*You're learning:* to treat an AI feature as just another dependency with a latency/cost/failure profile — not magic, and not to be feared either.

**Theory Questions**
1. What is RAG, and why use it instead of just fine-tuning a model on your data?
2. What problem does MCP solve when connecting an AI model to external systems?
3. What are the main architectural risks of putting an LLM call into a production request path?

**Practical Questions**
1. Build the RAG pipeline against a small dataset and verify it doesn't answer from data it wasn't given.
2. Build and test the MCP server exposing a read-only OrderFlow tool.
3. Write an ADR: "Should feature X use an AI agent or a deterministic API?" for a scenario you invent.

---

## Phase 10 — Frontend & Full-Stack Architecture Awareness
**Duration:** 2 weeks (~6 hrs/week)
**Why it matters:** You don't need to be a frontend expert, but an architect who can't reason about the frontend's constraints designs bad APIs.

**Topics**
- SPA vs. SSR vs. SSG trade-offs
- Vue 3 Composition API (vs. your Vue 2 experience); brief React comparison for context
- Micro-frontends
- Backend-for-Frontend (BFF) pattern
- State management approaches; Vite build tooling basics

**Key Concepts**
- Rendering trade-offs (SEO, time-to-interactive, server cost)
- How backend architecture choices constrain or enable frontend teams

**Hands-on Project**
1. Rebuild one page of an existing Vue 2 app in Vue 3 Composition API, calling OrderFlow through a BFF layer you design.
2. Write a short recommendation on whether micro-frontends make sense for a hypothetical 5-team org — with a clear "no, unless X" answer.
*You're learning:* enough frontend fluency to design APIs and contracts that don't fight the client team.

**Theory Questions**
1. SSR vs. CSR — what does each trade off, and for which product is each usually right?
2. What's a BFF, and when is the extra layer worth it?
3. What actually changed between Vue 2's Options API and Vue 3's Composition API?

**Practical Questions**
1. Convert one Vue 2 component to Vue 3 Composition API.
2. Sketch a BFF serving both a web and mobile client from one backend.

---

## Phase 11 — Architecture Documentation, Communication & Leadership
**Duration:** 3 weeks (~8 hrs/week)
**Why it matters:** An undocumented, uncommunicated architecture doesn't exist as far as the rest of the org is concerned. This is the "soft skill" that makes or breaks the job.

**Topics**
- C4 Model (Context, Container, Component, Code diagrams)
- ADRs in depth (you've been writing lightweight ones since Phase 0 — now formalize the practice)
- UML essentials, used sparingly (sequence, component diagrams)
- Running/participating in architecture review boards
- Communicating trade-offs to non-technical stakeholders
- Influencing without authority; mentoring developers

**Key Concepts**
- Diagrams as communication tools, not art
- Documentation as a decision trail future-you (and future teammates) will thank you for
- Tailoring the same architecture explanation for engineers vs. executives

**Hands-on Project**
1. Produce a full C4 model (Context, Container, and one Component diagram) for the entire OrderFlow system as it stands (Mermaid, diagrams.net, or Structurizr all work).
2. Assemble every ADR you've written across the previous phases into one folder.
3. Write a 1-page architecture proposal memo as if presenting to a VP of Engineering, and a separate 5-slide version for a business stakeholder audience.
*You're learning:* to compress the same system into different altitudes of explanation on demand.

**Theory Questions**
1. What are the 4 levels of the C4 model, and what question does each answer?
2. Why keep ADRs instead of a general wiki page?
3. What changes (and what doesn't) when you explain the same architecture to an executive instead of an engineer?

**Practical Questions**
1. Produce the C4 diagrams described above.
2. Write 3 fresh ADRs for real decisions made in your capstone (next phase).

---

## Phase 12 — Capstone Project
**Duration:** 6 weeks (~10 hrs/week)
**Why it matters:** This is the portfolio piece you'll actually show in interviews and use to argue for the title.

**Requirements** (evolve OrderFlow into a broader system, e.g., a full event-ticketing or e-commerce platform):
- Multiple bounded contexts/services
- At least one async, event-driven flow
- Containerized and deployed to Azure (AKS or Container Apps)
- CI/CD pipeline with IaC
- Secured with Entra ID
- Observability wired in (logs, metrics, traces)
- One AI-powered feature (RAG or agent) with an MCP integration point
- Full C4 diagrams + ADR folder

**Deliverable:** a GitHub repo, the architecture documentation, and a recorded 10-minute walkthrough — practice presenting it as you would in a real architecture review.

**Theory Questions (self-review)**
1. Can you justify every major decision with an explicit trade-off, not just "it seemed right"?
2. Where are the single points of failure, and how would the design change at 10x scale?

**Practical Questions**
1. Ask a peer or an AI assistant to play devil's-advocate reviewer and challenge 5 of your decisions. Can you defend each, or does the design need to change?

---

## Phase 13 — Certification & Job-Search Prep
**Duration:** 4 weeks (~8 hrs/week)
**Why it matters:** Turning demonstrated skill into an actual title change.

**Topics**
- Azure Solutions Architect Expert (AZ-305) exam prep — natural fit given your Azure grounding
- Light awareness of enterprise frameworks (TOGAF) — know it exists, don't over-invest unless the target role asks for it
- Building an architecture portfolio from your capstone
- Mock system-design interviews
- Repositioning resume/LinkedIn from "developer" to "architect"

**Hands-on Practice**
- Sit 3 timed (45-minute) mock system-design interviews on *different* systems than OrderFlow — e.g., "design a social feed," "design a payments system," "design a notification system" — with an AI or peer acting as interviewer.
- Critique your own answers against a rubric: functional requirements, non-functional requirements, high-level design, one deep-dive, trade-offs discussed.
- Sit the AZ-305 exam.

**Theory/Practical Questions**
1. For each mock interview, what would you change if told "now design it for 10x the scale"?
2. What's the one architectural decision from your capstone you're proudest of defending — and the one you're least sure about?

---

## After This Plan

Architecture skill compounds mostly through repetition on real systems and exposure to other people's decisions. Once you finish this curriculum, the highest-leverage habits are: keep writing ADRs for real decisions at work (not just practice ones), read the architecture write-ups of systems you admire, and seek out (or create) opportunities to review other people's designs — reviewing is often where architectural judgment sharpens fastest.
