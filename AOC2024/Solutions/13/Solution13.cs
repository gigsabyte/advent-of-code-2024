using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC2024.Solutions
{
    class Solution13 : Solution
    {
        protected override int Day => 13;

        protected override string Name => "Claw Contraption";

        protected override string Part1ExpectedResult => "480";

        protected override string Part2ExpectedResult => "875318608908";

        protected override string RunPart1(string input)
        {
            var machines = Parse(input);

            long total = 0;
            foreach (var machine in machines)
            {
                total += GetTokens(machine);
            }

            return total.ToString();
        }

        private long GetTokens(Machine machine)
        {
            var a1 = machine.A.Item1;
            var a2 = machine.A.Item2;
            var b1 = machine.B.Item1;
            var b2 = machine.B.Item2;
            var p1 = machine.Prize.Item1;
            var p2 = machine.Prize.Item2;

            // Solve for x and y
            // https://en.wikipedia.org/wiki/Cramer's_rule
            var x = (p1 * b2 - p2 * b1) / (a1 * b2 - b1 * a2);
            var y = (p2 * a1 - p1 * a2) / (a1 * b2 - b1 * a2);

            if (!machine.ReachesPrize((x, y)))
            {
                return 0;
            }

            // Button A takes 3 tokens per press
            return x * 3 + y;
        }


        protected override string RunPart2(string input)
        {
            var machines = Parse(input);

            var conversion = 10000000000000;
            long total = 0;
            foreach (var machine in machines)
            {
                machine.Prize = (machine.Prize.Item1 + conversion, machine.Prize.Item2 + conversion);
                total += GetTokens(machine);
            }

            return total.ToString();
        }

        private List<Machine> Parse(string input)
        {
            // Remove any carriage returns from the input for easier regex matching
            input = input.Replace("\r", "");

            var pattern = @"Button A: X\+([0-9]+), Y\+([0-9]+)\nButton B: X\+([0-9]+), Y\+([0-9]+)\nPrize: X=([0-9]+), Y=([0-9]+)";
            var machines = new List<Machine>();

            // Get matches
            var regex = new Regex(pattern);
            var match = regex.Match(input);

            // Process each regex match
            while (match.Success)
            {
                // Parse out the values from the operation
                var machine = new Machine();

                machine.A = (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                machine.B = (int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
                machine.Prize = (int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value));

                machines.Add(machine);

                match = match.NextMatch();
            }

            return machines;
        }

        public class Machine
        {
            public (int, int) A;
            public (int, int) B;
            public (long, long) Prize;

            public bool ReachesPrize((long, long) presses)
            {
                return presses.Item1 * A.Item1 + presses.Item2 * B.Item1 == Prize.Item1 &&
                       presses.Item1 * A.Item2 + presses.Item2 * B.Item2 == Prize.Item2;
            }
        }

    }
}
