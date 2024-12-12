using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2024.Solutions
{
    class Solution7 : Solution
    {
        protected override int Day => 7;

        protected override string Name => "Bridge Repair";

        protected override string Part1ExpectedResult => "3749";

        protected override string Part2ExpectedResult => "11387";

        protected override string RunPart1(string input)
        {
            Parse(input, out var totals, out var nums);
            long result = 0;

            for(var i = 0; i < totals.Count; ++i)
            {             
                if (CanEquate(totals[i], nums[i], 1, nums[i][0]))
                {
                    result += totals[i];
                }
            }
            
            return result.ToString();
        }

        protected override string RunPart2(string input)
        {
            Parse(input, out var totals, out var nums);
            long result = 0;

            for (var i = 0; i < totals.Count; ++i)
            {
                if (CanEquate(totals[i], nums[i], 1, nums[i][0], true))
                {
                    result += totals[i];
                }
            }

            return result.ToString();
        }

        private bool CanEquate(long total, List<long> input, int index, long currentTotal, bool useConcat = false)
        {
            if (index == input.Count)
            {
                return currentTotal == total;
            }

            if (currentTotal > total)
            {
                return false;
            }

            if (!useConcat)
            {
                return CanEquate(total, input, index + 1, currentTotal * input[index], useConcat) ||
                       CanEquate(total, input, index + 1, currentTotal + input[index], useConcat); ;
            }
            else
            {
                return CanEquate(total, input, index + 1, currentTotal * input[index], useConcat) ||
                       CanEquate(total, input, index + 1, currentTotal + input[index], useConcat) ||
                       CanEquate(total, input, index + 1, Concat(currentTotal, input[index]), useConcat);
            }
        }

        private long Concat(long a, long b)
        {
            var digits = b;
            while (digits > 0)
            {
                a *= 10;
                digits /= 10;
            }
            return a + b;
        }

        private void Parse(string input, out List<long> totals, out List<List<long>> nums)
        {
            using (var reader = new StringReader(input))
            {
                totals = new List<long>();
                nums = new List<List<long>>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var elems = line.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    totals.Add(long.Parse(elems[0]));

                    var subNums = new List<long>();
                    var numStrs = elems[1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach(var numStr in numStrs)
                    {
                        subNums.Add(long.Parse(numStr));
                    }
                    nums.Add(subNums);
                }
            }
        }
    }
}
