using Maze.InputTypes;

namespace Maze.Services
{
    public interface IInputService
    {
        Direction getInputDirection();
        MenuOptions getMenuInput();
        InGameOptions getInGameOptions();
    }
}