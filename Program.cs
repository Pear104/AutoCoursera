namespace auto_coursera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            Coursera.Login(Config.email, Config.password);

            Coursera.DoCourse("ENW492c");
            //Coursera.DoAssignment("ENW492c");
            //Coursera.DoSingleQuiz("ENW492c", CourseData.All["ENW492c"].QuizUrls[0]);
            //Coursera.MarkCourse("WDU203c");
        }
    }
}
