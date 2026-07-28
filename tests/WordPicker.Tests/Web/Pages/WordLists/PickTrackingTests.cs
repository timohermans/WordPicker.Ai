using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests.Web.Pages.WordLists;

[TestClass]
public class PickTrackingTests : BaseTest
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenPickingWord_RecordsPickHistory()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();
        context.WordEntries.Add(new WordEntry { WordListId = list.Id, Value = "apple" });
        context.SaveChanges();

        var pageModel = new PickModel(context);
        pageModel.SelectedListIds = new List<int> { list.Id };

        await pageModel.OnPostAsync();

        Assert.AreEqual(1, context.PickHistories.Count());
        var history = context.PickHistories.Single();
        Assert.AreEqual(list.Id, history.WordListId);
        Assert.AreEqual("apple", context.WordEntries.Find(history.WordEntryId)!.Value);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenAllWordsPicked_AdvancesCycle()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Test List", CurrentPickCycle = 1 };
        context.WordLists.Add(list);
        context.SaveChanges();
        var entry = new WordEntry { WordListId = list.Id, Value = "apple", LastPickCycle = 1 };
        context.WordEntries.Add(entry);
        context.SaveChanges();

        var pageModel = new PickModel(context);
        pageModel.SelectedListIds = new List<int> { list.Id };

        var result = await pageModel.OnPostAsync();

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual(2, context.WordLists.Find(list.Id)!.CurrentPickCycle);
        Assert.AreEqual(2, context.WordEntries.Find(entry.Id)!.LastPickCycle);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenNewWordAddedMidCycle_IsEligibleImmediately()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Test List", CurrentPickCycle = 1 };
        context.WordLists.Add(list);
        context.SaveChanges();
        var oldEntry = new WordEntry { WordListId = list.Id, Value = "old", LastPickCycle = 1 };
        context.WordEntries.Add(oldEntry);
        context.SaveChanges();
        var newEntry = new WordEntry { WordListId = list.Id, Value = "new", LastPickCycle = 0 };
        context.WordEntries.Add(newEntry);
        context.SaveChanges();

        var pageModel = new PickModel(context);
        pageModel.SelectedListIds = new List<int> { list.Id };

        var result = await pageModel.OnPostAsync();

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.IsNotNull(pageModel.Results[0].Word);
        Assert.AreEqual("new", pageModel.Results[0].Word);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenPicking_TracksCycleOnWordEntry()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();
        context.WordEntries.Add(new WordEntry { WordListId = list.Id, Value = "word" });
        context.SaveChanges();

        var pageModel = new PickModel(context);
        pageModel.SelectedListIds = new List<int> { list.Id };

        await pageModel.OnPostAsync();

        var updatedEntry = context.WordEntries.Single();
        Assert.AreEqual(1, updatedEntry.LastPickCycle);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task RemoveWord_CascadesToPickHistory()
    {
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();
        var entry = new WordEntry { WordListId = list.Id, Value = "apple" };
        context.WordEntries.Add(entry);
        context.SaveChanges();
        context.PickHistories.Add(new PickHistory { WordEntryId = entry.Id, WordListId = list.Id });
        context.SaveChanges();

        context.WordEntries.Remove(entry);
        context.SaveChanges();

        Assert.AreEqual(0, context.PickHistories.Count());
    }
}
