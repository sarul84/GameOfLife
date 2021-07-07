using System;
using System.Collections.Generic;

namespace GameOfLife
{
    enum CellTypeEnum
    {
        TopLeftCorner,
        TopRightCorner,
        BottomLeftCorner,
        BottomRightCorner,
        TopSide,
        BottomSide,
        LeftSide,
        RightSide,
        Center,
        OuterTopSide,
        OuterRightSide,
        OuterBottomSide,
        OuterLeftSide,
        None
    }

    struct CoOrdinates
    {
        public int X;
        public int Y;
        public CoOrdinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal static class GarbageCollection
    {
        public static Dictionary<CellTypeEnum, List<CoOrdinates>> ReachableCells;

        public static void InitializeReachableCells()
        {
            CellTypeEnum innerCell;
            ReachableCells = new Dictionary<CellTypeEnum, List<CoOrdinates>>();

            innerCell = CellTypeEnum.TopLeftCorner;
            List<CoOrdinates> TopLeftCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(0, 1),
                new CoOrdinates(1, 1),
                new CoOrdinates(1, 0)
            };
            ReachableCells.Add(innerCell, TopLeftCoOrdinateList);

            innerCell = CellTypeEnum.TopRightCorner;
            List<CoOrdinates> TopRightCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(1, 0),
                new CoOrdinates(1, -1),
                new CoOrdinates(0, -1)
            };
            ReachableCells.Add(innerCell, TopRightCoOrdinateList);

            innerCell = CellTypeEnum.BottomLeftCorner;
            List<CoOrdinates> BottomLeftCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(-1, 0),
                new CoOrdinates(-1, 1),
                new CoOrdinates(0, 1)
            };
            ReachableCells.Add(innerCell, BottomLeftCoOrdinateList);

            innerCell = CellTypeEnum.BottomRightCorner;
            List<CoOrdinates> BottomRightCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(0, -1),
                new CoOrdinates(-1, -1),
                new CoOrdinates(-1, 0)
            };
            ReachableCells.Add(innerCell, BottomRightCoOrdinateList);

            innerCell = CellTypeEnum.TopSide;
            List<CoOrdinates> TopSideCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(0, 1),
                new CoOrdinates(1, 1),
                new CoOrdinates(1, 0),
                new CoOrdinates(1, -1),
                new CoOrdinates(0, -1)
            };
            ReachableCells.Add(innerCell, TopSideCoOrdinateList);

            innerCell = CellTypeEnum.BottomSide;
            List<CoOrdinates> BottomSideCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(0, -1),
                new CoOrdinates(-1, -1),
                new CoOrdinates(-1, 0),
                new CoOrdinates(-1, 1),
                new CoOrdinates(0, 1)
            };
            ReachableCells.Add(innerCell, BottomSideCoOrdinateList);

            innerCell = CellTypeEnum.LeftSide;
            List<CoOrdinates> LeftSideCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(-1, 0),
                new CoOrdinates(-1, 1),
                new CoOrdinates(0, 1),
                new CoOrdinates(1, 1),
                new CoOrdinates(1, 0)
            };
            ReachableCells.Add(innerCell, LeftSideCoOrdinateList);

            innerCell = CellTypeEnum.RightSide;
            List<CoOrdinates> RightSideCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(1, 0),
                new CoOrdinates(1, -1),
                new CoOrdinates(0, -1),
                new CoOrdinates(-1, -1),
                new CoOrdinates(-1, 0)
            };
            ReachableCells.Add(innerCell, RightSideCoOrdinateList);

            innerCell = CellTypeEnum.Center;
            List<CoOrdinates> CenterCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(-1, 0),
                new CoOrdinates(-1, 1),
                new CoOrdinates(0, 1),
                new CoOrdinates(1, 1),
                new CoOrdinates(1, 0),
                new CoOrdinates(1, -1),
                new CoOrdinates(0, -1),
                new CoOrdinates(-1, -1)
            };
            ReachableCells.Add(innerCell, CenterCoOrdinateList);

            innerCell = CellTypeEnum.OuterTopSide;
            List<CoOrdinates> OuterTopSideCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(1, -1),
                new CoOrdinates(1, 0),
                new CoOrdinates(1, 1)
            };
            ReachableCells.Add(innerCell, OuterTopSideCoOrdinateList);

            innerCell = CellTypeEnum.OuterRightSide;
            List<CoOrdinates> OuterRightSideCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(-1, -1),
                new CoOrdinates(0, -1),
                new CoOrdinates(1, -1)
            };
            ReachableCells.Add(innerCell, OuterRightSideCoOrdinateList);

            innerCell = CellTypeEnum.OuterBottomSide;
            List<CoOrdinates> OuterBottomSideCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(-1, -1),
                new CoOrdinates(-1, 0),
                new CoOrdinates(-1, 1)
            };
            ReachableCells.Add(innerCell, OuterBottomSideCoOrdinateList);

            innerCell = CellTypeEnum.OuterLeftSide;
            List<CoOrdinates> OuterLeftSideCoOrdinateList = new List<CoOrdinates>
            {
                new CoOrdinates(-1, 1),
                new CoOrdinates(0, 1),
                new CoOrdinates(1, 1)
            };
            ReachableCells.Add(innerCell, OuterLeftSideCoOrdinateList);

        }

        public static CellTypeEnum GetCellType(Grid grid, CoOrdinates coOrdinates)
        {
            if ((coOrdinates.X < -1 || coOrdinates.X > grid.RowCount) || (coOrdinates.Y < -1 || coOrdinates.Y > grid.ColumnCount))
            {
                throw new ArgumentOutOfRangeException("Invalid Index value: must be greater than or equal to minus one and less than or equal to Row count");
            }
            CellTypeEnum enumCellType = CellTypeEnum.None;
            if (coOrdinates.X == 0 && coOrdinates.Y == 0)
                enumCellType = CellTypeEnum.TopLeftCorner;
            else if (coOrdinates.X == 0 && coOrdinates.Y == grid.ColumnCount - 1)
                enumCellType = CellTypeEnum.TopRightCorner;
            else if (coOrdinates.X == grid.RowCount - 1 && coOrdinates.Y == 0)
                enumCellType = CellTypeEnum.BottomLeftCorner;
            else if (coOrdinates.X == grid.RowCount - 1 && coOrdinates.Y == grid.ColumnCount - 1)
                enumCellType = CellTypeEnum.BottomRightCorner;
            else if (coOrdinates.X == 0 && (coOrdinates.Y > 0 && coOrdinates.Y < grid.ColumnCount - 1))
                enumCellType = CellTypeEnum.TopSide;
            else if (coOrdinates.X == grid.RowCount - 1 && (coOrdinates.Y > 0 && coOrdinates.Y < grid.ColumnCount - 1))
                enumCellType = CellTypeEnum.BottomSide;
            else if ((coOrdinates.X > 0 && coOrdinates.X < grid.RowCount - 1) && coOrdinates.Y == 0)
                enumCellType = CellTypeEnum.LeftSide;
            else if ((coOrdinates.X > 0 && coOrdinates.X < grid.RowCount - 1) && coOrdinates.Y == grid.ColumnCount - 1)
                enumCellType = CellTypeEnum.RightSide;
            else if ((coOrdinates.X > 0 && coOrdinates.X < grid.RowCount - 1) && (coOrdinates.Y > 0 && coOrdinates.Y < grid.ColumnCount - 1))
                enumCellType = CellTypeEnum.Center;
            else if (coOrdinates.X == -1 && (coOrdinates.Y > 0 && coOrdinates.Y < grid.ColumnCount - 1))
                enumCellType = CellTypeEnum.OuterTopSide;
            else if ((coOrdinates.X > 0 && coOrdinates.X < grid.RowCount - 1) && coOrdinates.Y == grid.ColumnCount)
                enumCellType = CellTypeEnum.OuterRightSide;
            else if (coOrdinates.X == grid.RowCount && (coOrdinates.Y > 0 && coOrdinates.Y < grid.ColumnCount - 1))
                enumCellType = CellTypeEnum.OuterBottomSide;
            else if ((coOrdinates.X > 0 && coOrdinates.X < grid.RowCount - 1) && coOrdinates.Y == -1)
                enumCellType = CellTypeEnum.OuterLeftSide;
            return enumCellType;
        }
    }
}
