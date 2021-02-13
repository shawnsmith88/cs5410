using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    public class Maze
    {
        public List<List<GridItem>> Grid;
        public int Dimensions;
        public  Maze(int dimensions)
        {
            Dimensions = dimensions;
            Grid = new List<List<GridItem>>();
            for (int i = 0; i < dimensions; i++)
            {
                List<GridItem> itemList = new List<GridItem>();
                for (int j = 0; j < dimensions; j++)
                {
                    GridItem gridItem = new GridItem();
                    gridItem.Bottom.GridItems.Add(gridItem);
                    gridItem.Top.GridItems.Add(gridItem);
                    if (i > 0)
                    {
                        gridItem.Top = Grid[i - 1][j].Bottom;
                    }
                    if (j > 0)
                    {
                        gridItem.Left = itemList[j - 1].Right;
                    }
                    gridItem.Y = j;
                    gridItem.X = i;
                    itemList.Add(gridItem);
                }
                Grid.Add(itemList);
            }
        }
    }
}
