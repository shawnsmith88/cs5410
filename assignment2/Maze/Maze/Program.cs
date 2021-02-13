using System;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MazeViewer mazeViewer = new MazeViewer();
            mazeViewer.ViewMaze();
        }
    }
}
