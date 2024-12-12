using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2024.Solutions
{
    class Solution2 : Solution
    {
        protected override int Day => 2;

        protected override string Name => "Red-Nosed Reports";

        protected override string Part1ExpectedResult => "2";

        protected override string Part2ExpectedResult => "4";

        protected override string RunPart1(string input)
        {
            var reports = ParseReports(input);

            var totalSafe = 0;

            foreach(var report in reports)
            {
                if(IsSafe(report))
                {
                    ++totalSafe;
                }
            }

            return totalSafe.ToString();
        }

        protected override string RunPart2(string input)
        {
            var reports = ParseReports(input);

            var totalSafe = 0;

            foreach (var report in reports)
            {
                
                if (IsSafe(report))
                {
                    ++totalSafe;
                    continue;
                }
                var isSafe = false;
                for(var i = 0; i < report.Count; ++i)
                {
                    var copy = new List<int>();
                    foreach(var num in report)
                    {
                        copy.Add(num);
                    }
                    copy.RemoveAt(i);
                    isSafe = IsSafe(copy);
                    if(isSafe)
                    {
                        break;
                    }
                }
                if(isSafe)
                {
                    ++totalSafe;
                }
            }

            return totalSafe.ToString();
        }

        private bool IsSafe(List<int> report)
        {
            if (Math.Abs(report[1] - report[0]) < 1 || Math.Abs(report[1] - report[0]) > 3)
            {
                return false;
            }
            var isSafe = true;
            var isIncreasing = (report[1] - report[0] >= 0);
            for (var i = 2; i < report.Count; ++i)
            {
                if (Math.Abs(report[i] - report[i - 1]) < 1 || Math.Abs(report[i] - report[i - 1]) > 3)
                {
                    isSafe = false;
                    break;
                }
                if ((isIncreasing && (report[i] - report[i - 1] < 0)) || (!isIncreasing && (report[i] - report[i - 1] >= 0)))
                {
                    isSafe = false;
                    break;
                }
            }
            return isSafe;
        }

        private List<List<int>> ParseReports(string input)
        {
            // Init
            var reports = new List<List<int>>();

            // Parse the reports from the input string
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var numStrs = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var nums = new List<int>();
                    foreach(var numStr in numStrs)
                    {
                        nums.Add(int.Parse(numStr));
                    }
                    reports.Add(nums);
                }
            }
            return reports;
        }

    }
}
