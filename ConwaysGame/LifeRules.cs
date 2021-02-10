using System;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGame
{
    public class LifeRules
    {

        public enum CellState
        {
            Dead,
            Alive,
        }
        private CellState[,] currentState;
        private CellState[,] nextState;
        private int height;
        private int width;

        

        public void Grid(int heightOfGrid, int widthOfGrid)
        {
            height = heightOfGrid;
            width = widthOfGrid;
            currentState = new CellState[height, width];
            nextState = new CellState[height, width];
            currentState[2, 6] = CellState.Alive;
            currentState[2, 7] = CellState.Alive;
            currentState[2, 8] = CellState.Alive;
            //RandomizeGridCells();
            var totalGrid = new StringBuilder();
            do
            {
                for (int i = 0; i < height; i++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        totalGrid.Append(currentState[i, x].ToString() == "Alive" ? "O" : ".");
                        if (x == (width - 1))
                            totalGrid.AppendLine();
                    }
                }
                UpdateGridState(height, width);
                currentState = nextState;
                nextState = new CellState[height, width];
                Console.Write(totalGrid);
                totalGrid.Clear();
            } while (Console.ReadKey(false).Key == ConsoleKey.Enter);

        }
        public void UpdateGridState(int height, int width)
        {
            Parallel.For(0, height, (i, state) =>
            {
                Parallel.For(0, width, (x, innerState) =>
                {
                    nextState[i, x] = GetNewState(currentState[i, x], GetNumberOfLivingNeighbors(i, x));
                });
            });
        }
        public void RandomizeGridCells()
        {
            Random rand = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (rand.Next(2) == 1)
                        currentState[i, x] = CellState.Alive;
                }
            }
        }
        public static CellState GetNewState(CellState currentState, int liveNeighbors)
        {
            if (currentState == CellState.Alive && (liveNeighbors < 2 || liveNeighbors > 3))
                return CellState.Dead;
            if (currentState == CellState.Dead && liveNeighbors == 3)
                return CellState.Alive;
            return currentState;
        }
        public int GetNumberOfLivingNeighbors(int currenti, int currentx)
        {
            int celli;
            int cellx;
            int liveNeighbors = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (i == 0 && x == 0)
                        continue;
                    celli = currenti + i;
                    cellx = currentx + x;
                    if (celli >= 0 && celli < height && cellx >= 0 && cellx < width)
                        if (currentState[celli, cellx].ToString() == "Alive")
                            liveNeighbors++;
                }
            }
            return liveNeighbors;
        }
        static void Main(string[] args)
        {
            LifeRules game = new LifeRules();
            game.Grid(5, 10);
        }
    }
}
