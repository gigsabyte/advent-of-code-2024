namespace AOC2024.Solutions
{
    class Solution4 : Solution
    {
        protected override int Day => 4;

        protected override string Name => "Ceres Search";

        protected override string RunPart1(string input)
        {
            var letters = SplitLines(input);

            var xmasCount = 0;
            for(int i = 0; i < letters.Length; ++i)
            {
                for(int j = 0; j < letters[i].Length; ++j)
                {
                    // Start by finding X
                    if(letters[i][j] != 'X')
                    {
                        continue;
                    }

                    // Forward + Backward
                    xmasCount += CheckMas(letters, i, j, 0, 1);
                    xmasCount += CheckMas(letters, i, j, 0, -1);

                    // Up and Down
                    xmasCount += CheckMas(letters, i, j, 1, 0);
                    xmasCount += CheckMas(letters, i, j, -1, 0);

                    // Diagonals
                    xmasCount += CheckMas(letters, i, j, -1, -1);
                    xmasCount += CheckMas(letters, i, j, -1, 1);
                    xmasCount += CheckMas(letters, i, j, 1, -1);
                    xmasCount += CheckMas(letters, i, j, 1, 1);
                    
                }
            }

            return xmasCount.ToString();
        }

        protected override string RunPart2(string input)
        {
            var letters = SplitLines(input);

            var xmasCount = 0;
            // Skip the first/last row and column
            // Since we're searching for the center of an X
            for (int i = 1; i < letters.Length - 1; ++i)
            {
                for (int j = 1; j < letters[i].Length - 1; ++j)
                {
                    // Start by finding A
                    if (letters[i][j] != 'A')
                    {
                        continue;
                    }
                    xmasCount += CheckX(letters, i, j);
                }
            }

            return xmasCount.ToString();
        }

        /// <summary>
        /// Check for M A S in the given direction
        /// </summary>
        /// <param name="vert">Amount to increase/decrease vertically per letter</param>
        /// <param name="hor">Amount to increase/decrease horizontally per letter</param>
        /// <returns></returns>
        private int CheckMas(string[] letters, int startRow, int startCol, int vert, int hor)
        {
            var next = new char[] { 'M', 'A', 'S' };
            for(int i = 1; i <= 3; ++i)
            {
                var row = startRow + (i * vert);
                var col = startCol + (i * hor);
                if (!HasChar(letters, row, col, next[i - 1]))
                {
                    return 0;
                }
            }
            return 1;
        }

        /// <summary>
        /// Check for M A S in an X shape around the start position (assuming it's A)
        /// </summary>
        private int CheckX(string[] letters, int startRow, int startCol)
        {
            int left = startCol - 1;
            int right = startCol + 1;
            int up = startRow - 1;
            int down = startRow + 1;

            // M.M
            // .A.
            // S.S
            if (HasChar(letters, up, left, 'M') && HasChar(letters, up, right, 'M'))
            {
                if(HasChar(letters, down, left, 'S') && HasChar(letters, down, right, 'S'))
                {
                    return 1;
                }
            }
            // S.S
            // .A.
            // M.M
            else if (HasChar(letters, down, left, 'M') && HasChar(letters, down, right, 'M'))
            {
                if (HasChar(letters, up, left, 'S') && HasChar(letters, up, right, 'S'))
                {
                    return 1;
                }
            }
            // M.S
            // .A.
            // M.S
            if (HasChar(letters, up, left, 'M') && HasChar(letters, down, left, 'M'))
            {
                if (HasChar(letters, up, right, 'S') && HasChar(letters, down, right, 'S'))
                {
                    return 1;
                }
            }
            // S.M
            // .A.
            // S.M
            else if (HasChar(letters, up, right, 'M') && HasChar(letters, down, right, 'M'))
            {
                if (HasChar(letters, up, left, 'S') && HasChar(letters, down, left, 'S'))
                {
                    return 1;
                }
            }
            return 0;
        }

        private string[] SplitLines(string input)
        {
            return input.Split('\n');
        }

        private bool HasChar(string[] list, int row, int col, char c)
        {
            if (row < 0 || col < 0 || list.Length <= row || list[row].Length <= col)
            {
                return false;
            }
            return list[row][col] == c;
        }

    }

    
}
