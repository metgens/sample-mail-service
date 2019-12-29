# Introduction

# Architecture

## Validation

## Exception handling

- Exception handling is provided using mvc filter called: `GlobalExceptionFilter` (in API project)
- Global exception filter logs exception data to logger and returns response with short message to user
  - in DEVELOPMENT environment response contains additionally complete exception message and stacktrace

## Logging

- logging is based on `Microsoft.Extensions.Logging` mechanism with added `NLog` provider
- base logging configuration (like loglevels etc.) is stored in `appsettings` in `Logging` section
- logging destinations are configured in `nlog.config` file