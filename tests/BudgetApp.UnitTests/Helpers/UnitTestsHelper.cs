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

    public static (
        TransactionController Controller,
        Mock<ITransactionService> TransactionMock,
        Mock<ICategoryService> CategoryMock,
        Mock<ILogger<TransactionController>> LoggerMock
    ) CreateTransactionControllerWithMocks()
    {
        var txMock = new Mock<ITransactionService>();
        var catMock = new Mock<ICategoryService>();
        var logMock = new Mock<ILogger<TransactionController>>();

        var controller = new TransactionController(txMock.Object, catMock.Object, logMock.Object);
        return (controller, txMock, catMock, logMock);
    }

    public static (
        CategoryController Controller,
        Mock<ICategoryService> CategoryMock,
        Mock<ILogger<CategoryController>> LoggerMock
    ) CreateCategoryControllerWithMocks()
    {
        var txMock = new Mock<ITransactionService>();
        var catMock = new Mock<ICategoryService>();
        var logMock = new Mock<ILogger<CategoryController>>();

        var controller = new CategoryController(catMock.Object, logMock.Object);
        return (controller, catMock, logMock);
    }

    internal static Category GetTestCategory()
    {
        var category = new Category { Id = 42, Name = "Sample" };
        return category;
    }

    internal static Transaction GetTestTransaction()
    {
        var transaction = new Transaction
        {
            Id = 1,
            CategoryId = 42,
            Amount = 100m,
            Date = DateTime.UtcNow,
            Name = "Test",
        };
        return transaction;
    }

    internal static TransactionViewModel GetTransactionViewModel()
    {
        var transaction = GetTestTransaction();
        var category = GetTestCategory();

        var transactionVm = new TransactionViewModel
        {
            TransactionId = transaction.Id,
            Amount = transaction.Amount,
            CategoryType = category.Name,
            Date = transaction.Date,
            Name = transaction.Name,
        };
        return transactionVm;
    }

    internal static CategoryViewModel GetCategoryViewModel()
    {
        var categoryVm = new CategoryViewModel();
        return categoryVm;
    }
}
