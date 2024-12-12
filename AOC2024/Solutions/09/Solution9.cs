using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOC2024.Solutions
{
    class Solution9 : Solution
    {
        protected override int Day => 9;

        protected override string Name => "Disk Fragmenter";

        protected override string Part1ExpectedResult => "1928";

        protected override string Part2ExpectedResult => "2858";

        protected override string RunPart1(string input)
        {
            var nums = Parse(input, out var files);
            
            // Find the first available space
            var i = 0;
            while (nums[i] > -1) ++i;

            // Start moving elements from the end of the array to available files
            // j will track the end of the moved values
            var j = nums.Count;
            while(i < j)
            {
                --j;
                nums[i] = nums[j];
                while (i < j && nums[i] > -1) ++i;
            }

            // Checksum
            long result = 0;
            for(i = 0; i < j; ++i)
            {
                result += i * nums[i];
            }

            return result.ToString();
        }

        protected override string RunPart2(string input)
        {
            var nums = Parse(input, out var files);

            // For part 2, use a stack of files to move them all at once
            while (files.Count > 0)
            {
                var file = files.Pop();
                
                var space = FindSpace(nums, file.Length, 0, file.Location);
                
                // If a space is found, copy the values over
                if(space > -1)
                {
                    for (var j = 0; j < file.Length; ++j)
                    {
                        nums[space + j] = file.ID;
                        nums[file.Location + j] = -1;
                    }
                    file.Location = space;
                }
            }

            // Checksum
            long result = 0;
            for (var i = 0; i < nums.Count; ++i)
            {
                if(nums[i] > -1)
                {
                    result += i * nums[i];
                }
                
            }

            return result.ToString();
        }

        /// <summary>
        /// Recursive search to find an available space
        /// </summary>
        private int FindSpace(List<int> nums, int length, int startInd, int maxInd)
        {
            // Find the starting index of a space
            var spaceInd = startInd;
            while (spaceInd < maxInd && nums[spaceInd] != -1)
            {
                ++spaceInd;
            }

            // Find the length of the space
            var thisLength = 0;
            while(spaceInd + thisLength < maxInd && nums[spaceInd + thisLength] == -1)
            {
                ++thisLength;
            }
            
            // If we've reached the end of our available search space, return -1
            if (spaceInd + thisLength >= maxInd)
            {
                return -1;
            }

            // If this space can fit the length we need, return the starting index
            if (thisLength >= length)
            {
                return spaceInd;
            }

            // Otherwise, keep searching
            return FindSpace(nums, length, spaceInd + thisLength + 1, maxInd);
        }

        private List<int> Parse(string input, out Stack<IntFile> files)
        {
            var ints = new List<int>();
            files = new Stack<IntFile>();
            var fileIndex = 0;
            for(var i = 0; i < input.Length; ++i)
            {
                var parsed = input[i] - '0';
                if (i % 2 == 0)
                {
                    files.Push(new IntFile() { Location = ints.Count, ID = fileIndex, Length = parsed });
                    for (var j = 0; j < parsed; ++j)
                    {
                        ints.Add(fileIndex);
                    }
                    ++fileIndex;
                }
                else
                {
                    if(i == input.Length - 1)
                    {
                        break;
                    }

                    for (var j = 0; j < parsed; ++j)
                    {
                        ints.Add(-1);
                    }
                }
            }
            return ints;
        }

        public class IntFile
        {
            public int Location;
            public int ID;
            public int Length;
        }
    }
}
