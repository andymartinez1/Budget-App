using BudgetApp.Controllers;
using BudgetApp.Entities;
using BudgetApp.Models;
using BudgetApp.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BudgetApp.UnitTests;

public class TransactionControllerTests
{
    [Fact]
    public async Task Index_ReturnsViewWithViewModel()
    {
        // Arrange
        var (controller, txMock, catMock, _) =
            UnitTestsHelper.CreateTransactionControllerWithMocks();
        var transactions = new List<Transaction> { UnitTestsHelper.GetTestTransaction() };
        var categories = new List<Category> { UnitTestsHelper.GetTestCategory() };

        txMock.Setup(s => s.GetAllAsync()).ReturnsAsync(transactions);
        catMock.Setup(s => s.GetAllAsync()).ReturnsAsync(categories);

        // Act
        var result = await controller.Index(null, null, null, null);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<TransactionCategoryViewModel>(viewResult.Model);
        Assert.Single(model.Transactions);
        Assert.NotEmpty(model.CategoriesSelectList);
    }

    [Fact]
    public async Task Details_ReturnsPartialView()
    {
        // Arrange
        var (controller, txMock, catMock, _) =
            UnitTestsHelper.CreateTransactionControllerWithMocks();
        var transaction = UnitTestsHelper.GetTestTransaction();
        var category = UnitTestsHelper.GetTestCategory();

        txMock.Setup(s => s.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);
        catMock.Setup(s => s.GetByIdAsync(transaction.CategoryId)).ReturnsAsync(category);

        // Act
        var result = await controller.Details(transaction.Id);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_DetailsModalPartial", partialViewResult.ViewName);
        var model = Assert.IsType<TransactionViewModel>(partialViewResult.Model);
        Assert.Equal(transaction.Amount, model.Amount);
    }

    [Fact]
    public async Task Create_Get_ReturnsPartialView()
    {
        // Arrange
        var (controller, _, catMock, _) = UnitTestsHelper.CreateTransactionControllerWithMocks();
        catMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Category>());

        // Act
        var result = await controller.Create();

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_CreateModalPartial", partialViewResult.ViewName);
        Assert.IsType<TransactionViewModel>(partialViewResult.Model);
    }

    [Fact]
    public async Task Create_Post_ValidModel_ReturnsCreated()
    {
        // Arrange
        var (controller, txMock, _, _) = UnitTestsHelper.CreateTransactionControllerWithMocks();
        var transactionVm = new TransactionViewModel { Name = "New Tx", Amount = 10m };
        txMock.Setup(s => s.AddAsync(transactionVm)).ReturnsAsync(new Transaction());

        // Act
        var result = await controller.Create(transactionVm);

        // Assert
        Assert.IsType<CreatedResult>(result);
        txMock.Verify(s => s.AddAsync(transactionVm), Times.Once);
    }

    [Fact]
    public async Task Create_Post_Failure_ReturnsPartialView()
    {
        // Arrange
        var (controller, txMock, catMock, _) =
            UnitTestsHelper.CreateTransactionControllerWithMocks();
        var transactionVm = new TransactionViewModel { Name = "New Tx", Amount = 10m };
        txMock.Setup(s => s.AddAsync(transactionVm)).ReturnsAsync((Transaction)null);
        catMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Category>());

        // Act
        var result = await controller.Create(transactionVm);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_CreateModalPartial", partialViewResult.ViewName);
        Assert.False(controller.ModelState.IsValid);
    }

    [Fact]
    public async Task Edit_Get_ReturnsPartialView()
    {
        // Arrange
        var (controller, txMock, catMock, _) =
            UnitTestsHelper.CreateTransactionControllerWithMocks();
        var transaction = UnitTestsHelper.GetTestTransaction();
        txMock.Setup(s => s.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);
        catMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Category>());

        // Act
        var result = await controller.Edit(transaction.Id);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_EditModalPartial", partialViewResult.ViewName);
        var model = Assert.IsType<TransactionViewModel>(partialViewResult.Model);
        Assert.Equal(transaction.Id, model.TransactionId);
    }

    [Fact]
    public async Task Edit_Post_UpdatesAndReturnsCreatedAtAction()
    {
        // Arrange
        var (controller, txMock, _, _) = UnitTestsHelper.CreateTransactionControllerWithMocks();
        var transaction = UnitTestsHelper.GetTestTransaction();
        var transactionVm = new TransactionViewModel
        {
            TransactionId = transaction.Id,
            Name = "Updated Tx",
            Amount = 20m,
            CategoryId = transaction.CategoryId,
        };
        txMock.Setup(s => s.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);

        // Act
        var result = await controller.Edit(transaction.Id, transactionVm);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("Details", createdResult.ActionName);
        txMock.Verify(s => s.UpdateAsync(transaction.Id, transactionVm), Times.Once);
    }

    [Fact]
    public async Task Delete_Get_ReturnsPartialView()
    {
        // Arrange
        var (controller, txMock, catMock, _) =
            UnitTestsHelper.CreateTransactionControllerWithMocks();
        var transaction = UnitTestsHelper.GetTestTransaction();
        var category = UnitTestsHelper.GetTestCategory();
        txMock.Setup(s => s.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);
        catMock.Setup(s => s.GetByIdAsync(transaction.CategoryId)).ReturnsAsync(category);

        // Act
        var result = await controller.Delete(transaction.Id);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_DeleteModalPartial", partialViewResult.ViewName);
        var model = Assert.IsType<TransactionViewModel>(partialViewResult.Model);
        Assert.Equal(transaction.Name, model.Name);
    }

    [Fact]
    public async Task DeleteConfirmed_Post_RedirectsToIndex()
    {
        // Arrange
        var (controller, txMock, _, _) = UnitTestsHelper.CreateTransactionControllerWithMocks();
        int transactionId = 1;

        // Act
        var result = await controller.DeleteConfirmed(transactionId);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        txMock.Verify(s => s.DeleteAsync(transactionId), Times.Once);
    }
}
