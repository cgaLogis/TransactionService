using MediatR;

namespace TransactionService.Application.UseCases.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommand : IRequest<CreateTransasctionResponse>
{
    public Guid Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
}