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
                    //wall.Item1.setWall(wall.Item2, false);

                    Tuple<int, int> delta = GetChangeDirection(wall.Item2);
                    var prevX = wall.Item1.X;
                    var prevY = wall.Item1.Y;
                    x = wall.Item1.X + delta.Item1;
                    y = wall.Item1.Y + delta.Item2;
                    if (x < 0 || y < 0 || x > _maze.Dimensions || y > _maze.Dimensions)
                    {
                        continue;
                    }
                    RemoveCorrectWalls(prevX, prevY, delta);

                    visited[x][y] = true;
                    bool addTop = (y > 0);
                    bool addBottom = (y < _maze.Dimensions);
                    bool addLeft = (x > 0);
                    bool addRight = (x < _maze.Dimensions);
                    
                    if (addTop)
                    {
                        if (!isInTupleArray(walls, _maze.Grid[x][y], Side.TOP))
                        {
                            Tuple<GridItem, Side> tuple = new Tuple<GridItem,Side>(_maze.Grid[x][y], Side.TOP);
                            walls.Add(tuple);
                        }
                    }

                    if (addBottom)
                    {
                        if (!isInTupleArray(walls, _maze.Grid[x][y], Side.BOTTOM))
                        {
                            Tuple<GridItem, Side> tuple = new Tuple<GridItem,Side>(_maze.Grid[x][y], Side.BOTTOM);
                            walls.Add(tuple);
                        }
                    }

                    if (addLeft)
                    {
                        if (!isInTupleArray(walls, _maze.Grid[x][y], Side.LEFT))
                        {
                            Tuple<GridItem, Side> tuple = new Tuple<GridItem,Side>(_maze.Grid[x][y], Side.LEFT);
                            walls.Add(tuple);
                        }
                    }

                    if (addRight)
                    {
                        if (!isInTupleArray(walls, _maze.Grid[x][y], Side.RIGHT))
                        {
                            Tuple<GridItem, Side> tuple = new Tuple<GridItem,Side>(_maze.Grid[x][y], Side.RIGHT);
                            walls.Add(tuple);
                        }
                    }
                }
                walls.RemoveAt(index);
                // make the wall a passage and mark the unvisited cell as part of the maze
                // add the neighboring walls of the cell to the wall list
                // remove the wall from the list
            }

            // for (int i = 0; i < _maze.Grid.Count; i++)
            // {
            //     var gl = _maze.Grid[i];
            //     for (int j = 0; j < gl.Count; j++)
            //     {
            //         var gi = gl[j];
            //         if (j == 0)
            //         {
            //             gi.Left = true;
            //         }
            //
            //         if (j == _maze.Grid.Count-1)
            //         {
            //             gi.Right = true;
            //         }
            //
            //         if (i == 0)
            //         {
            //             gi.Top = true;
            //         }
            //
            //         if (i == gl.Count-1)
            //         {
            //             gi.Bottom = true;
            //         }
            //     }
            // }
            

            return _maze;
        }

        private void RemoveCorrectWalls(int x, int y, Tuple<int, int> deltas)
        {
            var dx = deltas.Item1;
            var dy = deltas.Item2;
                if (deltas.Item1 == 1)
                {
                    _maze.Grid[x][y].Right = false;
                    _maze.Grid[x+1][y].Left = false;
                }

                if (deltas.Item1 == -1)
                {
                    _maze.Grid[x][y].Left = false;
                    _maze.Grid[x-1][y].Right = false;
                }

                if (deltas.Item2 == 1)
                {
                    _maze.Grid[x][y].Bottom = false;
                    _maze.Grid[x][y+1].Top = false;
                }

                if (deltas.Item2 == -1)
                {
                    _maze.Grid[x][y].Top = false;
                    _maze.Grid[x][y-1].Bottom = false;
                }
        }

        private bool BothVisited(Tuple<GridItem,Side> wall, List<List<bool>> visited)
        {
            int x = wall.Item1.X;
            int y = wall.Item1.Y;
            Console.Write("X: " + x + " Y: " + y);
            if (x < 0 || y < 0 || x > _maze.Dimensions || y > _maze.Dimensions) 
            { 
                return true;
            }
            int changeX = 0, changeY = 0;
            switch (wall.Item2)
            {
                case Side.TOP:
                    if (y >= 1)
                    {
                        changeY = -1;
                    }
                    break;
                case Side.BOTTOM:
                    if (y < _maze.Dimensions-2)
                    {
                        changeX = 1;
                    }
                    break;
                case Side.LEFT:
                    if (x > 0)
                    {
                        changeX = -1;
                    }
                    break;
                case Side.RIGHT:
                    if (x < _maze.Dimensions-2)
                    {
                        changeX = 1;
                    }
                    break;
                default:
                    return false;
            }
            Console.WriteLine(" dx: " + changeX + " dy: " + changeY);

            return visited[x][y] && visited[x + changeX][y + changeY];
        }

        private Tuple<int, int> GetChangeDirection(Side side)
        {
            int changeX = 0, changeY = 0;
            switch (side)
            {
                case Side.BOTTOM:
                    changeY = 1;
                    break;
                case Side.TOP: 
                    changeY = -1;
                    break;
                case Side.LEFT: 
                    changeX = -1;
                    break;
                case Side.RIGHT:
                    changeX = 1;
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
