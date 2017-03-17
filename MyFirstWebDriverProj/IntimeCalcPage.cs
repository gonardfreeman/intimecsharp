using System;
using OpenQA.Selenium;
using NUnit.Framework;

namespace MyFirstWebDriverProj
{ 
    class IntimeCalcPage
    {
        private IWebDriver driver;
        public IntimeCalcPage(IWebDriver driver)
        {
            this.driver = driver;
        } 

        public void Navigate(string url)
        {
            this.driver.Url = url;
        }

        public string ReturnText(By by)
        {
            return driver.FindElement(by).Text;
        }

        public void ChooseCity(string city, By by_city, By by_li, By by_autocompl)
        {
            var fromInput = driver.FindElement(by_city);
            fromInput.SendKeys(city);
            if(driver.Waiter(by_li, 2))
            {
                var lis = driver.FindElements(by_autocompl);
                foreach (var item in lis)
                {
                    if (item.Text.Contains(city))
                    {
                        item.Click();
                    }
                }
            }
            else
            {
                Assert.Fail("Nothing find :(");
            }
        }

        public void FillVolumes(string vol, By by)
        {
            driver.FindElement(by).SendKeys(vol);
        }

        public void Clicker(By by)
        {
            driver.FindElement(by).Click();
        }

    }
}
