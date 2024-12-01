using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2024.Solutions
{
    class Solution1 : Solution
    {
        protected override int Day => 1;

        protected override string Name => "Historian Hysteria";

        protected override string RunPart1(string input)
        {
            // Parse
            ParseIntLists(input, out var list1, out var list2);


            // Find distances and add to result
            var result = 0;
            for (var i = 0; i < list1.Count; ++i)
            {
                result += Math.Abs(list1[i] - list2[i]);
            }

            return result.ToString();
        }

        protected override string RunPart2(string input)
        {
            // Parse
            ParseIntLists(input, out var list1, out var list2);

            // Find counts
            var result = 0;
            foreach (int num in list1)
            {
                // Cheat with LINQ
                result += num * list2.Count(n => n == num);
            }

            return result.ToString();
        }

        private void ParseIntLists(string input, out List<int> list1, out List<int> list2)
        {
            // Init
            list1 = new List<int>();
            list2 = new List<int>();

            // Parse the lists from the input string
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var nums = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    list1.Add(int.Parse(nums[0]));
                    list2.Add(int.Parse(nums[1]));
                }
            }


            // Sort
            list1.Sort();
            list2.Sort();
        }
    }
}
