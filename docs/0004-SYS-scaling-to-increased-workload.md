# SYS - Scaling to increased workload

Date: 2019-12-29

## Status

Pending

## Context

- workload will be increased to millions per day

Problems:
- database table 'Mail' will be storing a lot of data
  - querying mails to send and updating statuses will be much slower
- sending e-mails synchronously will be not efficient and probably will generate a lot of timeouts
- SMTP account may be the bottleneck of the system
  
## Solution

- change into asynchronous data processing 
  - in-memory bus may be partially or totally replaced with queue like Kafka (Azure EventHub) or bus like RabbitMq (Azure ServiceBus). Kafka looks better because it has partitioning mechanisms which allows easily split processing to many consumers.
  - send mail command should be enqueued on asynchronous queue
  - commands should be dequeued by one or many consumers (depends on workload). Each consumer should have independent SMTP account for mail sending 
  - consumers (mail senders) may be deployed as serverless functions (Azure Functions) or on Kubernetes instances
- send pending mails API should be removed. There is no sense to use it when millions e-mail per day are waiting for delivery (it should be triggered all the time)
  - instead mail send command should be enqueued immediately (or with some delay) after mail data is complete and ready for send
- split data model into sent/drafts/rejected and pending mails
  - should increase performance of data to send retrieving  
 
