using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2024.Solutions
{
    class Solution8 : Solution
    {
        protected override int Day => 8;

        protected override string Name => "Resonant Collinearity";

        protected override string Part1ExpectedResult => "14";

        protected override string Part2ExpectedResult => "34";

        protected override string RunPart1(string input)
        {
            var size = Parse(input, out var antennas, out var locations);
            var antinodes = new HashSet<(int, int)>();
            
            foreach(var antenna in antennas)
            {
                var locs = locations[antenna];
                if (locs.Count > 1)
                {
                    AddAntiNodes(size, locs, antinodes, false);
                }
            }
            
            return antinodes.Count.ToString();
        }

        protected override string RunPart2(string input)
        {
            var size = Parse(input, out var antennas, out var locations);
            var antinodes = new HashSet<(int, int)>();
            foreach (var antenna in antennas)
            {
                var locs = locations[antenna];
                if(locs.Count == 1)
                {
                    continue;
                }

                AddAntiNodes(size, locs, antinodes, true);
                foreach (var loc in locs)
                {
                    antinodes.Add(loc);
                }
            }

            return antinodes.Count.ToString();
        }

        private void AddAntiNodes((int, int) size, List<(int, int)> locations, HashSet<(int, int)> existing, bool useTRule)
        {
            for (var i = 0; i < locations.Count; ++i)
            {
                for (var j = 0; j < locations.Count; ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (!TryGetAntiNodes(locations[i], locations[j], size, useTRule, out var nodes))
                    {
                        continue;
                    }
                    foreach (var node in nodes)
                    {
                        existing.Add(node);
                    }
                }
            }
        }

        private bool TryGetAntiNodes((int, int) node1, (int, int) node2, (int, int) size, bool useTRule, out List<(int, int)> nodes)
        {
            nodes = new List<(int, int)>();

            // Calculate the direction and magnitude we should be moving in to get a point 2x the distance from node2
            var diff = ((node2.Item1 - node1.Item1) * -1, (node2.Item2 - node1.Item2) * -1);

            var antiNode = (node1.Item1 + diff.Item1, node1.Item2 + diff.Item2);

            // Part 1 - Just calc once
            if(!useTRule)
            {
                if(IsInBounds(antiNode, size))
                {
                    nodes.Add(antiNode);
                    return true;
                }
                return false;
            }
            
            // Part 2: Keep going until we're out of bounds
            while (IsInBounds(antiNode, size))
            {
                nodes.Add(antiNode);
                antiNode = (antiNode.Item1 + diff.Item1, antiNode.Item2 + diff.Item2);
            }

            return nodes.Count > 0;
        }

        private bool IsInBounds((int, int) node, (int, int) size)
        {
            return node.Item1 >= 0 && node.Item2 >= 0 && node.Item1 < size.Item1 && node.Item2 < size.Item2;
        }

        private (int, int) Parse(string input, out HashSet<char> antennas, out Dictionary<char, List<(int, int)>> locations)
        {
            antennas = new HashSet<char>();
            locations = new Dictionary<char, List<(int, int)>>();

            var i = 0;
            var lineLength = 0;
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineLength = line.Length;
                    for (var j = 0; j < line.Length; ++j)
                    {
                        if (line[j] == '.')
                        {
                            continue;
                        }
                        if (antennas.Contains(line[j]))
                        {
                            locations[line[j]].Add((i, j));
                            continue;
                        }
                        antennas.Add(line[j]);
                        locations[line[j]] = new List<(int, int)>() { (i, j) };
                    }
                    ++i;
                }
            }
            return (i, lineLength);
        }
    }
}
