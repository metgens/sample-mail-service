# SYS - Database choice - MSSQL with Entityframework Core

Date: 2019-12-27

## Status

Accepted

## Context

- simple data structure (mails and attachments)
  
## Choices

- MS SQL and Dapper,
- MS SQL and Entity Framework Core
- MongoDB

## Decision

- MS SQL and Entity Framework Core
  - mainly because developer has skills and experinence in solutions using these products (short development time)
  - no need to have flexible (nosql) data structure
  - no need to have horizontal scalability

## Consequences

- Positive
  - good enough performance
  - using EF create, update methods will be easy to implement (no need to write SQL queries)
  - migrations mechanism
  - EF in-memory database for testing 
- Negative
  - has no horizontal scaling (in opposite to MongoDB automatic sharding), it may be required when this microservice will be used for example in many geolocations.
	
