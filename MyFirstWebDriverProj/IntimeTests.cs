using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace MyFirstWebDriverProj
{
    [TestFixture]
    public class IntimeTests
    {
        public IWebDriver Driver { get; set; }
        private string baseURL;

        [SetUp]
        public void SetupTest()
        {
            this.Driver = new FirefoxDriver();
            baseURL = "http://intime.ua";
        }

        [TearDown]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }

        [Test]
        public void MainPageAsserts()
        {
            IntimeMainPage intimeMainPage = new IntimeMainPage(this.Driver);
            intimeMainPage.Navigate(baseURL);
            string checkIt = intimeMainPage.AssertText(By.XPath("/html/body/div[2]/nav/div[3]/div[1]/ul/li[1]/a"));
            Assert.AreEqual("Стоимость доставки", checkIt);
        }
    }
}
