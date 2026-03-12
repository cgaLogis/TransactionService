using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.RateLimiting;
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
        builder.Services.AddRateLimiter(opts =>
        {
            opts.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            opts.OnRejected = async (context, token) =>
            {
                await context.HttpContext.Response.WriteAsync("Too many queries.Wait please.", cancellationToken: token);
            };

            opts.AddPolicy("TokenBucketPolicy", httpContext => {
                var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                return RateLimitPartition.GetTokenBucketLimiter(
                    partitionKey: ipAddress,
                    factory: _ => new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 5,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(5),
                        TokensPerPeriod = 1,
                        AutoReplenishment = true
                    }
                    );
            });
        });

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
        app.UseRateLimiter();
        app.UseAuthorization();
        
        app.MapGet("api/v1/transactions/{id}", async (Guid id,IMediator mediator) => await mediator.Send(new GetTransactionByIdQuery() { Id = id}))
            .RequireRateLimiting("TokenBucketPolicy");

        app.MapPost("api/v1/transactions", async ( CreateTransactionCommand command, IMediator mediator) => await mediator.Send(command));
        
        app.Run();
    }
}