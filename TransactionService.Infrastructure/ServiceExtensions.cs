using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Application.Interfaces;
using TransactionService.Infrastructure.Data;
using TransactionService.Infrastructure.Repositories;

namespace TransactionService.Infrastructure;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TransactionDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("TransactionDbConnString"),
                b => b.MigrationsAssembly(typeof(TransactionDbContext).Assembly.FullName)));
        
        services.AddTransient<ITransactionRepository, TransactionRepository>();
    }
}