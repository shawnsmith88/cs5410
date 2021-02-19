using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Maze
{
    class PrimGenerationAlgorithm : IGenerationAlgorithm
    {
        private const int START_X = 0;
        private const int START_Y = 0;
        private Maze _maze;
        public Maze GenerateMaze(int dimesions)
        {
            _maze = new Maze(dimesions);
            List<List<bool>> visited = GenerateVisitedList(dimesions);
            int x = START_X;
            int y = START_Y;
            visited[x][y] = true;
            List<Tuple<GridItem,Side>> walls = new List<Tuple<GridItem, Side>>();
            Random random = new Random();

            if (y != 0)
            {
                walls.Add(new Tuple<GridItem,Side>(_maze.Grid[x][y], Side.TOP));
            }

            if (y < _maze.Dimensions)
            {
                walls.Add(new Tuple<GridItem,Side>(_maze.Grid[x][y], Side.BOTTOM));
            }

            if (x != 0)
            {
                walls.Add(new Tuple<GridItem,Side>(_maze.Grid[x][y], Side.LEFT));
            }

            if (x < _maze.Dimensions)
            {
                walls.Add(new Tuple<GridItem,Side>(_maze.Grid[x][y], Side.RIGHT));
            }

            while (walls.Count > 0)
            {
                //pick a random wall from list
                var index = random.Next(0, walls.Count);
                var wall = walls[index];
                // if only one of the two cells that the  wall divides are visited, then
                if (!BothVisited(wall, visited))
                {
                    wall.Item1.setWall(wall.Item2, false);

                    Tuple<int, int> delta = GetChangeDirection(wall.Item2);
                    x = wall.Item1.X + delta.Item1;
                    y = wall.Item1.Y + delta.Item2;
                    if (x < 0 || y < 0 || x > _maze.Dimensions-1 || y > _maze.Dimensions-1)
                    {
                        continue;
                    }

                    visited[x][y] = true;
                    bool addTop = (y - 1 >= 0);
                    bool addBottom = (y + 1 <= _maze.Dimensions-1);
                    bool addLeft = (x - 1 >= 0);
                    bool addRight = (x + 1 <= _maze.Dimensions-1);
                    
                    if (addTop)
                    {
                        if (!isInTupleArray(walls, _maze.Grid[x][y], Side.TOP))
                        {
                            walls.Add(new Tuple<GridItem, Side>(_maze.Grid[x][y], Side.TOP));
                        }
                    }

                    if (addBottom)
                    {
                        if (!isInTupleArray(walls, _maze.Grid[x][y], Side.BOTTOM))
                        {
                            walls.Add(new Tuple<GridItem, Side>(_maze.Grid[x][y], Side.BOTTOM));
                        }
                    }

                    if (addLeft)
                    {
                        if (!isInTupleArray(walls, _maze.Grid[x][y], Side.LEFT))
                        {
                            walls.Add(new Tuple<GridItem, Side>(_maze.Grid[x][y], Side.LEFT));
                        }
                    }

                    if (addRight)
                    {
                        if (!isInTupleArray(walls, _maze.Grid[x][y], Side.RIGHT))
                        {
                            walls.Add(new Tuple<GridItem, Side>(_maze.Grid[x][y], Side.RIGHT));
                        }
                    }
                }
                walls.Remove(wall);
                // make the wall a passage and mark the unvisited cell as part of the maze
                // add the neighboring walls of the cell to the wall list
                // remove the wall from the list
            }

            return _maze;
        }

        private bool BothVisited(Tuple<GridItem,Side> wall, List<List<bool>> visited)
        {
            int x = wall.Item1.X;
            int y = wall.Item1.Y;
            if (x < 0 || y < 0 || x > _maze.Dimensions-1 || y > _maze.Dimensions-1) 
            { 
                return true;
            }
            int changeX = 0, changeY = 0;
            switch (wall.Item2)
            {
                case Side.TOP:
                    if (x < _maze.Dimensions-1)
                    {
                        changeX = 1;
                    }
                    break;
                case Side.BOTTOM:
                    if (x > 0)
                    {
                        changeX = -1;
                    }
                    break;
                case Side.LEFT:
                    if (y > 0)
                    {
                        changeY = -1;
                    }
                    break;
                case Side.RIGHT:
                    if (y < _maze.Dimensions-1)
                    {
                        changeY = 1;
                    }
                    break;
                default:
                    return false;
            }

            return visited[x][y] && visited[x + changeX][y + changeY];
        }

        private Tuple<int, int> GetChangeDirection(Side side)
        {
            int changeX = 0, changeY = 0;
            switch (side)
            {
                case Side.TOP:
                    changeX = 1;
                    break;
                case Side.BOTTOM: 
                    changeX = -1;
                    break;
                case Side.LEFT: 
                    changeY = -1;
                    break;
                case Side.RIGHT:
                    changeY = 1;
                    break;
                default:
                    return new Tuple<int, int>(changeX,changeY);
            }

            return new Tuple<int, int>(changeX, changeY);
        }

        private bool isInTupleArray(List<Tuple<GridItem, Side>> tuples, GridItem gridItem, Side side)
        {
            foreach (var tuple in tuples)
            {
                if (tuple.Item1.Equals(gridItem) && tuple.Item2.Equals(side))
                {
                    return true;
                }
            }

            return false;
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
