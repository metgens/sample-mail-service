# SYS - Architecture choice

Date: 2019-12-27

## Status

Accepted

## Context

- small microservice with API
- normal workload (hundreds of emails/day), but high utilization possible in the future (millions/day)
  - should be easy to adopt to this new workload requirement
  - no need to do this right now
  
## Choices

- layer architecture
- full CQRS with service bus (for example RabbitMq or cloud services)
- full CQRS with in memory bus
- CQRS with in memory bus and simplified read-side (no queries objects just repositories used in API controller)

## Decision

- full CQRS with in memory bus

## Consequences

- Positive
  - easy to adpot to the new workload requirement (by adding some queues like Azure ServiceBus or EventHub)
  - code separated into small handlers classes
  - easy to test
  - standard architecture known by developers
- Negative
  - more code than in layer architecture
  - younger developers need to know the bus (mediator) concept - connection between command/queries and handlers
	
