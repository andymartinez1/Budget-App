using BudgetApp.Entities;
using BudgetApp.Models;
using BudgetApp.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BudgetApp.UnitTests;

public class CategoryControllerTests
{
    [Fact]
    public async Task Index_ReturnsViewWithViewModel()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var categories = new List<Category> { UnitTestsHelper.GetTestCategory() };
        catMock.Setup(s => s.GetAllAsync()).ReturnsAsync(categories);

        // Act
        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<TransactionCategoryViewModel>(viewResult.Model);
        Assert.Single(model.Categories);
    }

    [Fact]
    public async Task Create_Get_ReturnsPartialView()
    {
        // Arrange
        var (controller, _, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();

        // Act
        var result = await controller.Create();

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_CreateCategoryModalPartial", partialViewResult.ViewName);
        Assert.IsType<CategoryViewModel>(partialViewResult.Model);
    }

    [Fact]
    public async Task Create_Post_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var categoryVm = new CategoryViewModel { Name = "New Category" };

        // Act
        var result = await controller.Create(categoryVm);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        catMock.Verify(s => s.AddAsync(categoryVm), Times.Once);
    }

    [Fact]
    public async Task Create_Post_InvalidModel_ReturnsPartialView()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var categoryVm = new CategoryViewModel();
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await controller.Create(categoryVm);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_CreateCategoryModalPartial", partialViewResult.ViewName);
        catMock.Verify(s => s.AddAsync(It.IsAny<CategoryViewModel>()), Times.Never);
    }

    [Fact]
    public async Task Edit_Get_ReturnsPartialView()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var category = UnitTestsHelper.GetTestCategory();
        catMock.Setup(s => s.GetByIdAsync(category.Id)).ReturnsAsync(category);

        // Act
        var result = await controller.Edit(category.Id);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_EditCategoryModalPartial", partialViewResult.ViewName);
        var model = Assert.IsType<CategoryViewModel>(partialViewResult.Model);
        Assert.Equal(category.Id, model.CategoryId);
    }

    [Fact]
    public async Task Edit_Post_ValidModel_UpdatesAndReturnsCreatedAtAction()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var category = UnitTestsHelper.GetTestCategory();
        var categoryVm = new CategoryViewModel { CategoryId = category.Id, Name = "Updated Name" };

        catMock.Setup(s => s.GetByIdAsync(category.Id)).ReturnsAsync(category);

        // Act
        var result = await controller.Edit(category.Id, categoryVm);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("Index", createdResult.ActionName);
        var returned = Assert.IsType<Category>(createdResult.Value);
        Assert.Equal("Updated Name", returned.Name);
        catMock.Verify(s => s.UpdateAsync(category.Id, categoryVm), Times.Once);
    }

    [Fact]
    public async Task Edit_Post_InvalidModel_ReturnsPartialView()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var categoryVm = new CategoryViewModel();
        controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await controller.Edit(1, categoryVm);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_EditCategoryModalPartial", partialViewResult.ViewName);
        catMock.Verify(
            s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryViewModel>()),
            Times.Never
        );
    }

    [Fact]
    public async Task Delete_Get_ReturnsPartialView()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var category = UnitTestsHelper.GetTestCategory();
        catMock.Setup(s => s.GetByIdAsync(category.Id)).ReturnsAsync(category);

        // Act
        var result = await controller.Delete(category.Id);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_DeleteCategoryModalPartial", partialViewResult.ViewName);
        var model = Assert.IsType<CategoryViewModel>(partialViewResult.Model);
        Assert.Equal(category.Name, model.Name);
    }

    [Fact]
    public async Task DeleteConfirmed_Post_RedirectsToIndex()
    {
        // Arrange
        var (controller, catMock, _) = UnitTestsHelper.CreateCategoryControllerWithMocks();
        var categoryId = 42;

        // Act
        var result = await controller.DeleteConfirmed(categoryId);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        catMock.Verify(s => s.DeleteAsync(categoryId), Times.Once);
    }
}
