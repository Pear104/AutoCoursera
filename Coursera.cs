using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace auto_coursera
{
    internal class Coursera
    {
        public static EdgeDriver driver = new EdgeDriver();

        public static void Login(string email, string password)
        {
            driver.Navigate().GoToUrl(CourseData.All["ITE302c"]["MOOC" + 1]["WEEK" + 1]);

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

        public static string NormalizeString(string input)
        {
            return new string(input.Where(c => !char.IsPunctuation(c) || c == '\"').ToArray())
                .ToLower()
                .Trim();
        }

        public static void DoSingleQuiz(string course, int mooc, int week)
        {
            driver.Navigate().GoToUrl(CourseData.All[course]["MOOC" + mooc]["WEEK" + week]);

            // Click on continue button
            driver
                .FindElement(
                    By.XPath("//*[@id=\"main\"]/div[1]/div[3]/div[1]/div[2]/div/div/div/div/div"),
                    10000
                )
                .Click();

            var questions = driver.FindElements(By.ClassName("rc-FormPartsQuestion"), 10000);

            // The keys read from file
            var keys = Helper.ReadKey($"key/{course}DB/questions_and_keys.txt");

            foreach (IWebElement question in questions)
            {
                var questionText = question
                    .FindElement(
                        By.CssSelector(
                            "div:nth-child(1) > div.rc-FormPartsQuestion__contentCell p:nth-child(1)"
                        )
                    )
                    .Text.Trim()
                    .Replace("“", "\"")
                    .Replace("”", "\"")
                    .Replace("’", "'")
                    .Replace("‛", "'");
                Console.WriteLine("===================================================");
                Console.WriteLine($"testQs-{questionText}-");

                // Check if the current question extracted from Selenium already exist in my keys file or not
                var quesFounds = keys.FindAll(key => key.Question.Contains(questionText));
                //if (quesFound != null)
                //{
                //    Console.WriteLine($"sheetQs-{quesFound.Question}-");
                //    Console.WriteLine($"sheetAns-{quesFound.Answer}-");
                //}
                var answers = question.FindElements(By.ClassName("rc-Option"));
                foreach (IWebElement answer in answers)
                {
                    Console.WriteLine($"testAns-{answer.Text.Trim()}-");
                    var answerText = answer
                        .Text.Trim()
                        .Replace("“", "\"")
                        .Replace("”", "\"")
                        .Replace("’", "'")
                        .Replace("‛", "'");
                    var autoTrueText =
                        "Yes, I have completed reviews of the work of 3 peers for each prompt in the preceding assignment.";
                    if (quesFounds.Count > 0)
                    {
                        foreach (var quesFound in quesFounds)
                        {
                            if (
                                (quesFound.Answer.Contains(answerText))
                                || answerText.Contains(autoTrueText)
                            )
                            {
                                Console.WriteLine($"key-{answerText}-");
                                answer.Click();
                            }
                        }
                    }
                    else
                    {
                        answer.Click();
                    }
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
            foreach (var week in CourseData.All[course]["MOOC" + mooc])
            {
                Console.WriteLine(int.Parse(week.Key.Last().ToString()));
                DoSingleQuiz(course, mooc, int.Parse(week.Key.Last().ToString()));
            }
        }

        public static void DoCourse(string course)
        {
            foreach (var mooc in CourseData.All[course])
            {
                DoMOOC(course, int.Parse(mooc.Key.Last().ToString()));
            }
        }

        public static void MarkCourse(string course)
        {
            foreach (var url in MarkData.All[course])
            {
                Mark(url);
            }
        }

        public static void Mark(string url)
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
            for (int i = 0; i < 6; i++)
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
            Thread.Sleep(5000);
        }
    }
}
