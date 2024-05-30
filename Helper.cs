using OpenQA.Selenium.Edge;

namespace auto_coursera
{
    internal class Helper
    {
        public static EdgeDriver driver = new EdgeDriver();

        public static List<string> ReadKey(string filepath)
        {
            List<string> key = new List<string>();
            try
            {
                string[] lines = File.ReadAllLines(filepath);
                key = lines.Select(x => x.Trim()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            return key;
        }
    }
}
