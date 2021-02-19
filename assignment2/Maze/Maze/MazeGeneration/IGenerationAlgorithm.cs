using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    public interface IGenerationAlgorithm
    {
        Maze GenerateMaze(int dimesions);
    }
}
