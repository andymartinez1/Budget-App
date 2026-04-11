using BudgetApp.UnitTests.Helpers;
using Moq;
using Xunit;

namespace BudgetApp.UnitTests;

public class TransactionControllerTests
{
    [Fact]
    public async Task Details_ReturnsViewWithTransaction_WhenTransactionExists()
    {
        // Arrange
        var (controller, txMock, catMock, _) =
            UnitTestsHelper.CreateTransactionControllerWithMocks();
        var category = UnitTestsHelper.GetTestCategory();
        var transaction = UnitTestsHelper.GetTestTransaction();
        var transactionVm = UnitTestsHelper.GetTransactionViewModel();

        txMock.Setup(s => s.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);
        catMock.Setup(c => c.GetByIdAsync(transaction.CategoryId)).ReturnsAsync(category);

        // Act
        var result = await controller.Details(transactionVm.TransactionId);

        // Assert
        var viewResult = Assert.IsType<PartialViewResult>(result);
        Assert.IsType<TransactionViewModel>(viewResult.Model);
    }

    [Fact]
    public async Task Edit_Post_UpdatesTransactionAndCallsUpdateService()
    {
        // Arrange
        var (controller, txMock, catMock, _) =
            UnitTestsHelper.CreateTransactionControllerWithMocks();
        var transaction = UnitTestsHelper.GetTestTransaction();
        var transactionVm = UnitTestsHelper.GetTransactionViewModel();

        txMock.Setup(s => s.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);
        txMock.Setup(s => s.UpdateAsync(transaction.Id)).ReturnsAsync(transaction);

        // Act
        var result = await controller.Edit(transaction.Id, transactionVm);

        // Assert
        var ok = Assert.IsType<CreatedAtActionResult>(result);
        var returned = Assert.IsType<Transaction>(ok.Value);
        Assert.Equal(transactionVm.Name, returned.Name);
        Assert.Equal(transactionVm.Amount, returned.Amount);
        Mock.Get(txMock.Object).Verify(s => s.UpdateAsync(transaction.Id), Times.Once);
    }

    [Fact]
    public async Task DeleteConfirmed_CallsDeleteAndRedirectsToIndex()
    {
        var (controller, txMock, catMock, _) =
            UnitTestsHelper.CreateTransactionControllerWithMocks();

        txMock.Setup(s => s.DeleteAsync(9)).Returns(Task.CompletedTask).Verifiable();

        var result = await controller.DeleteConfirmed(9);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(TransactionController.Index), redirect.ActionName);
        txMock.Verify(s => s.DeleteAsync(9), Times.Once);
    }
}
