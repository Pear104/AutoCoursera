using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace auto_coursera.Doing
{
    public class QuizDoing
    {
        public static void DoSingleQuiz(EdgeDriver driver, string course, string quizUrl)
        {
            driver.Navigate().GoToUrl(quizUrl);
            Console.WriteLine(quizUrl);
            // Click on continue button
            driver
                .FindElement(
                    By.XPath("//*[@id=\"main\"]/div[1]/div[3]/div[1]/div[2]/div/div/div/div/div"),
                    10000
                )
                .Click();

            var questions = driver.FindElements(By.ClassName("rc-FormPartsQuestion"), 10000);

            // The keys read from file
            var keys = Helper.ReadKey($"key/{course}.txt");
            var i = 1;
            foreach (IWebElement question in questions)
            {
                Console.WriteLine(i);
                i++;
                var questionText = string.Join(
                        "",
                        question
                            .FindElements(
                                By.CssSelector(
                                    "div:nth-child(1) > div.rc-FormPartsQuestion__contentCell p"
                                )
                            )
                            .Select(q => q.Text.Trim())
                    )
                    .Replace("“", "\"")
                    .Replace("”", "\"")
                    .Replace("’", "'")
                    .Replace("‛", "'");
                var questionTexts = question
                    .FindElements(
                        By.CssSelector("div:nth-child(1) > div.rc-FormPartsQuestion__contentCell p")
                    )
                    .Select(q => q.Text.Trim());
                Console.WriteLine("===================================");
                Console.WriteLine(questionText);
                Console.WriteLine("===================================");
                questionTexts.ToList().ForEach(q => Console.WriteLine(q));
                // Check if the current question extracted from Selenium already exist in my keys file or not
                var quesFounds = keys.FindAll(key =>
                    key.Question.Contains(string.Join("", questionText))
                );
                Console.WriteLine("Found: " + quesFounds.Count);
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
                                quesFound.Answer.Contains(answerText)
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

            // Click agree button
            driver
                .FindElement(
                    By.CssSelector(
                        "#agreement-checkbox-base-label-text"
                    //"//*[@id=\"cds-react-aria-84-panel-submission\"]/div/div/div/div[3]/div[1]/div[2]/div[1]/div/div/label/div"
                    ),
                    10000
                )
                .Click();

            // Click submit button
            driver.FindElement(By.CssSelector(".css-oc6ooc .css-ra3hwj"), 10000).Click();

            // Click "Next" button
            driver
                .FindElement(
                    By.XPath(
                        "//*[@id=\"TUNNELVISIONWRAPPER_CONTENT_ID\"]/div[1]/div/div/div/div/div/div/div[2]/div/button"
                    ),
                    10000
                )
                .Click();
        }
    }
}
