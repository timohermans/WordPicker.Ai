using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests.Web.Pages.WordLists;

[TestClass]
public class WordListsDetailsTests : BaseTest
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenListExists_ReturnsPageWithList()
    {
        // Arrange
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();

        var pageModel = new DetailsModel(context);

        // Act
        var result = await pageModel.OnGetAsync(list.Id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.IsNotNull(pageModel.WordList);
        Assert.AreEqual("Test List", pageModel.WordList.Name);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenListDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        using var context = CreateContext();
        var pageModel = new DetailsModel(context);

        // Act
        var result = await pageModel.OnGetAsync(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenListHasPickHistory_LoadsLast20Picks()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();
        var entry = new WordEntry { WordListId = list.Id, Value = "apple" };
        context.WordEntries.Add(entry);
        context.SaveChanges();
        context.PickHistories.Add(new PickHistory { WordEntryId = entry.Id, WordListId = list.Id, PickedAt = DateTime.UtcNow });
        context.SaveChanges();

        var pageModel = new DetailsModel(context);

        var result = await pageModel.OnGetAsync(list.Id);

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual(1, pageModel.PickHistories.Count);
        Assert.AreEqual("apple", pageModel.PickHistories[0].WordEntry.Value);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenListHasNoPickHistory_ShowsEmpty()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();

        var pageModel = new DetailsModel(context);

        var result = await pageModel.OnGetAsync(list.Id);

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual(0, pageModel.PickHistories.Count);
    }
}
