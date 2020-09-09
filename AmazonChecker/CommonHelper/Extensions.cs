using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;

namespace AmazonChecker.CommonHelper
{
    public static class Extensions
    {
        public static IWebElement FindWebElement(this ChromeDriver chromeDriver, string xPath)
        {
            if (chromeDriver == null || xPath == null) return null;
            try
            {
                return chromeDriver.FindElement(By.XPath(xPath));
            }
            catch
            {
                return null;
            }
        }

        public static List<IWebElement> FindWebElements(this ChromeDriver chromeDriver, string xPath)
        {
            if (chromeDriver == null || xPath == null) return null;
            try
            {
                return chromeDriver.FindElements(By.XPath(xPath)).Where(x => x.Displayed).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
