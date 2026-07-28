using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests.Web.Pages.WordLists;

[TestClass]
public class WordListsDetailsAddWordTests : BaseTest
{
    private static DetailsModel CreatePageModelWithContext(WordPicker.Web.Data.ApplicationDbContext context)
    {
        var pageModel = new DetailsModel(context) { NewWord = "" };
        var httpContext = new DefaultHttpContext();
        pageModel.PageContext = new PageContext(new ActionContext(httpContext, new RouteData(), new PageActionDescriptor()));
        pageModel.TempData = new TempDataDictionary(httpContext, MockTempDataProvider.Instance);
        return pageModel;
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAddWordAsync_WhenValidWord_AddsWordToList()
    {
        // Arrange
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();

        var pageModel = CreatePageModelWithContext(context);
        pageModel.NewWord = "hello";

        // Act
        var result = await pageModel.OnPostAddWordAsync(list.Id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual(1, context.WordEntries.Count());
        Assert.AreEqual("hello", context.WordEntries.Single().Value);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAddWordAsync_WhenWordIsEmpty_RedirectsWithoutAdding()
    {
        // Arrange
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();

        var pageModel = CreatePageModelWithContext(context);
        pageModel.NewWord = "";

        // Act
        var result = await pageModel.OnPostAddWordAsync(list.Id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual(0, context.WordEntries.Count());
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAddWordAsync_WhenWordIsDuplicate_DoesNotAdd()
    {
        // Arrange
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();
        context.WordEntries.Add(new WordEntry { WordListId = list.Id, Value = "hello" });
        context.SaveChanges();

        var pageModel = CreatePageModelWithContext(context);
        pageModel.NewWord = "hello";

        // Act
        var result = await pageModel.OnPostAddWordAsync(list.Id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual(1, context.WordEntries.Count());
    }
}

public class MockTempDataProvider : ITempDataProvider
{
    public static MockTempDataProvider Instance { get; } = new();

    public IDictionary<string, object?> LoadTempData(HttpContext context) => new Dictionary<string, object?>();

    public void SaveTempData(HttpContext context, IDictionary<string, object?> values) { }
}
