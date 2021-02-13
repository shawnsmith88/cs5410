using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    class MazeViewer
    {
        private Maze maze;
        private GenerationAlgorithm generationAlgorithm;
        public void ViewMaze()
        {
            generationAlgorithm = new PrimGenerationAlgorithm();
            maze = generationAlgorithm.GenerateMaze(25);
            Console.WriteLine("Generating Maze");
            foreach (List<GridItem> gridItems in maze.Grid)
            {
                foreach (GridItem gridItem in gridItems)
                {
                    if (gridItem.Top.Status)
                    {
                        Console.Write("T");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                    if (gridItem.Bottom.Status)
                    {
                        Console.Write("B");
                    }
                    else 
                    {
                        Console.Write("-");
                    }
                    if (gridItem.Left.Status)
                    {
                        Console.Write("L");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                    if (gridItem.Right.Status)
                    {
                        Console.Write("R");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                    Console.Write("\t");
                }
                Console.Write("\n");
            }
        }
    }
}
