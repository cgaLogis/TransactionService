using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Application;
using TransactionService.Application.UseCases.Transactions.Commands.CreateTransaction;
using TransactionService.Application.UseCases.Transactions.Queries.GetTransactionById;
using TransactionService.Infrastructure;

namespace TransactionService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "TransactionService");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapGet("api/v1/transaction", async ([FromQuery] Guid id,IMediator mediator) => await mediator.Send(new GetTransactionByIdQuery() { Id = id}));

        app.MapPost("api/v1/transaction", async (IMediator mediator, CreateTransactionCommand command) => await mediator.Send(command));
        
        app.Run();
    }
}