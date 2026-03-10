# TransactionService

## Migrations
### Create mingration
dotnet ef migrations add Init --project TransactionService.Infrastructure --startup-project TransactionService.Api

### Init migrations
1.встать в корень проекта TransactionService
2.выполнить команду

dotnet ef database update --project TransactionService.Infrastructure --startup-project TransactionService.Api