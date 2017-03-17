using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace MyFirstWebDriverProj
{
    /// <summary>
	/// 
	/// </summary>
	/// 
    [TestFixture]
    public class IntimeTests
    {
        public IWebDriver Driver { get; set; }
        private string baseURL;
        /// <summary>
        /// 
        /// </summary>
        /// 
        [SetUp]
        public void SetupTest()
        {
            this.Driver = new ChromeDriver(@"C:\Users\ho-bondarenko\Documents\python\selenium\intime");
            this.Driver.Manage().Window.Maximize();
            baseURL = "http://intime.ua";
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [TearDown]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Test]
        public void MainPageAsserts()
        {
            IntimeMainPage intimeMainPage = new IntimeMainPage(this.Driver);
            intimeMainPage.Navigate(baseURL);
            string checkIt = intimeMainPage.AssertText(By.XPath("/html/body/div[2]/nav/div[3]/div[1]/ul/li[1]/a"));
            Assert.AreEqual("Стоимость доставки", checkIt, "There must be text Стоимость доставки1 and text is "+ checkIt);
            Console.WriteLine("Test passed");

        }

        [TestCase(ExpectedResult = "Отделения и почтоматы",Author = "D.Bondarenko",TestName = "Check presention")]
        public string CheckPresence()
        {
            IntimeMainPage intimeMainPage = new IntimeMainPage(this.Driver);
            intimeMainPage.Navigate(baseURL);
            string checkIt = intimeMainPage.AssertText(By.XPath("/html/body/div[2]/nav/div[3]/div[1]/ul/li[2]/a"));
            Assert.AreEqual("Отделения и почтоматы", checkIt, "There must be text Стоимость доставки1 and text is " + checkIt);
            Console.WriteLine("Test passed");
            return checkIt;
        }

        [TestCase("Киев", "Харьков", "29", "32", "32", "32", TestName = "Positive VW<30 W<30", Author = "D. Bondarenko", ExpectedResult = "80")]
        public string CalcTest(string from, string to, string wgh, string L, string Wdth, string H)
        {
            IntimeCalcPage intimeCalcPage = new IntimeCalcPage(this.Driver);
            intimeCalcPage.Navigate(baseURL + "/ru-calc");
            intimeCalcPage.ChooseCity(from, By.XPath("//input[@name='from']"), By.XPath("(//div[contains(@class,'cities')])[1]/ul//li"), By.XPath("(//div[contains(@class,'cities')])[1]/ul/li"));
            intimeCalcPage.ChooseCity(to, By.XPath("//input[@name='to']"), By.XPath("(//div[contains(@class,'cities')])[2]/ul//li"), By.XPath("(//div[contains(@class,'cities')])[2]/ul/li"));
            intimeCalcPage.FillVolumes(wgh, By.XPath("//input[@name='weight']"));
            intimeCalcPage.FillVolumes(L, By.XPath("//input[@name='dimension_depth']"));
            intimeCalcPage.FillVolumes(Wdth, By.XPath("//input[@name='dimension_width']"));
            intimeCalcPage.FillVolumes(H, By.XPath("//input[@name='dimension_height']"));
            if (Driver.Waiter(By.XPath("//li[@class='message-text-right']/p[contains(@class,'block-message-result')]/span"),6,"80"))
            {
                string result = intimeCalcPage.ReturnText(By.XPath("//li[@class='message-text-right']/p[contains(@class,'block-message-result')]/span"));
                Assert.AreEqual("80", result, "Expected 80, result " + result);
                Console.WriteLine("Test Data: from---"+from+"; to---"+to+"; weight---"+wgh+"; Long---"+L+"; Width---"+Wdth+"; Height---"+H);
                Console.WriteLine("Test passed, result is " + result);
                return result;
            }
            else
            {
                Console.WriteLine("Test failed, Oracle reponse more than 2 seconds");
                return "Test failed, Oracle reponse more than 2 seconds";
            }
            
        }
    }
}
