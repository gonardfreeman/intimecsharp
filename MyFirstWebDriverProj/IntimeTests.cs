using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace MyFirstWebDriverProj
{
    [TestClass]
    public class IntimeTests
    {
        public IWebDriver Driver { get; set; }
        
        [TestInitialize]
        public void SetupTest()
        {
            this.Driver = new FirefoxDriver();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }

        [TestMethod]
        public void SearchTestField()
        {
            IntimeMainPage intimeMainPage = new IntimeMainPage(this.Driver);
            intimeMainPage.Navigate("http://intime.ua");
        }
    }
}
