using Drips.Selenium.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drips.Tests
{
   
    internal class HelloWorld : BaseTest
    {
        [Test]
        public void HelloWorldTest()
        {
            Console.WriteLine("Rachel");

            var search = new SearchResultsPage(driver);
            search.SearchForItem("pants");
        }
    }
}
