using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace auto_coursera
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(
            this IWebDriver driver,
            By by,
            int timeoutInSeconds
        )
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv =>
                    (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null
                );
            }
            return driver.FindElements(by);
        }

        public static void RemovePopup(this IWebDriver driver, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    wait.Until(
                            ExpectedConditions.ElementToBeClickable(
                                By.XPath(
                                    "//*[@id=\"rendered-content\"]/div/div[1]/div[1]/div/div/div/div/div/div[1]/button"
                                )
                            )
                        )
                        ?.Click();
                }
                catch { }
            }
        }

        //*[@id="rendered-content"]/div/div[1]/div[1]/div/div/div/div/div/div[1]/button
    }
}
