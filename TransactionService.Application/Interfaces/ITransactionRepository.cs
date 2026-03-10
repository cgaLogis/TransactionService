using System.Linq.Expressions;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> GetByIdAsync(Guid id,CancellationToken ct);
    Task<Transaction> CreateAsync(Transaction entity,CancellationToken ct);
    Task<bool> ExistsAsync(Expression<Func<Transaction, bool>> where, CancellationToken ct);
}