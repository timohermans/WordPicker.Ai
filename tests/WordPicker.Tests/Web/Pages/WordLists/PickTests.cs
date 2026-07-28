using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests.Web.Pages.WordLists;

[TestClass]
public class PickTests : BaseTest
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenCalled_PopulatesWordLists()
    {
        using var context = CreateContext();
        var pageModel = new PickModel(context);

        await pageModel.OnGetAsync();

        Assert.IsNotNull(pageModel.WordLists);
        Assert.AreEqual(0, pageModel.WordLists.Count);
        Assert.IsFalse(pageModel.ShowResults);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenWordListsExist_ReturnsSortedList()
    {
        using var context = CreateContext();
        context.WordLists.Add(new WordList { Name = "First List", CreatedAt = DateTime.UtcNow.AddDays(-1) });
        context.WordLists.Add(new WordList { Name = "Second List", CreatedAt = DateTime.UtcNow });
        context.SaveChanges();

        var pageModel = new PickModel(context);

        await pageModel.OnGetAsync();

        Assert.AreEqual(2, pageModel.WordLists.Count);
        Assert.AreEqual("Second List", pageModel.WordLists[0].Name);
        Assert.AreEqual("First List", pageModel.WordLists[1].Name);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenNoListsSelected_ReturnsPageWithError()
    {
        using var context = CreateContext();
        context.WordLists.Add(new WordList { Name = "Test List" });
        context.SaveChanges();

        var pageModel = new PickModel(context);
        pageModel.SelectedListIds = new List<int>();

        var result = await pageModel.OnPostAsync();

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.IsFalse(pageModel.ModelState.IsValid);
        Assert.AreEqual(0, pageModel.Results.Count);
        Assert.IsFalse(pageModel.ShowResults);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenValidSelection_PicksOneWordPerList()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();
        context.WordEntries.Add(new WordEntry { WordListId = list.Id, Value = "apple" });
        context.WordEntries.Add(new WordEntry { WordListId = list.Id, Value = "banana" });
        context.SaveChanges();

        var pageModel = new PickModel(context);
        pageModel.SelectedListIds = new List<int> { list.Id };

        var result = await pageModel.OnPostAsync();

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.IsTrue(pageModel.ShowResults);
        Assert.AreEqual(1, pageModel.Results.Count);
        Assert.AreEqual("Test List", pageModel.Results[0].ListName);
        Assert.IsNotNull(pageModel.Results[0].Word);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenListHasNoWords_ShowsPlaceholder()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Empty List" };
        context.WordLists.Add(list);
        context.SaveChanges();

        var pageModel = new PickModel(context);
        pageModel.SelectedListIds = new List<int> { list.Id };

        var result = await pageModel.OnPostAsync();

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.IsTrue(pageModel.ShowResults);
        Assert.AreEqual(1, pageModel.Results.Count);
        Assert.AreEqual("Empty List", pageModel.Results[0].ListName);
        Assert.IsNull(pageModel.Results[0].Word);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenMultipleLists_PicksWordFromEach()
    {
        using var context = CreateContext();
        var listA = new WordList { Name = "List A" };
        var listB = new WordList { Name = "List B" };
        context.WordLists.AddRange(listA, listB);
        context.SaveChanges();
        context.WordEntries.Add(new WordEntry { WordListId = listA.Id, Value = "hello" });
        context.WordEntries.Add(new WordEntry { WordListId = listB.Id, Value = "world" });
        context.SaveChanges();

        var pageModel = new PickModel(context);
        pageModel.SelectedListIds = new List<int> { listA.Id, listB.Id };

        var result = await pageModel.OnPostAsync();

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual(2, pageModel.Results.Count);
        Assert.AreEqual("List A", pageModel.Results[0].ListName);
        Assert.AreEqual("hello", pageModel.Results[0].Word);
        Assert.AreEqual("List B", pageModel.Results[1].ListName);
        Assert.AreEqual("world", pageModel.Results[1].Word);
    }
}
