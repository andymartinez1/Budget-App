using BudgetApp.Controllers;
using BudgetApp.Models;
using BudgetApp.Models.ViewModels;
using BudgetApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace BudgetApp.UnitTests.Helpers;

public class UnitTestsHelper
{
    private static int _categoryId;
    private static int _transactionId = 1;

    public UnitTestsHelper(int categoryId, int transactionId)
    {
        _categoryId = categoryId;
        _transactionId = transactionId;
    }

    internal static Mock<ITransactionService> GetTransactionServiceMock(int id)
    {
        var mockTransactionService = new Mock<ITransactionService>();
        mockTransactionService
            .Setup(s => s.GetTransactionByIdAsync(id));
        return mockTransactionService;
    }

    internal static Mock<ICategoryService> GetCategoryServiceMock(int id)
    {
        var mockCategoryService = new Mock<ICategoryService>();
        mockCategoryService.Setup(c => c.GetCategoryByIdAsync(id));
        return mockCategoryService;
    }

    internal static Mock<ILogger<TransactionController>> GetLoggerMock()
    {
        var mockLogger = new Mock<ILogger<TransactionController>>();
        return mockLogger;
    }

    public static (TransactionController Controller,
        Mock<ITransactionService> TransactionMock,
        Mock<ICategoryService> CategoryMock,
        Mock<ILogger<TransactionController>> LoggerMock)
        CreateTransactionControllerWithMocks()
    {
        var txMock = new Mock<ITransactionService>();
        var catMock = new Mock<ICategoryService>();
        var logMock = new Mock<ILogger<TransactionController>>();

        var controller = new TransactionController(txMock.Object, catMock.Object, logMock.Object);
        return (controller, txMock, catMock, logMock);
    }

    internal static Category GetTestCategory()
    {
        var category = new Category
        {
            CategoryId = 42,
            Type = "Sample"
        };
        return category;
    }

    internal static Transaction GetTestTransaction()
    {
        var transaction = new Transaction
        {
            TransactionId = 1,
            CategoryId = 42,
            Amount = 100m,
            Date = DateTime.UtcNow,
            Name = "Test"
        };
        return transaction;
    }

    internal static TransactionViewModel GetTransactionViewModel()
    {
        var transaction = GetTestTransaction();
        var category = GetTestCategory();

        var transactionVm = new TransactionViewModel
        {
            TransactionId = transaction.TransactionId,
            Amount = transaction.Amount,
            CategoryType = category.Type,
            Date = transaction.Date,
            Name = transaction.Name
        };
        return transactionVm;
    }
}