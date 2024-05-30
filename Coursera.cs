using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace auto_coursera
{
    internal class Coursera
    {
        public static EdgeDriver driver = new EdgeDriver();

        public static void Login(string email, string password)
        {
            driver.Navigate().GoToUrl(CourseData.All["ITE302c"]["MOC" + 1]["WEEK" + 1]);

            driver.FindElement(By.Id("email"), 10000).SendKeys(email);
            driver.FindElement(By.Id("password"), 10000).SendKeys(password);

            // Click login
            driver
                .FindElement(
                    By.XPath("/html/body/div[4]/div/div/section/section/div[1]/form/div[3]/button"),
                    10000
                )
                .Click();

            // Close the popup
            //Thread.Sleep(20);
            try
            {
                driver
                    .FindElement(
                        By.XPath(
                            //*[@id="cds-react-aria-12"]/div[3]/div/div/div[2]/div[3]/div/button
                            "//*[@class=\"css-1gq8bzo\"]/div[3]/div/div/div[2]/div[3]/div/button"
                        ),
                        100
                    )
                    .Click();
            }
            catch { }
        }

        public static void DoSingleQuiz(string course, int mooc, int week)
        {
            driver.Navigate().GoToUrl(CourseData.All[course]["MOC" + mooc]["WEEK" + week]);
            driver
                .FindElement(
                    By.XPath("//*[@id=\"main\"]/div[1]/div[3]/div[1]/div[2]/div/div/div/div/div"),
                    10000
                )
                .Click();

            var testOption = driver.FindElements(By.ClassName("rc-Option"), 10000);
            var key = Helper.ReadKey($"key/{course}DB/MOC{mooc}WEEK{week}.txt");
            foreach (IWebElement element in testOption)
            {
                if (key.Contains(element.Text.Trim()))
                {
                    Console.WriteLine(element.Text);
                    element.Click();
                }
            }
            driver
                .FindElement(
                    By.XPath(
                        "//*[@id=\"TUNNELVISIONWRAPPER_CONTENT_ID\"]/div[2]/div/div/div/div/div/div/div[2]/div/div[1]/div/div[2]/div[1]/div/div"
                    ),
                    10000
                )
                .Click();
            driver
                .FindElement(
                    By.XPath(
                        "//*[@id=\"TUNNELVISIONWRAPPER_CONTENT_ID\"]/div[2]/div/div/div/div/div/div/div[2]/div/div[2]/button[1]"
                    ),
                    10000
                )
                .Click();
            driver
                .FindElement(
                    By.XPath(
                        "//*[@id=\"TUNNELVISIONWRAPPER_CONTENT_ID\"]/div[1]/div/div/div/div/div/div/div[2]/div/button"
                    ),
                    10000
                )
                .Click();
        }

        public static void DoMOOC(string course, int mooc)
        {
            foreach (var week in CourseData.All[course]["MOC" + mooc])
            {
                Console.WriteLine(int.Parse(week.Key.Last().ToString()));
                DoSingleQuiz("ITE302c", mooc, int.Parse(week.Key.Last().ToString()));
            }
        }

        public static void DoCourse(string course)
        {
            foreach (var mooc in CourseData.All[course])
            {
                DoMOOC("ITE302c", int.Parse(mooc.Key.Last().ToString()));
            }
        }

        public static void Mark(int mooc)
        {
            driver
                .Navigate()
                .GoToUrl(
                    "https://www.coursera.org/learn/ethical-frameworks-action/peer/B8Wrx/ethical-review/submit"
                //"https://www.coursera.org/learn/promote-ethical-data-driven-technologies/peer/MneNm/op-ed-article/submit"
                );
            // "Review assignments"
            //*[@id="main"]/div[1]/div[1]/div[1]/div[2]/div/div/div[2]/div/div/div[2]/button
            driver
                .FindElement(
                    By.XPath(
                        "//*[@id=\"main\"]/div[1]/div[1]/div[1]/div[2]/div/div/div[2]/div/div/div[2]/button"
                    ),
                    10000
                )
                .Click();
            Console.WriteLine("Clicked Review assignments");

            // "Start reviewing"
            //*[@id="main"]/div[1]/div[1]/div[2]/button
            driver
                .FindElement(By.XPath("//*[@id=\"main\"]/div[1]/div[1]/div[2]/button"), 10000)
                .Click();
            Console.WriteLine("Clicked Start reviewing");

            // Options
            for (int i = 0; i < 5; i++)
            {
                MarkSingle();
            }
        }

        public static void MarkSingle()
        {
            var options = driver.FindElements(
                By.CssSelector("div.rc-FormPart > div > div:nth-child(2) > div"),
                20
            );
            Console.WriteLine(options.Count());

            foreach (IWebElement element in options)
            {
                element.Click();
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
            Thread.Sleep(5000);
        }
    }
}
