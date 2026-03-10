using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TransactionService.Application;

public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }

}