using System;
using System.Collections.Generic;

namespace AOC2024.Solutions
{
    class Solution12 : Solution
    {
        protected override int Day => 12;

        protected override string Name => "Garden Groups";

        protected override string Part1ExpectedResult => "1930";

        protected override string Part2ExpectedResult => "1206";

        protected override string RunPart1(string input)
        {
            var plots = Parse(input, out var visited);
            
            long total = 0;
            for(var i = 0; i < plots.Length; ++i)
            {
                for(var j = 0; j < plots[i].Length; ++j)
                {
                    if(!visited[i][j])
                    {
                        total += GetCost(plots, (i, j), visited);
                    }
                }
            }

            return total.ToString();
        }
        private long GetCost(char[][] plots, (int, int) plot, bool[][] visited)
        {
            var plotRange = new HashSet<(int, int)>();
            Search(plots, plots[plot.Item1][plot.Item2], plot, visited, plotRange);

            long perimeter = 0;
            foreach (var space in plotRange)
            {
                var i = space.Item1;
                var j = space.Item2;

                if (!plotRange.Contains((i - 1, j)))
                {
                    ++perimeter;
                }
                if (!plotRange.Contains((i + 1, j)))
                {
                    ++perimeter;
                }
                if (!plotRange.Contains((i, j - 1)))
                {
                    ++perimeter;
                }
                if (!plotRange.Contains((i, j + 1)))
                {
                    ++perimeter;
                }
            }
            return perimeter * plotRange.Count;
        }


        protected override string RunPart2(string input)
        {
            var plots = Parse(input, out var visited);

            long total = 0;
            for (var i = 0; i < plots.Length; ++i)
            {
                for (var j = 0; j < plots[i].Length; ++j)
                {
                    if (!visited[i][j])
                    {
                        total += GetBulkCost(plots, (i, j), visited);
                    }
                }
            }

            return total.ToString();
        }

        private long GetBulkCost(char[][] plots, (int, int) plot, bool[][] visited)
        {
            var plotRange = new HashSet<(int, int)>();
            Search(plots, plots[plot.Item1][plot.Item2], plot, visited, plotRange);

            long sides = 0;

            // Count corners for spaces
            foreach (var space in plotRange)
            {
                var i = space.Item1;
                var j = space.Item2;

                var top = !plotRange.Contains((i - 1, j));
                var bottom = !plotRange.Contains((i + 1, j));
                var left = !plotRange.Contains((i, j - 1));
                var right = !plotRange.Contains((i, j + 1));

                // Top right corner OR "inner" corner (bottom right)
                if (top && (right || plotRange.Contains((i - 1, j + 1))))
                {
                    ++sides;
                }

                // Bottom right corner OR "inner" corner (bottom left)
                if (right && (bottom || plotRange.Contains((i + 1, j + 1))))
                {
                    ++sides;
                }

                // Bottom left corner OR "inner" corner (top left)
                if (bottom && (left || plotRange.Contains((i + 1, j - 1))))
                {
                    ++sides;
                }

                // Top left corner OR "inner" corner (top right)
                if (left && (top || plotRange.Contains((i - 1, j - 1))))
                {
                    ++sides;
                }

            }

            return sides * plotRange.Count;
        }

        private void Search(char[][] plots, char toSearch, (int, int) currPlot, bool[][] visited, HashSet<(int, int)> plotRange)
        {
            // Exit if:
            if (!IsValid(currPlot, plots) || // The space isn't valid
                visited[currPlot.Item1][currPlot.Item2] || // We've already visited this space
                plots[currPlot.Item1][currPlot.Item2] != toSearch) // This space doesn't have the right char
            {
                return;
            }

            visited[currPlot.Item1][currPlot.Item2] = true;
            plotRange.Add(currPlot);

            var locs = new (int, int)[] {
                (currPlot.Item1 + 1, currPlot.Item2),
                (currPlot.Item1, currPlot.Item2 + 1),
                (currPlot.Item1 - 1, currPlot.Item2),
                (currPlot.Item1, currPlot.Item2 - 1) };

            foreach (var loc in locs)
            {
                Search(plots, toSearch, loc, visited, plotRange);
            }
        }

        bool IsValid((int, int) index, char[][] plots)
        {
            return index.Item1 >= 0 && index.Item2 >= 0 && index.Item1 < plots.Length && index.Item2 < plots[index.Item1].Length;
        }

        private char[][] Parse(string input, out bool[][] visited)
        {
            var lines = input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var plots = new char[lines.Length][];
            visited = new bool[lines.Length][];

            for(var i = 0; i < lines.Length; ++i)
            {
                plots[i] = lines[i].ToCharArray();
                visited[i] = new bool[plots[i].Length];
            }

            return plots;
        }
    }
}
