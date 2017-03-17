using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyFirstWebDriverProj
{
    public static class PageMethods
    {
        public static bool Waiter(this IWebDriver driver, By by, int timeoutInSeconds, string text = "")
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            var el = wait.Until(drv => drv.FindElement(by));
            if(el.Text.Length > 0)
            {
                Console.WriteLine(el.Text.Length);
                return true;
            }
            else
            {
                if(wait.Until(ExpectedConditions.TextToBePresentInElement(el, text)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
