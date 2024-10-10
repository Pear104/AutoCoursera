using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace auto_coursera
{
    public class AssignmentDoing
    {
        public static void DoAssignment(EdgeDriver driver, string course)
        {
            foreach (var assignment in CourseData.All[course].Assignments)
            {
                if (assignment.Type == "file")
                    DoFileAssignment(driver, assignment);
                else
                    DoTextAssignment(driver, assignment);
            }
        }

        public static string tempText =
            "In the heart of the ancient, sprawling forest, where the towering trees whispered secrets to the wind and the dappled sunlight filtered through the dense canopy in shifting patterns of gold and green, a group of intrepid explorers, having traversed winding trails and crossed babbling brooks, finally stumbled upon a hidden, moss-covered ruin that had been lost to time, its crumbling stone walls adorned with intricate carvings that hinted at a long-forgotten civilization, while the air was filled with the sounds of chirping birds and the distant murmur of a waterfall, creating an atmosphere of mystical serenity that seemed to hold the promise of untold mysteries waiting to be uncovered by those brave enough to delve into its enigmatic depths";

        public static void DoFileAssignment(EdgeDriver driver, Assignment assignment)
        {
            Console.WriteLine(assignment.Url);
            driver.Navigate().GoToUrl(assignment.Url);

            driver.FindElements(By.CssSelector(".cds-tab-wrapper"), 1000)[1].Click();
            Console.WriteLine("Clicked My submission");
            try
            {
                driver
                    .FindElement(By.XPath("//*[@id=\"dialog-content\"]/div/div/input"), 1000)
                    .SendKeys("a");
                Console.WriteLine("Signature");

                driver
                    .FindElement(By.CssSelector(".css-1hllf5q > button:nth-child(1)"), 1000)
                    .Click();
                Console.WriteLine("Clicked Yes");
            }
            catch (Exception ex) { }

            driver.FindAndClick(By.CssSelector("#agreement-checkbox-base-label-text"), 1000);
            Console.WriteLine("Clicked Agreement");

            driver.FindElement(By.XPath("//*[@id=\"title\"]"), 1000).Clear();
            driver.FindElement(By.XPath("//*[@id=\"title\"]"), 1000).SendKeys("My project");
            Console.WriteLine("Filled title");
            driver.FindElement(By.CssSelector(".css-1pddt1l"), 1000).Click();
            driver
                .FindElements(By.CssSelector("input[type='file']"), 1000)[0]
                .SendKeys("D:\\downloads\\ENW492c.docx");
            Console.WriteLine("Filled file");
            Thread.Sleep(2000);

            Console.WriteLine("Started to click Submit");
            try
            {
                driver.FindAndClick(
                    By.CssSelector(
                        "div.rc-PeerItemVerificationSubmission > div.cds-1.css-ycvr1q > button.cds-105.cds-button-disableElevation.cds-button-primary.css-136aa4o"
                    ),
                    100
                );

                driver.FindAndClick(
                    By.CssSelector(
                        "div.css-1hllf5q > div > div > button.cds-105.cds-button-disableElevation.cds-button-primary.css-3uje6d"
                    ),
                    100
                );
            }
            catch (Exception ex) { }

            //Console.WriteLine("Clicked Submit");
            //for (int i = 0; i < 5; i++)
            //{
            //driver.FindElement(By.CssSelector(".css-n47tr8"), 2).Click();
            //driver.FindElement(By.CssSelector(".cds-172 > button:nth-child(2)"), 1000).Click();
            //Console.WriteLine("Clicked My submission");
            //}
            try
            {
                driver.FindAndClick(
                    By.CssSelector("div.cds-localNotification-actions button:nth-child(2)"),
                    1000
                );
                Console.WriteLine("Clicked Switch to peer grading");

                var elms = driver.FindElements(
                    By.CssSelector(".cds-checkboxAndRadio-labelText"),
                    1000
                );
                foreach (var e in elms)
                {
                    e.Click();
                    Console.WriteLine("Clicked Submit");
                }

                driver.FindAndClick(By.CssSelector(".cds-input-placeholder"), 10);
                Console.WriteLine("Form clicked");

                driver.FindElements(By.CssSelector(".cds-selectOption-container"), 10)[0].Click();
                Console.WriteLine("Choose from dropdown clicked");
                driver
                    .FindElement(By.CssSelector("input[placeholder=\"Enter text here\"]"), 10)
                    .SendKeys("ok");
                driver.FindElement(By.CssSelector(".css-1hllf5q button:nth-child(2)"), 20).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Timed out");
            }
        }

        public static void DoTextAssignment(EdgeDriver driver, Assignment assignment)
        {
            Console.WriteLine(assignment.Url);
            driver.Navigate().GoToUrl(assignment.Url);

            //Click "My submission"

            driver.FindElements(By.CssSelector(".cds-tab-wrapper"), 1000)[1].Click();
            Console.WriteLine("Clicked My submission");
            try
            {
                // Signature
                driver
                    .FindElement(By.XPath("//*[@id=\"dialog-content\"]/div/div/input"), 1)
                    .SendKeys("a");
                Console.WriteLine("Signature");

                // Click "Yes"
                driver
                    .FindElement(By.CssSelector(".css-1hllf5q > button:nth-child(1)"), 1)
                    .Click();
                Console.WriteLine("Clicked Yes");
            }
            catch (Exception ex) { }

            try
            {
                driver
                    .FindElements(By.CssSelector(".data-cml-editor-padding-container"), 1)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.Click();
                        x.SendKeys(tempText);
                    });
                driver
                    .FindElements(By.CssSelector("textarea.c-peer-review-submit-text-field"), 1)
                    .ToList()
                    .ForEach(x => x.SendKeys(tempText));
            }
            catch (Exception ex) { }

            driver.FindAndClick(By.CssSelector("#agreement-checkbox-base-label-text"), 1);
            Console.WriteLine("Clicked Agreement");

            driver.FindElement(By.XPath("//*[@id=\"title\"]"), 1).Clear();
            driver.FindElement(By.XPath("//*[@id=\"title\"]"), 1).SendKeys("My project");
            Console.WriteLine("Filled title");

            Console.WriteLine("Started to click Submit");
            try
            {
                driver.FindAndClick(
                    By.CssSelector(
                        "div.rc-PeerItemVerificationSubmission > div.cds-1.css-ycvr1q > button.cds-105.cds-button-disableElevation.cds-button-primary.css-136aa4o"
                    ),
                    1
                );

                driver.FindAndClick(
                    By.CssSelector(
                        "div.css-1hllf5q > div > div > button.cds-105.cds-button-disableElevation.cds-button-primary.css-3uje6d"
                    ),
                    1
                );
            }
            catch (Exception ex) { }

            //Console.WriteLine("Clicked Submit");
            driver.FindElement(By.CssSelector(".cds-172 > button:nth-child(2)"), 1).Click();
            driver.FindElement(By.CssSelector(".css-n47tr8"), 1).Click();
            Console.WriteLine("Clicked My submission");
            try
            {
                driver.FindAndClick(
                    By.CssSelector("div.cds-localNotification-actions button:nth-child(2)"),
                    1000
                );
                Console.WriteLine("Clicked Switch to peer grading");

                var elms = driver.FindElements(
                    By.CssSelector(".cds-checkboxAndRadio-labelText"),
                    1000
                );
                foreach (var e in elms)
                {
                    e.Click();
                    Console.WriteLine("Clicked Submit");
                }

                driver.FindAndClick(By.CssSelector(".cds-input-placeholder"), 10);
                Console.WriteLine("Form clicked");

                driver.FindElements(By.CssSelector(".cds-selectOption-container"), 10)[0].Click();
                Console.WriteLine("Choose from dropdown clicked");
                driver
                    .FindElement(By.CssSelector("input[placeholder=\"Enter text here\"]"), 10)
                    .SendKeys("ok");
                driver.FindElement(By.CssSelector(".css-1hllf5q button:nth-child(2)"), 20).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Timed out");
            }
        }
    }
}
