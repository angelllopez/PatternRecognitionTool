﻿using System;
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
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            if (!File.Exists(inputFilePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input file does not exist.");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            if (!File.Exists(patternFilePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pattern file does not exist.");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            try
            {
                // Creates or Clear output file if it exists.
                File.WriteAllText(outputFilePath, string.Empty);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }

            // Read multiline pattern from file.
            string[] patterns = File.ReadAllLines(patternFilePath);

            // Check if input file is empty.
            if (new FileInfo(inputFilePath).Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input file is empty.");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            // Check if patterns array is empty.
            if (patterns.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pattern file is empty.");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            // Check if patterns array contain valid regular expressions.
            foreach (string pattern in patterns)
            {
                try
                {
                    Regex.Match("", pattern);
                }
                catch (ArgumentException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pattern is not valid.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                    return;
                }
            }

            try
            {

                using (StreamReader input = new StreamReader(inputFilePath))
                using (StreamWriter output = new StreamWriter(outputFilePath, true))
                {
                    // loop through each pattern in the patterns array.
                    foreach (string pattern in patterns)
                    {
                        string line;

                        // Read line by line and check if it matches the pattern.
                        while ((line = input.ReadLine()) != null)
                        {
                            if (Regex.IsMatch(line, pattern))
                            {
                                output.WriteLine(line);
                            }
                        }

                        // Reset the stream position to the beginning of the file.
                        input.BaseStream.Position = 0;
                        input.DiscardBufferedData();
                    }
                }

                Console.WriteLine("Pattern recognition completed successfully.");

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
