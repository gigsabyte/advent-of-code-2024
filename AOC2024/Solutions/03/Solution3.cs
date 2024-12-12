using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024.Solutions
{
    class Solution3 : Solution
    {
        protected override int Day => 3;

        protected override string Name => "Mull It Over";

        protected override string Part1ExpectedResult => "161";

        protected override string Part2ExpectedResult => "48";

        protected override string RunPart1(string input)
        {
            return ParseOperations(input).ToString();
        }

        protected override string RunPart2(string input)
        {
            return ParseOperations(input, true).ToString();
        }


        /// <summary>
        /// Shared logic for parsing mul() operations from the input
        /// </summary>
        /// <param name="input">String parsed from input.txt</param>
        /// <param name="parseDos">Whether to parse do()/don't()</param>
        /// <returns></returns>
        private int ParseOperations(string input, bool parseDos = false)
        {
            // Init
            var result = 0;

            // Pattern:
            // mul\(([0-9]+),([0-9]+)\) <- multiply X and Y (but for our purposes just add them to the list
            // do\(\) <- enable
            // don't\(\) <- disable
            var multPattern = @"mul\(([0-9]+),([0-9]+)\)|do\(\)|don't\(\)";

            // Get matches
            var regex = new Regex(multPattern);
            var match = regex.Match(input);
            var enabled = true;

            // Process each regex match
            while(match.Success)
            {
                // Filter out do's and don'ts
                if (match.Value == "do()" || match.Value == "don't()")
                {
                    if (parseDos)
                    {
                        enabled = match.Length == 4;
                    }
                    match = match.NextMatch();
                    continue;
                }

                // If we care about do()/don't(), skip mul() operations while disabled
                if(parseDos && !enabled)
                {
                    match = match.NextMatch();
                    continue;
                }

                // Parse out the ints from the operation
                var first = int.Parse(match.Groups[1].Value);
                var second = int.Parse(match.Groups[2].Value);

                // Multiply and add to result
                result += first * second;

                match = match.NextMatch();
            }

            return result;
        }

    }
}
