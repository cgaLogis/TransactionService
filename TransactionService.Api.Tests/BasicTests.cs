using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using TransactionService.Api;

public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TransactionGetById_Return_NotFoundExceptionMessage()
    {
        // Arrange
        var client = _factory.CreateClient();
        var transactionGuid = Guid.NewGuid();
        // Act
        var response = await client.GetAsync($"/api/v1/transaction?id={transactionGuid}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task TransactionGetById_Return_Success()
    {
        // Arrange
        var client = _factory.CreateClient();
        var transactionGuid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        // Act
        var response = await client.GetAsync($"/api/v1/transaction?id={transactionGuid}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK, await response.Content.ReadAsStringAsync());
    }
}
