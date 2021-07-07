using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Life
    {
        public Grid InputGrid { get; }

        public Grid OutputGrid { get; }

        private Task EvaluateCellTask;

        private Task EvaluateGridGrowthTask;

        public int MaxGenerations = 1;

        public int RowCount => InputGrid.RowCount;

        public int ColumnCount => InputGrid.ColumnCount;

        public Life(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0) throw new ArgumentOutOfRangeException("Row and Column size must be greater than zero");
            InputGrid = new Grid(rows, columns);
            OutputGrid = new Grid(rows, columns);
            GarbageCollection.InitializeReachableCells();
        }

        public void ToggleGridCell(int x, int y)
        {
            if (InputGrid.RowCount <= x || InputGrid.ColumnCount <= y) throw new ArgumentOutOfRangeException("Argument out of bound");
            InputGrid.ToggleCell(x, y);

        }

        internal void Start()
        {
            int currentGeneration = 0;
            InputGrid.Display();
            do
            {
                currentGeneration++;
                
                ProcessGeneration();

                Console.WriteLine("Generation: " + currentGeneration);

                InputGrid.Display();
                
            } while (currentGeneration < MaxGenerations);
        }

        private void ProcessGeneration()
        {
            SetNextGeneration();
            Tick();
            FlipGridState();
        }

        private void SetNextGeneration()
        {
            
            if ((EvaluateCellTask == null) || (EvaluateCellTask != null && EvaluateCellTask.IsCompleted))
            {
                EvaluateCellTask = ChangeCellsState();
                
                EvaluateCellTask.Wait();
            }
            if ((EvaluateGridGrowthTask == null) || (EvaluateGridGrowthTask != null && EvaluateGridGrowthTask.IsCompleted))
            {
                EvaluateGridGrowthTask = ChangeGridState();
            }
        }

        private void Tick()
        {
            if (EvaluateGridGrowthTask != null)
            {
                EvaluateGridGrowthTask.Wait();
            }
        }

        private void FlipGridState()
        {
            OutputGrid.Copy(InputGrid);
            OutputGrid.ReInitialize();
        }

        private Task ChangeCellsState()
        {
            return Task.Factory.StartNew(() =>
            Parallel.For(0, InputGrid.RowCount, x =>
            {
                Parallel.For(0, InputGrid.ColumnCount, y =>
                {
                    GameRule.ChangeCellsState(InputGrid, OutputGrid, new CoOrdinates(x, y));
                });
            }));
        }

        private Task ChangeGridState()
        {
            return Task.Factory.StartNew(delegate ()
            {
                GameRule.ChangeGridState(InputGrid, OutputGrid);
            }
            );
        }
    }
}
