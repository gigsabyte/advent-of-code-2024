using System;
using System.Collections.Generic;

namespace AOC2024.Solutions
{
    class Solution11 : Solution
    {
        protected override int Day => 11;

        protected override string Name => "Plutonian Pebbles";

        protected override string Part1ExpectedResult => "55312";

        protected override string Part2ExpectedResult => "65601038650482";

        protected override string RunPart1(string input)
        {
            var stones = Parse(input);
            long total = 0;
            var cache = new Dictionary<(long, int), long>();
            foreach (var stone in stones)
            {
                total += CountBlinks(stone, 25, cache);
            }

            return total.ToString();
        }

        protected override string RunPart2(string input)
        {
            var stones = Parse(input);
            long total = 0;
            var cache = new Dictionary<(long, int), long>();
            foreach (var stone in stones)
            {
                total += CountBlinks(stone, 75, cache);
            }

            return total.ToString();
        }


        private long CountBlinks(long stone, int blinks, Dictionary<(long, int), long> cache)
        {
            // Base case - we're done blinking, this is one stone
            if (blinks == 0)
            {
                return 1;
            }

            // If the result for this combination is cached, return that value
            if (cache.ContainsKey((stone, blinks)))
            {
                return cache[(stone, blinks)];
            }

            long result;

            // If the stone is 0, it becomes 1
            if (stone == 0)
            {
                result = CountBlinks(1, blinks - 1, cache);
                cache[(stone, blinks)] = result;
                return result;
            }

            // If the stone has an odd number of digits, multiply by 2024
            var stoneStr = stone.ToString();
            if (stoneStr.Length % 2 != 0)
            {
                result = CountBlinks(stone * 2024, blinks - 1, cache);
                cache[(stone, blinks)] = result;
                return result;
            }

            // If the stone has an even number of digits, split into two and keep counting
            var mid = stoneStr.Length / 2;
            var leftStone = long.Parse(stoneStr.Substring(0, mid));
            var rightStone = long.Parse(stoneStr.Substring(mid, mid));

            result = CountBlinks(leftStone, blinks - 1, cache) + CountBlinks(rightStone, blinks - 1, cache);
            cache[(stone, blinks)] = result;
            return result;
        }


        private List<long> Parse(string input)
        {
            var lines = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var stones = new List<long>();

            for(var i = 0; i < lines.Length; ++i)
            {
                stones.Add(long.Parse(lines[i]));
            }

            return stones;
        }
    }
}
