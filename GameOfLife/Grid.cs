using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Grid
    {
        public List<GridRow> Rows { set; get; }

        public Grid(int rows, int columns)
        {
            InitializeGrid(rows, columns);
        }

        public Cell this[int x, int y]
        {
            get
            {
                if (Rows.Count <= x || ColumnCount <= y)
                {
                    throw new ArgumentOutOfRangeException("Argument out of bound");
                }
                return Rows[x].Cells[y];
            }
            set
            {
                if (Rows.Count <= x || ColumnCount <= y)
                {
                    throw new ArgumentOutOfRangeException("Argument out of bound");
                }

                Rows[x].Cells[y] = value;
            }
        }

        public GridRow this[int x]
        {
            get
            {
                if (Rows.Count <= x) throw new ArgumentOutOfRangeException("Argument out of bound");
                return Rows[x];
            }
            set
            {
                if (Rows.Count <= x) throw new ArgumentOutOfRangeException("Argument out of bound");
                Rows[x] = value;
            }
        }

        public int RowCount => Rows.Count;

        public int ColumnCount { set; get; }

        public void ReInitialize()
        {
            InitializeGrid(RowCount, ColumnCount);
        }

        private void InitializeGrid(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0) throw new ArgumentOutOfRangeException("Row and Column size must be greater than zero");

            Rows = new List<GridRow>();
            for (int i = 0; i < rows; i++)
            {
                GridRow row = new GridRow();
                for (int j = 0; j < columns; j++)
                {
                    Cell cell = new Cell(false);
                    row.AddGridCell(cell);
                }
                Rows.Add(row);
            }
            ColumnCount = columns;
        }

        public void ToggleCell(int x, int y)
        {
            if (Rows.Count <= x || ColumnCount <= y) throw new ArgumentNullException("Cell doesn't have data for required indexes");
            this[x, y].IsAlive = !this[x, y].IsAlive;
        }

        public void InsertRow(int index, GridRow row)
        {
            if (index < 0 || index >= RowCount) throw new ArgumentOutOfRangeException("Invalid Index value: must be greater than or equal to zero and less than Row count");
            Rows.Insert(index, row);
        }

        public void AddRow(GridRow row)
        {
            Rows.Add(row);
        }
    }
}
