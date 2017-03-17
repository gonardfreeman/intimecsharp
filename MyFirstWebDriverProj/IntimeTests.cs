using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


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
            this.Driver = new ChromeDriver(@"C:\Users\ho-bondarenko\Documents\python\selenium\intime");
            this.Driver.Manage().Window.Maximize();
            baseURL = "http://intime.ua";
        }

        [TearDown]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }

        [TestCase("//li[@class=' menu--map']/a", "Отделения и почтоматы", Author = "D. Bondarenko", ExpectedResult = "Отделения и почтоматы", TestName = "Testing of text(Отделения и почтоматы)")]
        [TestCase("//li[@class=' menu--calc']/a", "Стоимость доставки", Author = "D. Bondarenko", ExpectedResult = "Стоимость доставки", TestName = "Testing of text(Стоимость доставки)")]
        public string CheckText(string path, string text)
        {
            IntimeMainPage intimeMainPage = new IntimeMainPage(this.Driver);
            intimeMainPage.Navigate(baseURL);
            string checkIt = intimeMainPage.AssertText(By.XPath(path));
            Assert.AreEqual(text, checkIt, "There must be text "+text+" and text is "+ checkIt);
            Console.WriteLine("Test passed! Text is "+ checkIt);
            return checkIt;
        }

        [TestCase("Киев", "Харьков", "31", "86", "86", "86", "386", TestName = "Positive VW>30 W>30", Author = "D. Bondarenko", ExpectedResult = "386")]
        [TestCase("Киев", "Харьков", "2", "86", "86", "86", "386", TestName = "Positive VW>30 W<30", Author = "D. Bondarenko", ExpectedResult = "386")]
        [TestCase("Киев", "Харьков", "30", "32", "32", "32", "80", TestName = "Positive VW<30 W=30", Author = "D. Bondarenko", ExpectedResult = "80")]
        [TestCase("Киев", "Харьков", "29", "32", "32", "32", "80", TestName = "Positive VW<30 W<30", Author = "D. Bondarenko", ExpectedResult = "80")]
        public string CalcTest(string from, string to, string wgh, string L, string Wdth, string H, string res)
        {
            IntimeCalcPage intimeCalcPage = new IntimeCalcPage(this.Driver);
            intimeCalcPage.Navigate(baseURL + "/ru-calc");
            intimeCalcPage.ChooseCity(from, By.XPath("//input[@name='from']"), By.XPath("(//div[contains(@class,'cities')])[1]/ul//li"), By.XPath("(//div[contains(@class,'cities')])[1]/ul/li"));
            intimeCalcPage.ChooseCity(to, By.XPath("//input[@name='to']"), By.XPath("(//div[contains(@class,'cities')])[2]/ul//li"), By.XPath("(//div[contains(@class,'cities')])[2]/ul/li"));
            intimeCalcPage.FillVolumes(wgh, By.XPath("//input[@name='weight']"));
            intimeCalcPage.FillVolumes(L, By.XPath("//input[@name='dimension_depth']"));
            intimeCalcPage.FillVolumes(Wdth, By.XPath("//input[@name='dimension_width']"));
            intimeCalcPage.FillVolumes(H, By.XPath("//input[@name='dimension_height']"));
            intimeCalcPage.Clicker(By.XPath("//body"));
            if (Driver.WaiterText(By.XPath("//li[@class='message-text-right']/p[contains(@class,'block-message-result')]/span"),8, res))
            {
                string result = intimeCalcPage.ReturnText(By.XPath("//li[@class='message-text-right']/p[contains(@class,'block-message-result')]/span"));
                Assert.AreEqual(res, result, "Expected "+res+", result " + result);
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

        [TestCaseSource(typeof(TestData), "IntimeCalcDataNegative")]
        public void NegativeCalcTests(string from, string to, string wgh, string L, string Wdth, string H, string res)
        {
            IntimeCalcPage intimeCalcPage = new IntimeCalcPage(this.Driver);
            intimeCalcPage.Navigate(baseURL + "/ru-calc");
            intimeCalcPage.ChooseCity(from, By.XPath("//input[@name='from']"), By.XPath("(//div[contains(@class,'cities')])[1]/ul//li"), By.XPath("(//div[contains(@class,'cities')])[1]/ul/li"));
            intimeCalcPage.ChooseCity(to, By.XPath("//input[@name='to']"), By.XPath("(//div[contains(@class,'cities')])[2]/ul//li"), By.XPath("(//div[contains(@class,'cities')])[2]/ul/li"));
            intimeCalcPage.FillVolumes(wgh, By.XPath("//input[@name='weight']"));
            intimeCalcPage.FillVolumes(L, By.XPath("//input[@name='dimension_depth']"));
            intimeCalcPage.FillVolumes(Wdth, By.XPath("//input[@name='dimension_width']"));
            intimeCalcPage.FillVolumes(H, By.XPath("//input[@name='dimension_height']"));
            intimeCalcPage.Clicker(By.XPath("//body"));
            if (Driver.WaiterText(By.XPath("//li[@class='message-text-right']/p[contains(@class,'block-message-result')]/span"), 10, res))
            {
                string result = intimeCalcPage.ReturnText(By.XPath("//li[@class='message-text-right']/p[contains(@class,'block-message-result')]/span"));
                Assert.AreNotEqual(res, result, "Expected " + res + ", result " + result);
                Console.WriteLine("Test Data: from---" + from + "; to---" + to + "; weight---" + wgh + "; Long---" + L + "; Width---" + Wdth + "; Height---" + H);
                Console.WriteLine("Test passed, result is " + result);
            }
        }

    }
}
