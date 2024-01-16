using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PatternRecognitionTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Using Path.Combine to build paths for better cross-platform support.
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string inputFilePath = Path.Combine(baseDirectory, "input.txt");
            string outputFilePath = Path.Combine(baseDirectory, "output.txt");
            string patternFilePath = Path.Combine(baseDirectory, "pattern.txt");

            // Start of the program.
            Console.CursorVisible = false;
            Console.WriteLine("Pattern Recognition Tool");
            Console.WriteLine("============================================");

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            // Move the cursor to the beginning of the previous line
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Input file does not exist.");
                return;
            }

            if (!File.Exists(patternFilePath))
            {
                Console.WriteLine("Pattern file does not exist.");
                return;
            }

            // Creates or Clear output file if it exists.
            File.WriteAllText(outputFilePath, string.Empty);

            string pattern = File.ReadAllText(patternFilePath);

            // Check if input file is empty.
            if (new FileInfo(inputFilePath).Length == 0)
            {
                Console.WriteLine("Input file is empty.");
                return;
            }

            // Check if pattern is empty.
            if (string.IsNullOrEmpty(pattern))
            {
                Console.WriteLine("Pattern is empty.");
                return;
            }

            // Check if pattern is valid.
            try
            {
                Regex.Match("", pattern);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Pattern is not valid.");
                return;
            }

            try
            {
                using (StreamReader input = new StreamReader(inputFilePath))
                using (StreamWriter output = new StreamWriter(outputFilePath, true))
                {
                    string line;

                    while ((line = input.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, pattern))
                        {
                            output.WriteLine(line);
                        }
                    }
                }

                Console.WriteLine("Pattern recognition completed successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
