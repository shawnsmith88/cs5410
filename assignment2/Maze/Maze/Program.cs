using System;

namespace Maze
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MazeGameLoop())
                game.Run();
        }
    }
}
