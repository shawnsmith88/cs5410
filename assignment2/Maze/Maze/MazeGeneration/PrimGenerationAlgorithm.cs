using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    class PrimGenerationAlgorithm : GenerationAlgorithm
    {
        private const int START_X = 0;
        private const int START_Y = 0;
        private Maze Maze;
        public Maze GenerateMaze(int dimesions)
        {
            Maze = new Maze(dimesions);
            List<List<bool>> visited = GenerateVisitedList(dimesions);
            int x = START_X;
            int y = START_Y;
            visited[x][y] = true;
            List<Wall> walls = new List<Wall>();
            if (x > 0)
            {
                walls.Add(Maze.Grid[x][y].Top);
            }
            if (y > 0)
            {
                walls.Add(Maze.Grid[x][y].Left);
            }
            if (x < dimesions)
            {
                walls.Add(Maze.Grid[x][y].Bottom);
            }
            if (y < dimesions)
            {
                walls.Add(Maze.Grid[x][y].Right);
            }
            Random random = new Random();

            while (walls.Count > 0)
            {
                //pick a random wall from list
                int index = random.Next(0, walls.Count);
                Wall wall = walls[index];
                // if only one of the two cells that the  wall divides are visited, then
                if (!BothVisited(wall, visited))
                {
                    wall.Status = false;
                    foreach (GridItem gridItem in wall.GridItems)
                    {
                        visited[gridItem.X][gridItem.Y] = true;
                        if (!walls.Contains(gridItem.Bottom) && gridItem.Bottom.Status == true)
                        {
                            walls.Add(gridItem.Bottom);
                        }
                        if (!walls.Contains(gridItem.Top) && gridItem.Bottom.Status == true)
                        {
                            walls.Add(gridItem.Top);
                        }
                        if (!walls.Contains(gridItem.Left) && gridItem.Bottom.Status == true)
                        {
                            walls.Add(gridItem.Left);
                        }
                        if (!walls.Contains(gridItem.Right) && gridItem.Bottom.Status == true)
                        {
                            walls.Add(gridItem.Right);
                        }
                    }
                }
                walls.Remove(wall);
                // make the wall a passage and mark the unvisited cell as part of the maze
                // add the neighboring walls of the cell to the wall list
                // remove the wall from the list
            }

            return Maze;
        }

        private bool BothVisited(Wall wall, List<List<bool>> visited)
        {
            foreach(GridItem gridItem in wall.GridItems)
            {
                if (!visited[gridItem.X][gridItem.Y])
                {
                    return false;
                }
            }
            return true;
        }

        private List<List<bool>> GenerateVisitedList(int dimensions)
        {
            List<List<bool>> list = new List<List<bool>>();
            for (int i = 0; i < dimensions; i++)
            {
                List<bool> inner = new List<bool>();
                for (int j = 0; j < dimensions; j++)
                {
                    inner.Add(false);
                }
                list.Add(inner);
            }
            return list;
        }
    }
}
