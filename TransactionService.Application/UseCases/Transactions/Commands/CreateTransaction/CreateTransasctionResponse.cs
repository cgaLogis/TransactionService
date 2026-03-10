namespace TransactionService.Application.UseCases.Transactions.Commands.CreateTransaction;

public record CreateTransasctionResponse
{
    public DateTime InsertDateTime { get; set; }
}