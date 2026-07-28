using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests;

[TestClass]
public class WordListsIndexTests : BaseTest
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenCalled_PopulatesWordLists()
    {
        // Arrange
        using var context = CreateContext();
        var pageModel = new IndexModel(context);

        // Act
        await pageModel.OnGetAsync();

        // Assert
        Assert.IsNotNull(pageModel.WordLists);
        Assert.AreEqual(0, pageModel.WordLists.Count);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenWordListsExist_ReturnsSortedList()
    {
        // Arrange
        using var context = CreateContext();
        context.WordLists.Add(new WordList { Name = "First List", CreatedAt = DateTime.UtcNow.AddDays(-1) });
        context.WordLists.Add(new WordList { Name = "Second List", CreatedAt = DateTime.UtcNow });
        context.SaveChanges();

        var pageModel = new IndexModel(context);

        // Act
        await pageModel.OnGetAsync();

        // Assert
        Assert.AreEqual(2, pageModel.WordLists.Count);
        Assert.AreEqual("Second List", pageModel.WordLists[0].Name);
        Assert.AreEqual("First List", pageModel.WordLists[1].Name);
    }
}
