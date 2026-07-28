using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests;

[TestClass]
public class WordListsDetailsRemoveWordTests : BaseTest
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostRemoveWordAsync_WhenWordExists_RemovesWord()
    {
        // Arrange
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();
        var word = new WordEntry { WordListId = list.Id, Value = "hello" };
        context.WordEntries.Add(word);
        context.SaveChanges();

        var pageModel = new DetailsModel(context);

        // Act
        var result = await pageModel.OnPostRemoveWordAsync(list.Id, word.Id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual(0, context.WordEntries.Count());
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostRemoveWordAsync_WhenWordDoesNotExist_DoesNothing()
    {
        // Arrange
        using var context = CreateContext();
        var list = new WordList { Name = "Test List" };
        context.WordLists.Add(list);
        context.SaveChanges();
        var word = new WordEntry { WordListId = list.Id, Value = "hello" };
        context.WordEntries.Add(word);
        context.SaveChanges();

        var pageModel = new DetailsModel(context);

        // Act
        var result = await pageModel.OnPostRemoveWordAsync(list.Id, 999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual(1, context.WordEntries.Count());
    }
}
