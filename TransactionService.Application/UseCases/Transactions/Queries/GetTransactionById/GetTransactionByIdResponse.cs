namespace TransactionService.Application.UseCases.Transactions.Queries.GetTransactionById;

public record GetTransactionByIdResponse(Guid Id,DateTime TransactionDate,decimal Amount);