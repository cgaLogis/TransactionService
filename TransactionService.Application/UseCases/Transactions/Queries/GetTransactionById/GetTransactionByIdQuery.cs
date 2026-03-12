using MediatR;
using TransactionService.Application.Excceptions;
using TransactionService.Application.Interfaces;

namespace TransactionService.Application.UseCases.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQuery : IRequest<GetTransactionByIdResponse>
{
    public Guid Id { get; set; }

    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, GetTransactionByIdResponse>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<GetTransactionByIdResponse> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var exists = await _transactionRepository.ExistsAsync(x => x.Id == request.Id, cancellationToken);
            if (!exists)
                throw new NotFoundTransactionException(request.Id);
            var item = await _transactionRepository.GetByIdAsync(request.Id, cancellationToken);
            return new GetTransactionByIdResponse(item.Id, item.TransactionDate, item.Amount);
        }
    }

}