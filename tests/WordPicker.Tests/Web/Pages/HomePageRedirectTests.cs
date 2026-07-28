using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPicker.Web.Pages;

namespace WordPicker.Tests.Web.Pages;

[TestClass]
public class HomePageRedirectTests
{
    [TestMethod]
    [TestCategory(TestCategories.Unit)]
    public void OnGet_RedirectsToWordListsIndex()
    {
        var pageModel = new IndexModel();

        var result = pageModel.OnGet();

        var redirectResult = result as RedirectToPageResult;
        Assert.IsNotNull(redirectResult);
        Assert.AreEqual("/WordLists/Index", redirectResult.PageName);
    }
}