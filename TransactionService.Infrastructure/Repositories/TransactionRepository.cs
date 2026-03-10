using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;
using TransactionService.Infrastructure.Data;

namespace TransactionService.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly TransactionDbContext _context;

    public TransactionRepository(TransactionDbContext context)
    {
        _context = context;
    }
    public async Task<Transaction> GetByIdAsync(Guid id,CancellationToken ct)
    {
        var item = await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,ct);
        return item;
    }
    
    public async Task<bool> ExistsAsync(Expression<Func<Transaction,bool>> where,CancellationToken ct)
    {
        return await _context.Transactions.AsNoTracking().AnyAsync(where,ct);;
    }
    public async Task<Transaction> CreateAsync(Transaction entity,CancellationToken ct)
    {
        await _context.Transactions.AddAsync(entity,ct);
        await _context.SaveChangesAsync();
        return entity;
    }
}