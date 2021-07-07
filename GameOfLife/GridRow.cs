using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class GridRow
    {
        public List<Cell> Cells { get; set; }

        public GridRow()
        {
            Cells = new List<Cell>();
        }

        public Cell this[int y]
        {
            get { if (Cells.Count >= y) throw new ArgumentOutOfRangeException("Argument out of bound"); return Cells[y]; }
            set { if (Cells.Count >= y) throw new ArgumentOutOfRangeException("Argument out of bound"); Cells[y] = value; }
        }

        public void AddGridCell(Cell cell)
        {
            Cells.Add(cell);
        }

        public void InsertGridCell(int index, Cell cell, int columnCount)
        {
            if (index < 0 || index >= columnCount) throw new ArgumentOutOfRangeException("Invalid Index value: must be greater than zero and less than Column count");
            Cells.Insert(index, cell);
        }
    }
}
