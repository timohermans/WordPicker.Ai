using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests;

[TestClass]
public class WordListsIndexPostTests : BaseTest
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenValidName_AddsWordList()
    {
        // Arrange
        using var context = CreateContext();
        var pageModel = new IndexModel(context)
        {
            NewWordList = new WordList { Name = "Test List" }
        };

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual(1, context.WordLists.Count());
        Assert.AreEqual("Test List", context.WordLists.Single().Name);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenNameIsEmpty_ReturnsPageWithError()
    {
        // Arrange
        using var context = CreateContext();
        var pageModel = new IndexModel(context)
        {
            NewWordList = new WordList { Name = "" }
        };
        pageModel.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual(0, context.WordLists.Count());
    }
}
