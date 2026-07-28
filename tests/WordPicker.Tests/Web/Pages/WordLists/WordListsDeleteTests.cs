using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests.Web.Pages.WordLists;

[TestClass]
public class WordListsDeleteTests : BaseTest
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnGetAsync_WhenListExists_ReturnsPageWithList()
    {
        // Arrange
        using var context = CreateContext();
        var list = new WordList { Name = "List to Delete" };
        context.WordLists.Add(list);
        context.SaveChanges();

        var pageModel = new DeleteModel(context);

        // Act
        var result = await pageModel.OnGetAsync(list.Id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.IsNotNull(pageModel.WordList);
        Assert.AreEqual("List to Delete", pageModel.WordList.Name);
    }

    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public async Task OnPostAsync_WhenListExists_RemovesList()
    {
        // Arrange
        using var context = CreateContext();
        var list = new WordList { Name = "List to Delete" };
        context.WordLists.Add(list);
        context.SaveChanges();

        var pageModel = new DeleteModel(context);

        // Act
        var result = await pageModel.OnPostAsync(list.Id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual(0, context.WordLists.Count());
    }
}
