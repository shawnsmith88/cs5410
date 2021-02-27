using System;
using System.Collections.Generic;
using Maze.InputTypes;
using Microsoft.Xna.Framework.Input;

namespace Maze.Services
{
    public class KeyboardInputService: IInputService
    {
        private List<Keys> _upKeys;
        private List<Keys> _downKeys;
        private List<Keys> _rightKeys;
        private List<Keys> _leftKeys;
        private KeyboardState previousKeyboardState;
        private Keys _hintKey;
        private Keys _shortestPathKey;
        private Keys _breadcrumbKey;
        public KeyboardInputService()
        {
            _upKeys = new List<Keys>();
            _downKeys = new List<Keys>();
            _leftKeys = new List<Keys>();
            _rightKeys = new List<Keys>();
            _upKeys.Add(Keys.Up);
            _upKeys.Add(Keys.W);
            _downKeys.Add(Keys.Down);
            _downKeys.Add(Keys.S);
            _leftKeys.Add(Keys.Left);
            _leftKeys.Add(Keys.A);
            _rightKeys.Add(Keys.Right);
            _rightKeys.Add(Keys.D);
            _hintKey = Keys.H;
            _breadcrumbKey = Keys.B;
            _shortestPathKey = Keys.P;
        }

        public InGameOptions getInGameOptions()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (var key in keyboardState.GetPressedKeys())
            {
                if (_breadcrumbKey == key && !isPreviousPressed(key))
                {
                    return InGameOptions.Breadcrumbs;
                }

                if (_hintKey == key && !isPreviousPressed(key))
                {
                    return InGameOptions.Hint;
                }

                if (_shortestPathKey == key && !isPreviousPressed(key))
                {
                    return InGameOptions.ShortestPath;
                }
            }

            return InGameOptions.None;
        }

        public Direction getInputDirection()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Direction toReturn = Direction.None;
            foreach (var key in keyboardState.GetPressedKeys())
            {
                if (_upKeys.Contains(key) && !isPreviousPressed(key))
                {
                    toReturn = Direction.Up;
                }

                if (_downKeys.Contains(key) && !isPreviousPressed(key))
                {
                    toReturn = Direction.Down;
                }

                if (_leftKeys.Contains(key) && !isPreviousPressed(key))
                {
                    toReturn = Direction.Left;
                }

                if (_rightKeys.Contains(key) && !isPreviousPressed(key))
                {
                    toReturn = Direction.Right;
                }
            }

            previousKeyboardState = keyboardState;

            return toReturn;
        }

        private bool isPreviousPressed(Keys trying)
        {
            foreach (var key in previousKeyboardState.GetPressedKeys())
            {
                if (trying == key)
                {
                    return true;
                }
            }

            return false;
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