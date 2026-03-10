using MediatR;
using TransactionService.Application.Excceptions;
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
        var transactionsCount = await _transactionRepository.GetTransactionsCountAsync(cancellationToken);
        
        if (transactionsCount > 100)
        {
            throw new TransactionsMaxCountException();
        }

        var exists = await _transactionRepository.ExistsAsync(x => x.Id == request.Id, cancellationToken);

        if (request.Amount < 0)
            throw new TransactionAmountLessZeroException(request.Id,request.Amount);

        DateTime insertDateTime = DateTime.MinValue;

        if (exists)
        {
            var item = await _transactionRepository.GetByIdAsync(request.Id,cancellationToken);
            insertDateTime = item.TransactionDate;
        }else
        {


            var entity = new Transaction
            {
                Id = request.Id,
                TransactionDate = request.TransactionDate,
                Amount = request.Amount
            };
            var item = await _transactionRepository.CreateAsync(entity, cancellationToken);
            insertDateTime = item.TransactionDate;
        }

        return new CreateTransasctionResponse
        {
            InsertDateTime = insertDateTime
        };

    }
}