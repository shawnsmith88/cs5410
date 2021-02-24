using Maze.InputTypes;
using Microsoft.Xna.Framework.Input;

namespace Maze.Services
{
    public class KeyboardInputService: IInputService
    {
        public Direction getInputDirection()
        {
            throw new System.NotImplementedException();
        }

        public MenuOptions getMenuInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (var key in keyboardState.GetPressedKeys())
            {
                if (key == Keys.F1)
                {
                    return MenuOptions.StartGame5x5;
                }

                if (key == Keys.F2)
                {
                    return MenuOptions.StartGame10x10;
                }

                if (key == Keys.F3)
                {
                    return MenuOptions.StartGame15x15;
                }

                if (key == Keys.F4)
                {
                    return MenuOptions.StartGame20x20;
                }

                if (key == Keys.F5)
                {
                    return MenuOptions.HighScores;
                }

                if (key == Keys.F6)
                {
                    return MenuOptions.Credits;
                }

            }

            return MenuOptions.None;
        }
    }
}