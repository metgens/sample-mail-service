# Introduction

Mail.Service is a sample project that main purpose is sending e-mails using external SMTP account.
It contain REST API which allows:
- create a mail, without sending it,
- edit mail
- add/remove mail attachments
- trigger mail sending
- get stored mails and their statuses

# Running locally

- `dotnet run --project MailService.Api/MailService.Api.csproj` 
- Swagger local address: https://localhost:5001/swagger/index.html

# Architecture

## CQRS

![CQRS diagram](https://github.com/metgens/sample-mail-service-api/blob/master/docs/images/cqrs.png)

- Main architecture is based on CQRS concept
- Commands and Queries use in-memory bus [MediatR](https://github.com/jbogard/MediatR)
- Because data model is simple, read/write sides use same tables in relational (MSSQL) database
- Write side uses Entity Framework Core 
- Read side also uses Entity Framework, because it will be only used occasionally for maintenance purposes.

## Validation

- Command validation is handled using [Fluent validation](https://fluentvalidation.net/) library and [MediatR pipeline behaviors](https://github.com/jbogard/MediatR/wiki/Behaviors)
- Rules for validation are stored in commands classes files. Their names end with `Validator` sufix. For example: `AddMailAttachmentCmdValidator`
- MediatR behavior is implemented in `ValidateCommandBehavior` class.

## Exception handling

- Exception handling is provided using mvc filter called: `GlobalExceptionFilter` (in API project)
- Global exception filter logs exception data to logger and returns response with short message to user
  - in DEVELOPMENT environment response contains additionally complete exception message and stacktrace

## Logging

- logging is based on `Microsoft.Extensions.Logging` mechanism with added [NLog](https://nlog-project.org/) provider
- base logging configuration (like loglevels etc.) is stored in `appsettings` in `Logging` section
- logging destinations are configured in `nlog.config` file
