using BudgetApp.Entities;
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

        catMock.Setup(s => s.GetByIdAsync(category.Id)).ReturnsAsync(category);

        // Act
        var result = await controller.Edit(category.Id, categoryVm);

        // Assert
        var ok = Assert.IsType<CreatedAtActionResult>(result);
        var returned = Assert.IsType<Category>(ok.Value);
        Assert.Equal(categoryVm.Name, returned.Name);
        Mock.Get(catMock.Object).Verify(s => s.UpdateAsync(category.Id), Times.Once);
    }
}
