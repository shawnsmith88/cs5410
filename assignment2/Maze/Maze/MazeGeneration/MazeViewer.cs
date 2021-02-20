using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    class MazeViewer
    {
        private Maze _maze;
        private IGenerationAlgorithm _generationAlgorithm;
        public void ViewMaze()
        {
            _generationAlgorithm = new PrimGenerationAlgorithm();
            _maze = _generationAlgorithm.GenerateMaze(5);
            Console.WriteLine("Generating Maze");
            foreach (List<GridItem> gridItems in _maze.Grid)
            {
                foreach (GridItem gridItem in gridItems)
                {
                    if (gridItem.Left)
                    {
                        Console.Write("L");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                    if (gridItem.Top)
                    {
                        Console.Write("T");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                    if (gridItem.Bottom)
                    {
                        Console.Write("B");
                    }
                    else 
                    {
                        Console.Write("-");
                    }

                    if (gridItem.Right)
                    {
                        Console.Write("R");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
    }
}
