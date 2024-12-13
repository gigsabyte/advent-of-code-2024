using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOC2024.Solutions
{
    class Solution10 : Solution
    {
        protected override int Day => 10;

        protected override string Name => "Hoof It";

        protected override string Part1ExpectedResult => "36";

        protected override string Part2ExpectedResult => "81";

        protected override string RunPart1(string input)
        {
            var trail = Parse(input);

            long result = 0;
            for (var i = 0; i < trail.Length; ++i)
            {
                for(var j = 0; j < trail[i].Length; ++j)
                {
                    if(trail[i][j] != '0')
                    {
                        continue;
                    }

                    var score = 0;
                    var rating = 0;
                    GetScore(trail, (i, j), (i, j), '0', ref score, ref rating, new HashSet<(int, int)>());

                    result += score;
                }
            }

            return result.ToString();
        }

        protected override string RunPart2(string input)
        {
            var trail = Parse(input);

            long result = 0;
            for (var i = 0; i < trail.Length; ++i)
            {
                for (var j = 0; j < trail[i].Length; ++j)
                {
                    if (trail[i][j] != '0')
                    {
                        continue;
                    }

                    var score = 0;
                    var rating = 0;
                    GetScore(trail, (i, j), (i, j), '0', ref score, ref rating, new HashSet<(int, int)>());

                    result += rating;
                }
            }

            return result.ToString();
        }

        private void GetScore(char[][] trail, (int, int) lastPos, (int, int) currPos, char currInd, ref int visitedTotal, ref int ratingTotal, HashSet<(int, int)> visited)
        {
            var row = currPos.Item1;
            var col = currPos.Item2;

            // Exit if out of bounds
            if (row < 0 || col < 0 || row >= trail.Length || col >= trail[row].Length)
            {
                return;
            }
            // Exit if this isn't a valid part of the trail
            if (trail[row][col] != currInd)
            {
                return;
            }

            // Exit if we've reached the end of the trail
            if(trail[row][col] == '9')
            {
                // Only increment our visited total if we haven't visited here before
                if (visited.Add((row, col)))
                {
                    ++visitedTotal;
                }
                // Always increment the rating total
                ++ratingTotal;
                return;
            }

            // Get the next index to search for
            var newInd = (char)(currInd + (char)1);
            if(currInd == '0')
            {
                // If this is the beginning, search in every direction
                GetScore(trail, currPos, (row - 1, col), newInd, ref visitedTotal, ref ratingTotal, visited);
                GetScore(trail, currPos, (row + 1, col), newInd, ref visitedTotal, ref ratingTotal, visited);
                GetScore(trail, currPos, (row, col - 1), newInd, ref visitedTotal, ref ratingTotal, visited);
                GetScore(trail, currPos, (row, col + 1), newInd, ref visitedTotal, ref ratingTotal, visited);
            }
            else
            {
                // Only search places we haven't already visited
                var lastRow = lastPos.Item1;
                var lastCol = lastPos.Item2;

                if (lastRow != row - 1) GetScore(trail, currPos, (row - 1, col), newInd, ref visitedTotal, ref ratingTotal, visited);
                if (lastRow != row + 1) GetScore(trail, currPos, (row + 1, col), newInd, ref visitedTotal, ref ratingTotal, visited);
                if (lastCol != col - 1) GetScore(trail, currPos, (row, col - 1), newInd, ref visitedTotal, ref ratingTotal, visited);
                if (lastCol != col + 1) GetScore(trail, currPos, (row, col + 1), newInd, ref visitedTotal, ref ratingTotal, visited);
            }
            
        }

        private char[][] Parse(string input)
        {
            var lines = input.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var chars = new char[lines.Length][];

            for(var i = 0; i < lines.Length; ++i)
            {
                chars[i] = lines[i].ToCharArray();
            }
            return chars;
        }
    }
}
