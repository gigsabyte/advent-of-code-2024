using System.Collections.Generic;
using System.IO;

namespace AOC2024.Solutions
{
    class Solution6 : Solution
    {
        protected override int Day => 6;

        protected override string Name => "Guard Gallivant";

        protected override string Part1ExpectedResult => "41";

        protected override string Part2ExpectedResult => "6";

        protected override string RunPart1(string input)
        {
            ParsePositions(input, out var obstacles, out var visited, out var guard);

            var distinctPositions = Patrol(guard, obstacles, visited);

            return distinctPositions.ToString();
        }

        protected override string RunPart2(string input)
        {
            ParsePositions(input, out var obstacles, out var visited, out var guard);

            var originalGuard = new Guard();
            originalGuard.CopyFrom(guard);

            long total = Patrol(guard, obstacles, visited);

            var cycles = 0;
            var newVisited = ToIntList(visited);

            for(var i = 0; i < visited.Count; ++i)
            {
                for(var j = 0; j < visited.Count; ++j)
                {
                    // Skip positions not in the original path and the guard's original position
                    if(!visited[i][j] || (i == originalGuard.x && j == originalGuard.y))
                    {
                        continue;
                    }

                    // Reset
                    ResetList(newVisited);
                    guard.CopyFrom(originalGuard);

                    // See if making this position an obstacle causes a cycle
                    obstacles[i][j] = true;
                    if(IsPatrolCycle(guard, obstacles, newVisited, total))
                    {
                        ++cycles;
                    }
                    obstacles[i][j] = false;
                }
            }

            return cycles.ToString();
        }

        private long Patrol(Guard guard, List<List<bool>> obstacles, List<List<bool>> visited)
        {
            long total = 0;
            while (true)
            {
                guard.GetNewPosition(out var x, out var y);
                if (x < 0 || y < 0 || x >= obstacles.Count || y >= obstacles[x].Count)
                {
                    break;
                }
                if (obstacles[x][y])
                {
                    guard.Rotate();
                    continue;
                }
                guard.x = x;
                guard.y = y;

                if (!visited[x][y])
                {
                    ++total;
                    visited[x][y] = true;
                }
            }
            return total;
        }

        private bool IsPatrolCycle(Guard guard, List<List<bool>> obstacles, List<List<int>> visited, long total)
        {
            var isCycle = true;
            var subtotal = 1;
            while (subtotal < total)
            {
                guard.GetNewPosition(out var x, out var y);
                if (x < 0 || y < 0 || x >= obstacles.Count || y >= obstacles[x].Count)
                {
                    isCycle = false;
                    break;
                }
                if (obstacles[x][y])
                {
                    guard.Rotate();
                    continue;
                }
                guard.x = x;
                guard.y = y;

                ++visited[x][y];
                if (visited[x][y] > 2)
                {
                    ++subtotal;
                }
            }
            return isCycle;
        }

        private List<List<int>> ToIntList(List<List<bool>> boolList)
        {
            var intList = new List<List<int>>();
            for (int i = 0; i < boolList.Count; ++i)
            {
                var row = new List<int>();
                for (int j = 0; j < boolList[i].Count; ++j)
                {
                    row.Add(0);
                }
                intList.Add(row);
            }
            return intList;
        }

        private void ResetList(List<List<int>> intList)
        {
            for (int i = 0; i < intList.Count; ++i)
            {
                for (int j = 0; j < intList[i].Count; ++j)
                {
                    intList[i][j] = 0;
                }
            }
        }

        private void ParsePositions(string input, out List<List<bool>> obstacles, out List<List<bool>> visited, out Guard guard)
        {
            // Init
            obstacles = new List<List<bool>>();
            visited = new List<List<bool>>();
            guard = new Guard();

            // Parse the rules and updates from the input string
            using (var reader = new StringReader(input))
            {
                string line;
                var row = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    var col = 0;
                    obstacles.Add(new List<bool>());
                    visited.Add(new List<bool>());
                    foreach (char c in line)
                    {
                        if(c == '#')
                        {
                            obstacles[row].Add(true);
                            visited[row].Add(false);
                            ++col;
                            continue;
                        }
                        obstacles[row].Add(false);
                        if(c == '^' || c == '>' || c == 'v' || c == '<')
                        {
                            guard.x = row;
                            guard.y = col;
                            switch(c)
                            {
                                case '^':
                                    guard.facing = Guard.Direction.Up;
                                    break;
                                case '>':
                                    guard.facing = Guard.Direction.Right;
                                    break;
                                case 'v':
                                    guard.facing = Guard.Direction.Down;
                                    break;
                                case '<':
                                    guard.facing = Guard.Direction.Left;
                                    break;
                                default:
                                    break;
                            }
                            visited[row].Add(true);
                        }
                        else
                        {
                            visited[row].Add(false);
                        }
                        ++col;
                    }
                    ++row;
                }
            }
        }
    }

    class Guard
    {
        public int x;
        public int y;

        public Direction facing;

        public void Rotate()
        {
            if(facing == Direction.Left)
            {
                facing = Direction.Up;
            }
            else
            {
                ++facing;
            }
        }

        public void GetNewPosition(out int newX, out int newY)
        {
            newX = x;
            newY = y;
            switch(facing)
            {
                case Direction.Up:
                    newX = x - 1;
                    break;
                case Direction.Down:
                    newX = x + 1;
                    break;
                case Direction.Left:
                    newY = y - 1;
                    break;
                case Direction.Right:
                    newY = y + 1;
                    break;
                default:
                    break;
            }
        }

        public void CopyFrom(Guard other)
        {
            x = other.x;
            y = other.y;
            facing = other.facing;
        }

        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
    }
}
