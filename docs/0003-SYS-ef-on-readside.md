# SYS - Entity Framework Core on read side

Date: 2019-12-28

## Status

Accepted

## Context

- simple data structure (mails and attachments)
- Get methods in API will be used occasionally, mainly for maintenance purposes

## Choices

- ADO.NET,
- Dapper,
- Entity Framework Core

## Decision

- Entity Framework Core
  - because read side is not a main part of app and will be used occasionally the performance of EF Core is sufficient

## Consequences

- Positive
  - easy to develop
  - already have defined EF Context for write side
- Negative
  - Dapper or ADO.NET has better performance, but need more development time to write SQL queries
	
