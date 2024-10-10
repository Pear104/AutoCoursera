using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace auto_coursera.Doing
{
    public class MarkDoing
    {
        public static void Mark(EdgeDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            try
            {
                // "Review assignments"
                //*[@id="main"]/div[1]/div[1]/div[1]/div[2]/div/div/div[2]/div/div/div[2]/button
                driver
                    .FindElement(
                        By.XPath(
                            "//*[@id=\"main\"]/div[1]/div[1]/div[1]/div[2]/div/div/div[2]/div/div/div[2]/button"
                        ),
                        5
                    )
                    .Click();
                Console.WriteLine("Clicked Review assignments");
            }
            catch { }

            // "Start reviewing"
            //*[@id="main"]/div[1]/div[1]/div[2]/button
            driver
                .FindElement(By.XPath("//*[@id=\"main\"]/div[1]/div[1]/div[2]/button"), 10000)
                .Click();
            Console.WriteLine("Clicked Start reviewing");

            // Options
            for (int i = 0; i < 4; i++)
            {
                MarkSingle(driver);
            }
        }

        public static void MarkSingle(EdgeDriver driver)
        {
            var options = driver.FindElements(
                By.CssSelector("div.rc-FormPart > div > div:nth-child(2) > div"),
                20
            );
            Console.WriteLine(options.Count());

            foreach (IWebElement element in options)
            {
                try
                {
                    element.Click();
                }
                catch
                {
                    Console.WriteLine("Element not clickable");
                }
            }

            try
            {
                var feedback = driver.FindElement(
                    By.XPath(
                        "//*[@id=\"main\"]/div[1]/div[1]/div[2]/div/div[3]/div/div[2]/div/div[6]/div/textarea"
                    )
                );
                feedback.SendKeys("ahihi do ngoc");
            }
            catch
            {
                Console.WriteLine("this test has no feedback");
            }

            var submit = driver.FindElement(
                By.XPath("//*[@id=\"main\"]/div[1]/div[1]/div[2]/div/div[5]/div[1]/button"),
                10
            );
            submit.Click();
            Thread.Sleep(2000);
        }
    }
}
