using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    public class Wall
    {
        public bool Status;
        public List<GridItem> GridItems;

        public Wall()
        {
            Status = true;
            GridItems = new List<GridItem>();
        }
    }
}
