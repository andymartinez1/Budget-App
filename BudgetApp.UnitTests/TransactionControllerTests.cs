using BudgetApp.Models.ViewModels;
using BudgetApp.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BudgetApp.UnitTests;

public class TransactionControllerTests
{
    [Fact]
    public async Task Details_ReturnsViewWithTransaction_WhenTransactionExists()
    {
        // Arrange
        var (controller, txMock, catMock, _) = UnitTestsHelper.CreateTransactionControllerWithMocks();
        var category = UnitTestsHelper.GetTestCategory();
        var transaction = UnitTestsHelper.GetTestTransaction();
        var transactionVm = UnitTestsHelper.GetTransactionViewModel();
        transactionVm.TransactionId = transaction.TransactionId;

        txMock.Setup(s => s.GetTransactionByIdAsync(transaction.TransactionId)).ReturnsAsync(transaction);
        catMock.Setup(c => c.GetCategoryByIdAsync(transaction.CategoryId)).ReturnsAsync(category);

        // Act
        var result = await controller.Details(transactionVm.TransactionId);

        // Assert
        var viewResult = Assert.IsType<PartialViewResult>(result);
        Assert.IsType<TransactionViewModel>(viewResult.Model);
    }
}