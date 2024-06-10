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

            Coursera.DoCourse("ITE302c");
            //Coursera.MarkCourse("WDU203c");
        }
    }
}
