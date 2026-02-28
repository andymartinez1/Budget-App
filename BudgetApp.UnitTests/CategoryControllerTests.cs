using BudgetApp.Models;
using BudgetApp.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BudgetApp.UnitTests;

public class CategoryControllerTests
{
    [Fact]
    public async Task Edit_Post_UpdatesCategoryAndCallsUpdateService()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var category = UnitTestsHelper.GetTestCategory();
        var categoryVm = UnitTestsHelper.GetCategoryViewModel();

        catMock.Setup(s => s.GetCategoryByIdAsync(category.CategoryId)).ReturnsAsync(category);

        // Act
        var result = await controller.Edit(category.CategoryId, categoryVm);

        // Assert
        var ok = Assert.IsType<CreatedAtActionResult>(result);
        var returned = Assert.IsType<Category>(ok.Value);
        Assert.Equal(categoryVm.Type, returned.Type);
        Mock.Get(catMock.Object).Verify(s => s.UpdateCategoryAsync(category.CategoryId), Times.Once);
    }
}