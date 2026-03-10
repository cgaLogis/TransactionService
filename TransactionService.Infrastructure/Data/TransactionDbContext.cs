using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.Entities;

namespace TransactionService.Infrastructure.Data;

public class TransactionDbContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionDbContext).Assembly);
        //base.OnModelCreating(modelBuilder);
    }
}