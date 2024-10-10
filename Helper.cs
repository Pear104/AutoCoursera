namespace auto_coursera
{
    public class Course
    {
        public string[] QuizUrls { get; set; }
        public Assignment[] Assignments { get; set; }
    }

    public class Assignment
    {
        public Assignment(string url, string type)
        {
            Url = url;
            Type = type;
        }

        public string Url { get; set; }
        public string Type { get; set; }
    }

    public class Quiz
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    internal class Helper
    {
        public static List<Quiz> ReadKey(string filepath)
        {
            List<Quiz> keys = new List<Quiz>();
            try
            {
                string[] lines = File.ReadAllLines(filepath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    if (parts.Length == 2)
                    {
                        string question = parts[0].Trim();
                        string answer = parts[1].Trim();
                        keys.Add(new Quiz() { Answer = answer, Question = question });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            return keys;
        }
    }
}
