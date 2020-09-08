using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonChecker.CommonHelper
{
    public class ChromeHelper: ChromeDriver
    {
        private ChromeDriver _chromeDriver;

        public ChromeHelper()
        {
            _chromeDriver = InitWebDriver();
        }
        public ChromeDriver InitWebDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            var service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("start-maximized");
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddArguments("profile.default_content_setting_values.images", "2");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddUserProfilePreference("credentials_enable_service", true);
            var chromDriver = new ChromeDriver(service, chromeOptions);
            return chromDriver;
        }

        public IWebElement FindElement(string xpath)
        {
            IWebElement result = null;
            try
            {
                result = _chromeDriver.FindElement(By.XPath(xpath));
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public List<IWebElement> FindElements(string xpath)
        {
            var result = new List<IWebElement>();
            try
            {
                result = _chromeDriver.FindElements(By.XPath(xpath)).ToList();
                if (result.Any())
                {
                    result = result.Where(x => x.Displayed).ToList();
                }
            }
            catch
            {
            }

            return result;
        }
    }
}
