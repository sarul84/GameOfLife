using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public static class GridExtension
    {
        public static void Display(this Grid grid)
        {
            foreach (GridRow row in grid.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    Console.Write(cell.ToString());
                }
                Console.WriteLine();
            }
        }

        public static void Copy(this Grid sourceGrid, Grid targetGrid)
        {
            MatchSchema(sourceGrid, targetGrid);
            targetGrid.ReInitialize();
            AssignCellValues(sourceGrid, targetGrid);
        }

        private static void MatchSchema(Grid sourceGrid, Grid targetGrid)
        {
            while (targetGrid.RowCount < sourceGrid.RowCount)
            {
                GridRow newRow = new GridRow();
                for (int k = 0; k < targetGrid.ColumnCount; k++)
                {
                    Cell newCell = new Cell(false);
                    newRow.AddGridCell(newCell);
                }
                targetGrid.AddRow(newRow);
            }
            while (targetGrid.ColumnCount < sourceGrid.ColumnCount)
            {
                Cell cell = new Cell(false);
                for (int k = 0; k < targetGrid.RowCount; k++)
                {
                    targetGrid[k].AddGridCell(cell);
                }
                targetGrid.ColumnCount += 1;
            }

        }

        private static void AssignCellValues(Grid sourceGrid, Grid targetGrid)
        {
            for (int i = 0; i < sourceGrid.RowCount; i++)
            {
                for (int j = 0; j < sourceGrid.ColumnCount; j++)
                {
                    targetGrid[i, j].IsAlive = sourceGrid[i, j].IsAlive;
                }
            }
        }
    }
}
