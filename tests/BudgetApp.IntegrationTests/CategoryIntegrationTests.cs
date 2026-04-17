using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BudgetApp.IntegrationTests;

public class CategoryIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public CategoryIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_CategoryIndex_ReturnsSuccess_WhenAuthenticated()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/Category/Index");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Budget", content);
    }

    [Fact]
    public async Task Post_CategoryCreate_ReturnsRedirectToIndex_WhenAuthenticated()
    {
        // Arrange
        var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false }
        );
        var formData = new Dictionary<string, string> { { "Name", "IntegrationTestCategory" } };
        var content = new FormUrlEncodedContent(formData);

        // Act
        var response = await client.PostAsync("/Category/Create", content);

        // Assert
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        // ActionName Index in CategoryController will redirect to /Category or /Category/Index
        Assert.Contains("Category", response.Headers.Location.OriginalString);
    }
}
