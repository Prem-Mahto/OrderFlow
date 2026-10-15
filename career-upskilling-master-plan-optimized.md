# Career & Upskilling Master Plan (Optimized)

**Direction:** C#/.NET Developer → Senior/Lead Engineer → Software/Solutions Architect, with Cloud, Distributed Systems, and AI/ML architecture as long-term specialisms.

**Plan length:** 14 phases, ~58 weeks (~13–14 months)
**Typical commitment:** 8–10 focused hours per week
**Primary project:** **OrderFlow**, an evolving order-management system that becomes the portfolio capstone.

This plan uses the *From C# Developer to Software Architect* curriculum's phase numbers and timing as the technical spine (that document has the topics, concepts, and hands-on exercises for each phase). This document is the operating layer around it: which employer-provided benefits to activate, when, on which device, and what habits keep the whole thing from collapsing under a full-time job.

**What changed from the first draft of this plan:** the capstone and job-search phases had been compressed to 2 and 1 week respectively to make room for extra time elsewhere. Given the stated goal — a deployed, documented, AI-augmented, interview-ready portfolio piece — that's too tight. Both are restored to their original length (6 and 4 weeks), pulled back from phases that had been padded beyond what they need.

---

## Starting point and target outcome

### Current strengths
- ~10 years of C#/.NET and web-application development experience
- Solid SQL skills
- JavaScript and ~1 year of Vue 2 experience
- Basic Azure exposure

### Growth gaps to close
- Owning architecture from the first design decision through deployment and operation
- Designing distributed and cloud systems independently
- Explaining architecture decisions, trade-offs, risks, and delivery plans clearly
- Building credible hands-on capability in cloud, DevOps, security, and AI/agent integration

### Definition of success
A public, personal-GitHub portfolio version of OrderFlow with: architecture documentation (C4 + ADRs), a working Azure deployment, CI/CD, observability, deliberate security decisions, one AI-augmented feature (RAG + an MCP integration point), and a defensible system-design story you can walk a technical interviewer through end to end.

---

## Learning philosophy

Every phase follows the same loop:
1. **Learn the core idea** — one selected course, doc, or targeted video.
2. **Apply it to OrderFlow immediately** — no long run-up of courses before building.
3. **Test understanding** — the theory/practical questions from the curriculum, or a design prompt.
4. **Review and improve** the implementation and the decisions behind it.
5. **Explain it** aloud or in writing, as if presenting to a tech lead or interviewer.

### Weekly rhythm (~8–10 hrs)
- **2–3 hrs:** focused study
- **4–5 hrs:** OrderFlow implementation or refactoring
- **1 hr:** quiz, review, decision-log entry
- **Optional 1 hr:** targeted video, only when a specific gap is blocking progress

---

## The roadmap

| Phase | Weeks | Focus | OrderFlow outcome |
|---|---|---|---|
| 0 | 1 | Mindset & self-assessment | Baseline, target competencies, first ADR habit |
| 1 | 2–5 | Advanced OOP, SOLID, design patterns | Refactor the messy v1 core using SOLID + GoF patterns |
| 2 | 6–11 | Architecture styles, DDD, CQRS | Clean Architecture layers, bounded contexts, a Saga |
| 3 | 12–17 | System-design fundamentals | Capacity math, caching, a scale-focused design review |
| 4 | 18–21 | Docker & Kubernetes | Containerize; deploy to a local cluster |
| 5 | 22–28 | Azure cloud architecture | Real Azure deployment: compute, data, messaging, APIM |
| 6 | 29–31 | APIs, integration, messaging | Contract-first APIs, gRPC, the Outbox pattern |
| 7 | 32–34 | Security & identity | Entra ID, Key Vault, an OWASP pass |
| 8 | 35–38 | DevOps, IaC, observability | CI/CD (GitHub Actions), Bicep/Terraform, tracing |
| 9 | 39–43 | AI, agents & MCP | RAG assistant + a minimal MCP server over OrderFlow |
| 10 | 44–45 | Frontend & full-stack awareness | Vue 3 page + a BFF layer |
| 11 | 46–48 | Documentation & communication | Full C4 model, ADR folder, stakeholder memo |
| 12 | 49–54 | Capstone | Harden, document, deploy, and record a walkthrough |
| 13 | 55–58 | Certification & job-search prep | AZ-305, mock system-design interviews, resume |

> Pacing is a guide, not a deadline. If a phase needs longer, protect the hands-on outcome and shift the remaining weeks — don't skip the architectural fundamentals to stay on the calendar.

---

## OrderFlow: project progression

One continuous system, not disconnected tutorial projects. It gains complexity only when a phase gives it a reason to.

1. **Foundation** — a clean .NET order-management app with tests and domain-oriented boundaries.
2. **Domain architecture** — order, inventory, customer, fulfilment workflows; DDD and CQRS where they clarify the design.
3. **System design** — documented quality attributes, failure modes, scaling assumptions, alternatives considered.
4. **Containers and cloud** — a repeatable container setup and a cost-conscious Azure deployment.
5. **Distributed integration** — API contracts and an event-driven flow (order placed → inventory reserved → fulfilment requested).
6. **Security and operations** — identity, authorization, secrets, CI/CD, IaC, tests, logs/metrics/traces, alerts.
7. **AI extension** — one bounded feature with a clear reason (e.g., a support assistant answering order questions via RAG), exposed to AI clients through a minimal MCP server — not AI for its own sake.
8. **Portfolio capstone** — polished personal repo, setup instructions, diagrams, ADRs, demo path, honest trade-off narrative.

Keep an **ADR** for every consequential choice: context, decision, alternatives, consequences, revisit conditions. This is your interview evidence later.

---

## Resource activation strategy

**Value ranking, for the "how much does this actually help" question:** Cloud Academy and Pluralsight are the two that close your real gaps (hands-on cloud/infra labs, and structured architecture/design theory) — treat those as central, not optional. The Azure credit is essential but modest — it buys small, real deployments, not a production-grade environment. DataCamp is narrowly useful, mainly for the AI phase. Visual Studio Professional is baseline tooling — necessary, not a differentiator. Everything else on the benefits page is genuinely optional; none of it closes a gap this plan has.

| Resource | Activate | Use it for | Why this timing |
|---|---|---|---|
| Visual Studio Professional | Now | Main IDE, debugging, tests, source control | No clock running down; just the daily tool |
| C# Dev Kit for VS Code | Now | Lightweight, cross-platform editing | Free; useful if you ever code outside Parallels |
| Pluralsight (6 mo / ~26 wks) | **Week 2** (Phase 1) | OOP/patterns → DDD/CQRS → system design → containers | Its catalog matches Phases 1–4 well; 26 weeks covers all four with room to spare |
| Cloud Academy (1 yr / ~52 wks) | **Week 18** (Phase 4) | Hands-on Docker/K8s/Azure/security/DevOps labs, later AZ-305 prep | Covers every infra-heavy phase through the end of the plan; **its labs run in the browser, so they work even on the restricted work laptop** |
| Azure $50/month credit | **Week 22** (Phase 5) | Small, scoped Azure deployments | First real deployment task, not before — avoids paying for idle resources |
| DataCamp (3 mo / ~13 wks) | **Week 37** (2 wks before Phase 9) | Python/data literacy for embeddings and RAG | Head start before building the RAG pipeline; runs out right as you no longer need it |
| Azure DevOps Basic | Optional, Phase 8 | Boards/Pipelines familiarity only (many employers use it) | Do **not** make it your CI/CD backbone — GitHub Actions from day one, since the final repo lives on personal GitHub anyway |
| Syncfusion Essential Studio | Optional, Phase 10 | Prebuilt UI components if you want a faster-looking frontend | Only if UI polish becomes a real goal — the plan doesn't need it |
| M365 Developer (E5), Syncfusion Bold BI, CODE Magazine, Technical Support (2 incidents) | On demand only | Whatever specific need arises | None of these close a gap in this plan |
| Visual Studio Live! (Live! 360) | Skip unless you want the networking | — | The discount covers registration only — travel/lodging likely still costs money, which cuts against wanting to avoid extra spend |
| Microsoft Q&A / Tech Community / Azure Community / Developer Community / Concierge Chat | As needed | Troubleshooting real blockers | Prefer official docs first; use these for genuine dead ends |
| YouTube | Any phase, on demand | Targeted explanation after a precise question | Never as a course feed |

### Explicit exclusions
- **Tim Corey discount** — not used, regardless of price.
- **Azure DevTest PAYG** — not used; it's a path to unplanned personal cost.
- No paid service is activated just because a trial or offer exists.

If a justified experiment doesn't fit the $50 credit, check the expected cost, set a budget alert, and prefer the Azure free tier on your personal account before spending anything real.

---

## Device, account, and source-control strategy

### Work laptop (Mac running Windows via Parallels)
- Fine for self-study, reading, and **browser-based Cloud Academy labs** — those don't need local installs.
- Docker Desktop and similar tools may be blocked by policy; don't try to work around company controls. If a tool won't install, use a Cloud Academy sandbox or move the task to the personal laptop.
- **Worth checking before you start writing OrderFlow code on it at all:** some employment agreements claim rights to anything built on company hardware, even personal projects, even on your own time. That's a question for your actual agreement or HR, not something to guess at — if there's any doubt, keep all actual OrderFlow coding on the personal laptop and use the work laptop strictly for labs, reading, and video.

### Personal laptop and accounts
- The build environment for anything Docker/Kubernetes, and the environment the final capstone must run cleanly from.
- OrderFlow's repository lives on **personal GitHub** from the start — not migrated over later. CI/CD runs on **GitHub Actions**, not Azure DevOps, so there's no dependency on an employer-tied tool for a personal portfolio.
- Deployment target is your **personal Azure account**, preferring the free tier; use the employer's $50 credit only for learning experiments where that's within policy.
- Before the capstone, do a clean clone on the personal laptop and confirm the setup instructions actually work from scratch.

---

## Practical rules that prevent overload
1. **One primary learning resource at a time**, chosen because it fits the current phase.
2. **Build every week.** A week of study with no visible OrderFlow change is the exception, not the norm.
3. **Limit parallel technologies** — introduce a new tool only when the architecture gives it a reason.
4. **Use "just enough" cloud** — start small, watch cost, tear down what's unused.
5. **Official docs for implementation details**; courses for structure; YouTube for targeted clarification.
6. **Capture evidence** — ADRs, diagrams, screenshots, test results, short notes on what changed and why.
7. **Review before moving on** — end each phase with: what was built, what was learned, what's still unclear.
8. **Certification is optional and comes last**, after demonstrated project competence.
9. **Use fictional/sample data** for anything that ends up in the public portfolio.
10. **Adjust pacing, not sequence** — if a phase runs long, protect the hands-on outcome and shift dates.

---

## Phase completion checklist
Before moving on, confirm:
- I can explain the phase's key concepts in plain language.
- OrderFlow contains a small, working application of the phase.
- I recorded the decision(s) and trade-offs made.
- I can name one risk, one alternative, and why the current choice is reasonable.
- I did the phase's theory/practical questions and know what's still unclear.
- The next phase has a limited, specific resource plan (not "watch more of everything").

---

## Ongoing cadence
- **Weekly:** run the learning loop, update OrderFlow, log blockers/decisions.
- **Monthly:** review time spent, subscription value, Azure cost, and next month's narrow focus.
- **At every phase boundary:** a short self-review — continue, reinforce, or refactor.
- **At the capstone:** validate the personal-laptop build, personal GitHub repo, deployment, diagrams, and the interview narrative end to end.

The guiding principle: use each benefit deliberately, turn learning into architecture evidence, and finish with a project that's fully yours and demonstrates the role you're moving into.
