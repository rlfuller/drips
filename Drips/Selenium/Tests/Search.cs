using Drips.Selenium.Pages;
using Drips.Tests;

namespace Drips.Selenium.Tests
{

    internal class Search : BaseTest
    {

        [Test]
        public void SearchForItemsInCatalogReturnsProduct()
        {

            var search = new SearchResultsPage(driver);
            search.SearchForItem("pants");

            var searchResults = new SearchResultsPage(driver);
            var items = searchResults.GetSearchResults();
            Assert.That(items.Count, Is.GreaterThan(0));

        }

        [Test]
        public void SearchForItemsNotInCatalog()
        {
            var basePage = new BasePage(driver);
                     
            basePage.SearchForItem("dsfsdfs");

            var searchResults = new SearchResultsPage(driver);
            var message = searchResults.GetMessage();

            var expectedResults = "Your search returned no results.";
            Assert.That(message, Is.EqualTo(expectedResults));
        }
    }
}
