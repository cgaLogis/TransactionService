using MediatR;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.UseCases.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand,CreateTransasctionResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    public async Task<CreateTransasctionResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Transaction
        {
            Id = request.Id,
            TransactionDate = request.TransactionDate,
            Amount = request.Amount
        };
        var item = await _transactionRepository.CreateAsync(entity,cancellationToken);
        return new CreateTransasctionResponse
        {
            InsertDateTime = item.TransactionDate
        };
    }
}