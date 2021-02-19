using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    public class GridItem
    {
        // Each boolean indicates whether or not there is a wall, all are initialized to true.
        public bool Left;
        public bool Right;
        public bool Top;
        public bool Bottom;
        public int X;
        public int Y;

        public GridItem()
        {
            Left = true;
            Right = true;
            Top = true;
            Bottom = true;
        }

        public bool getWall(Side side)
        {
            switch (side)
            {
                case Side.LEFT:
                    return this.Left;
                case Side.RIGHT:
                    return this.Right;
                case Side.TOP:
                    return this.Top;
                case Side.BOTTOM:
                    return this.Bottom;
                default:
                    return false;
            }
        }

        public void setWall(Side side, bool value)
        {
            switch (side)
            {
                case Side.TOP:
                    this.Top = value;
                    break;
                case Side.LEFT:
                    this.Left = value;
                    break;
                case Side.BOTTOM:
                    this.Bottom = value;
                    break;
                case Side.RIGHT:
                    this.Right = value;
                    break;
                default:
                    return;
            }
        }
        
    }
}
