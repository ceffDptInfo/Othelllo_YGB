namespace Reversi_WPF
{
    public class Reversi
    {
        public int[,] grid;

        public int activePlayer = 1;
        public int HEIGHT = 8;
        public int WIDTH = 8;

        public Reversi()
        {
            grid = new int[WIDTH, HEIGHT];
            Initialize();
        }

        public void Initialize()
        {
            // Create 4 first dot

            grid[WIDTH / 2 - 1, HEIGHT / 2 - 1] = -1;
            grid[WIDTH / 2 - 1, HEIGHT / 2] = 1;
            grid[WIDTH / 2, HEIGHT / 2 - 1] = 1;
            grid[WIDTH / 2, HEIGHT / 2] = -1;
        }

        public void Update()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    grid[i, j] = -1;
                }
            }
        }

        public bool PutPawn(int x, int y)
        {
            if (CanPutPawn(x, y))
            {
                grid[x, y] = activePlayer;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue;
                        if (TestLine(x, y, i, j)) ReturnLine(x, y, i, j);

                    }
                }
                return true;
            }
            return false;
        }

        public bool CanPutPawn(int x, int y)
        {
            if (grid[x, y] != 0)
            {
                return false;
            }

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    if (TestLine(x, y, dx, dy))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TestLine(int x, int y, int dx, int dy)
        {
            int nx = x + dx; // Next Direction x
            int ny = y + dy; // Next Direction y
            bool isTheFirstPawn = true;

            while (GetCell(nx, ny) == activePlayer * -1)
            {
                nx += dx;
                ny += dy;
                isTheFirstPawn = false;
            }

            if (GetCell(nx, ny) == activePlayer && !isTheFirstPawn)
                return true;
            return false;
        }

        private int GetCell(int x, int y)
        {
            if (x < 0 || x >= 8 || y < 0 || y >= 8)
            {
                return 0;
            }
            return grid[x, y];
        }

        private void ReturnLine(int x, int y, int dx, int dy)
        {
            int nx = x + dx; // Next Direction x
            int ny = y + dy; // Next Direction y

            while (GetCell(nx, ny) == activePlayer * -1)
            {
                grid[nx, ny] = activePlayer;
                nx += dx;
                ny += dy;
            }
        }
        public bool IsFinished()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void ChangePlayer()
        {
            activePlayer *= -1;
        }

        public int GetWinner()
        {
            int white = 0;
            int black = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        white++;
                    }
                    else if (grid[i, j] == -1)
                    {
                        black++;
                    }
                }
            }

            if (white > black)
            {
                return 1;
            }
            else if (black > white)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public int GetScore(int player)
        {
            int score = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grid[i, j] == player)
                    {
                        score++;
                    }
                }
            }

            return score;
        }
    }
}
