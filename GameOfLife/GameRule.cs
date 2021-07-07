using System;
using System.Collections.Generic;

namespace GameOfLife
{
    class GameRule
    {
        public static void ChangeCellsState(Grid inputGrid, Grid outputGrid, CoOrdinates coOrdinates)
        {
            int liveNeighbourCount = CountAliveNeighbours(inputGrid, coOrdinates);
            lock (outputGrid)
            {
                if (IsAliveInNextState(inputGrid[coOrdinates.X, coOrdinates.Y], liveNeighbourCount))
                {
                    outputGrid[coOrdinates.X, coOrdinates.Y].IsAlive = true;
                }

            }

        }

        private static int CountAliveNeighbours(Grid grid, CoOrdinates coOrdinates)
        {
            int liveNeighbours = 0;
            
            CellTypeEnum enumInnerCell = GarbageCollection.GetCellType(grid, coOrdinates);
            List<CoOrdinates> reachableCells = new List<CoOrdinates>();
            
            GarbageCollection.ReachableCells.TryGetValue(enumInnerCell, out reachableCells);
            if (reachableCells.Count == 0) throw new ArgumentNullException("Cannot find reachable co-ordinates");
            foreach (CoOrdinates coOrds in reachableCells)
            {
                liveNeighbours += IsAliveNeighbour(grid, coOrdinates, coOrds);
            }
            return liveNeighbours;
        }

        private static int IsAliveNeighbour(Grid grid, CoOrdinates baseCoOrdinates, CoOrdinates offSetCoOrdinates)
        {
            int live = 0; 
            int x = baseCoOrdinates.X + offSetCoOrdinates.X; 
            int y = baseCoOrdinates.Y + offSetCoOrdinates.Y;
            
            if ((x >= 0 && x < grid.RowCount) && y >= 0 && y < grid.ColumnCount)
            {
                live = grid[x, y].IsAlive ? 1 : 0;
            }

            return live;
        }

        private static bool IsAliveInNextState(Cell cell, int liveNeighbourCount)
        {
            bool alive = false;
            if (cell.IsAlive)
            {
                if (liveNeighbourCount == 2 || liveNeighbourCount == 3) //
                {
                    alive = true;
                }
            }            
            else if (liveNeighbourCount == 3)
            {
                alive = true;
            }
            return alive;
        }

        public static void ChangeGridState(Grid inputGrid, Grid outputGrid)
        {
            CheckRowGrowth(inputGrid, outputGrid, -1);
            CheckRowGrowth(inputGrid, outputGrid, inputGrid.RowCount);
            CheckColumnGrowth(inputGrid, outputGrid, -1);
            CheckColumnGrowth(inputGrid, outputGrid, inputGrid.ColumnCount);
        }

        private static void CheckColumnGrowth(Grid inputGrid, Grid outputGrid, int colId)
        {
            bool columnCreatedFlag = false;
           
            for (int i = 1; i < inputGrid.RowCount - 1; i++)
            {
                if (CountAliveNeighbours(inputGrid, new CoOrdinates(i, colId)) == 3)
                {
                    if (columnCreatedFlag == false)
                    {
                        
                        for (int k = 0; k < outputGrid.RowCount; k++)
                        {
                            Cell newDeadCell = new Cell(false);
                            if (colId == -1)
                            {
                                outputGrid[k].InsertGridCell(0, newDeadCell, outputGrid.ColumnCount);
                            }
                            else
                            {
                                outputGrid[k].AddGridCell(newDeadCell);
                            }
                        }
                       
                        outputGrid.ColumnCount += 1;
                        columnCreatedFlag = true;
                    }
                    int yAxis = (colId == -1) ? 0 : outputGrid.ColumnCount - 1;
                    outputGrid[i, yAxis].IsAlive = true;
                }
            }
        }

        private static void CheckRowGrowth(Grid inputGrid, Grid outputGrid, int rowId)
        {
            bool rowCreatedFlag = false;
            
            for (int j = 1; j < inputGrid.ColumnCount - 1; j++)
            {
                if (CountAliveNeighbours(inputGrid, new CoOrdinates(rowId, j)) == 3)
                {
                    if (rowCreatedFlag == false)
                    {
                        GridRow newRow = new GridRow();
                        
                        for (int k = 0; k < outputGrid.ColumnCount; k++)
                        {
                            Cell newDeadCell = new Cell(false);
                            newRow.AddGridCell(newDeadCell);
                        }
                        
                        if (rowId == -1)
                        {
                            outputGrid.InsertRow(0, newRow);
                        }
                        else
                        {
                            outputGrid.AddRow(newRow);
                        }
                        rowCreatedFlag = true;
                    }

                    int XAxis = (rowId == -1) ? 0 : outputGrid.RowCount - 1;
                    outputGrid[XAxis, j].IsAlive = true;
                }
            }
        }
    }
}
