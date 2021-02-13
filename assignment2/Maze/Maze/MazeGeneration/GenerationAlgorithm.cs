using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    public interface GenerationAlgorithm
    {
        Maze GenerateMaze(int dimesions);
    }
}
