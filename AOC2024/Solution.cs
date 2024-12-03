using System;
using System.IO;
using System.Text;

namespace AOC2024
{
    abstract class Solution
    {
        protected abstract int Day { get; }
        protected abstract string Name { get; }

        public void Run()
        {
            PrintTitle(out var footer);

            var input = GetInput();

            if(string.IsNullOrEmpty(input))
            {
                Console.Error.WriteLine("Could not retrieve input text. Exiting...");
                return;
            }

            Console.WriteLine($" Part 1 Result:\n {RunPart1(input)}");
            Console.WriteLine(" --------------");
            Console.WriteLine($" Part 2 Result:\n {RunPart2(input)}");
            Console.WriteLine(footer);
        }

        private void PrintTitle(out string footer)
        {
            var title = $"Day {Day}: '{Name}'";

            var decorationBuilder = new StringBuilder(title.Length + 5);
            decorationBuilder.Append("*-");
            for(var i = 0; i < title.Length / 3 - 1; ++i)
            {
                decorationBuilder.Append("*--");
            }
            if(title.Length % 3 > 0)
            {
                decorationBuilder.Append("*--");
            }
            decorationBuilder.Append("*-*");
            Console.WriteLine(decorationBuilder.ToString());
            Console.WriteLine($" {title}");

            decorationBuilder.Clear();

            decorationBuilder.Append("*-");
            for (var i = 0; i < title.Length - 2; ++i)
            {
                decorationBuilder.Append("-");
            }
            decorationBuilder.Append("-*");
            footer = decorationBuilder.ToString();
            Console.WriteLine(footer);
        }

        private string GetInput()
        {
            var input = string.Empty;
            var filepath = $"./Solutions/{Day.ToString("00")}/input.txt";

            try
            {
                input = File.ReadAllText(filepath);
            }
            catch(FileNotFoundException fnfe)
            {
                Console.Error.WriteLine($"File at path '{filepath}' does not exist!\nMake sure to add input.txt before running solution!!!");
                Console.Error.WriteLine($"Full error text:\n{fnfe.Message}");
            }
            catch(Exception e)
            {
                Console.Error.WriteLine($"Encountered an unknown error attempting to get input at file '{filepath}'.");
                Console.Error.WriteLine($"Full error text:\n{e.Message}");
            }

            return input;
        }

        protected abstract string RunPart1(string input);
        protected abstract string RunPart2(string input);
    }
}
