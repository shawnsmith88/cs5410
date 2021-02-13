using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    public unsafe class GridItem
    {
        // Each boolean indicates whether or not there is a wall, all are initialized to true.
        public Wall Left;
        public Wall Right;
        public Wall Top;
        public Wall Bottom;
        public int X;
        public int Y;

        public GridItem()
        {
            if (Left == null)
            {
                Left = new Wall();
            }
            if (Right == null)
            {
                Right = new Wall();
            }
            if (Top == null)
            {
                Top = new Wall();
            }
            if (Bottom == null)
            {
                Bottom = new Wall();
            }
            Left.Status = true;
            Right.Status = true;
            Top.Status = true;
            Bottom.Status = true;
        }

        public void SetSide(Wall wall, Side side)
        {
            wall.GridItems.Add(this);
            switch(side)
            {
                case Side.LEFT:
                    Left = wall;
                    break;
                case Side.RIGHT:
                    Right = wall;
                    break;
                case Side.TOP:
                    Top = wall;
                    break;
                case Side.BOTTOM:
                    Bottom = wall;
                    break;
            }
        }
    }
}
