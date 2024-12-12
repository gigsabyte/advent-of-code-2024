using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2024.Solutions
{
    class Solution5 : Solution
    {
        protected override int Day => 5;

        protected override string Name => "Print Queue";

        protected override string Part1ExpectedResult => "143";

        protected override string Part2ExpectedResult => "123";

        protected override string RunPart1(string input)
        {
            ParseRulesAndUpdates(input, out var rules, out var updates);
            int result = 0;

            foreach(var update in updates)
            {
                if(IsValid(update, rules))
                {
                    result += update[update.Count / 2];
                }
            }

            return result.ToString();
        }

        protected override string RunPart2(string input)
        {
            ParseRulesAndUpdates(input, out var rules, out var updates);
            int result = 0;

            foreach (var update in updates)
            {
                if (!IsValid(update, rules))
                {
                    Sort(update, rules);
                    result += update[update.Count / 2];
                }
            }
            return result.ToString();
        }

        private bool IsValid(List<int> update, Dictionary<int, HashSet<int>> rules)
        {
            var numSet = new HashSet<int>();
            foreach (var num in update)
            {
                if (rules.ContainsKey(num) && numSet.Overlaps(rules[num]))
                {
                    return false;
                }
                numSet.Add(num);
            }
            return true;
        }

        private void Sort(List<int> update, Dictionary<int, HashSet<int>> rules)
        {
            update.Sort(delegate(int a, int b) { 
               if(rules.ContainsKey(a) && rules[a].Contains(b))
                {
                    return -1;
                }
               else if(rules.ContainsKey(b) && rules[b].Contains(a))
                {
                    return 1;
                }
                return 0;
            }
            );
        }


        private void ParseRulesAndUpdates(string input, out Dictionary<int, HashSet<int>> rules, out List<List<int>> updates)
        {
            // Init
            rules = new Dictionary<int, HashSet<int>>();
            updates = new List<List<int>>();

            // Parse the rules and updates from the input string
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if(line.Contains("|"))
                    {
                        var nums = line.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        AddRule(rules, int.Parse(nums[0]), int.Parse(nums[1]));

                    }
                    else if(line.Contains(","))
                    {
                        var nums = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var numList = new List<int>();
                        foreach(var numstr in nums)
                        {
                            numList.Add(int.Parse(numstr));
                        }
                        updates.Add(numList);
                    }
                    
                }
            }
        }

        private void AddRule(Dictionary<int, HashSet<int>> rules, int key, int val)
        {
            if(rules.ContainsKey(key))
            {
                rules[key].Add(val);
            }
            else
            {
                rules[key] = new HashSet<int>() { val };
            }
        }
    }
}
