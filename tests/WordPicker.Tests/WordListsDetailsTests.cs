using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Models;
using WordPicker.Web.Pages.WordLists;

namespace WordPicker.Tests;

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
}
