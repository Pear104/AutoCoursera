using auto_coursera.Doing;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace auto_coursera
{
    internal class Coursera
    {
        public static EdgeDriver driver;

        static Coursera()
        {
            var options = new EdgeOptions();
            options.AddArgument("start-maximized"); // Example option: start the browser maximized
            options.AddArgument("--disable-popup-blocking"); // Example option: disable popup blocking

            driver = new EdgeDriver(options);
        }

        public static void Login(string email, string password)
        {
            driver.Navigate().GoToUrl(CourseData.All["ITE302c"].QuizUrls[0]);

            driver.FindElement(By.XPath("//*[@id=\"cds-react-aria-1\"]"), 10000).SendKeys(email);
            driver.FindElement(By.XPath("//*[@id=\"cds-react-aria-2\"]"), 10000).SendKeys(password);

            // Click login
            driver
                .FindElement(
                    By.XPath("/html/body/div[4]/div/div/section/section/div[1]/form/div[3]/button"),
                    10000
                )
                .Click();

            // Close the popup
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

        public static void DoCourse(string course)
        {
            foreach (var quizUrl in CourseData.All[course].QuizUrls)
            {
                QuizDoing.DoSingleQuiz(driver, course, quizUrl);
            }

            foreach (var assignment in CourseData.All[course].Assignments)
            {
                AssignmentDoing.DoAssignment(driver, course);
            }

            foreach (var assignment in CourseData.All[course].Assignments)
            {
                MarkDoing.Mark(driver, assignment.Url + "/give-feedback");
            }
        }
    }
}
