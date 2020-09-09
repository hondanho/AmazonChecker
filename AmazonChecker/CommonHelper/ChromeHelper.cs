using OpenQA.Selenium.Chrome;

namespace AmazonChecker.CommonHelper
{
    public static class ChromeHelper
    {
        public static ChromeDriver InitWebDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            //chromeOptions.AddArguments("headless");
            //chromeOptions.AddArguments("start-maximized");
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
    }
}
