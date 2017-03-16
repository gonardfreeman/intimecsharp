using OpenQA.Selenium;

namespace MyFirstWebDriverProj
{
    class IntimeMainPage
    {
        private IWebDriver driver;

        public IntimeMainPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate(string url)
        {
            this.driver.Url = url;
        }
        public string AssertText(By by)
        {
            string text = this.driver.FindElement(by).Text;
            return text;
        }
    }
}
