using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordPicker.Tests;

[TestClass]
public abstract class BaseTest
{
    protected WordPicker.Web.Data.ApplicationDbContext CreateContext()
    {
        return TestDbContextFactory.Create();
    }
}

public static class TestCategories
{
    public const string Unit = "Unit";
    public const string Integration = "Integration";
}
