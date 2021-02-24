using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Maze.Services
{
    public class MazeService
    {
        public MazeService()
        {
        }
        public void render(Maze maze, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, List<GameObject> _gameObjects)
        {
            int windowWidth = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Width);
            int windowHeight = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Height);
            int mazeWidth = (int) Math.Round(windowWidth * 0.3);
            int mazeHeight = (int) Math.Round(windowHeight * 0.3);
            int initX = (int) Math.Round(windowWidth * 0.5) - mazeWidth/2;
            int initY = (int) Math.Round(windowHeight * 0.5) - mazeHeight;
            int drawXAt = initX;
            int drawYAt = initY;
            int xDiff = mazeWidth / maze.Dimensions;
            int yDiff = mazeWidth / maze.Dimensions;
            int secondXDif = xDiff / 10;
            int secondYDif = yDiff / 10;
            foreach (var list in maze.Grid)
            {
                foreach (var gridItem in list)
                {
                    if (gridItem.Top)
                    {
                        MazeSquare mazeSquare = new MazeSquare();
                        mazeSquare.Rectangle = new Rectangle(drawXAt,drawYAt,xDiff,secondYDif);
                        mazeSquare.Texture = content.Load<Texture2D>(MazeTextures.Horizontal);
                        _gameObjects.Add(mazeSquare);
                    }

                    if (gridItem.Bottom && gridItem.Y == maze.Dimensions-1)
                    {
                        MazeSquare mazeSquare = new MazeSquare();
                        mazeSquare.Rectangle = new Rectangle(drawXAt,drawYAt+yDiff,xDiff,secondYDif);
                        mazeSquare.Texture = content.Load<Texture2D>(MazeTextures.Horizontal);
                        _gameObjects.Add(mazeSquare);
                    }
                    
                    if (gridItem.Right && gridItem.X == maze.Dimensions-1)
                    {
                        MazeSquare mazeSquare = new MazeSquare();
                        mazeSquare.Rectangle = new Rectangle(drawXAt+xDiff,drawYAt,secondXDif,yDiff);
                        mazeSquare.Texture = content.Load<Texture2D>(MazeTextures.Vertical);
                        _gameObjects.Add(mazeSquare);
                    }

                    if (gridItem.Left)
                    {
                        MazeSquare mazeSquare = new MazeSquare();
                        mazeSquare.Rectangle = new Rectangle(drawXAt,drawYAt,secondXDif,yDiff);
                        mazeSquare.Texture = content.Load<Texture2D>(MazeTextures.Vertical);
                        _gameObjects.Add(mazeSquare);
                    }
                    
                    drawYAt += yDiff;
                }

                drawYAt = initY;
                drawXAt += xDiff;
            }
        }

        public void Draw(List<GameObject> _gameObjects, SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects)
            {
                spriteBatch.Draw(gameObject.Texture, gameObject.Rectangle, Color.White);
            }
        }

        private string determineLoadedSquare(GridItem gridItem)
        {
            string toLoad = MazeTextures.All;
            if (gridItem.Top && gridItem.Bottom && gridItem.Left && gridItem.Right)
            {
                toLoad = MazeTextures.All;
            }
            
            else if (gridItem.Top && gridItem.Bottom && gridItem.Left)
            {
                toLoad = MazeTextures.LeftTopBottom;
            }

            else if (gridItem.Top && gridItem.Bottom && gridItem.Right)
            {
                toLoad = MazeTextures.RightTopBottom;
            }

            else if (gridItem.Top && gridItem.Left && gridItem.Right)
            {
                toLoad = MazeTextures.LeftRightTop;
            }

            else if (gridItem.Bottom && gridItem.Left && gridItem.Right)
            {
                toLoad = MazeTextures.LeftRightBottom;
            }

            else if (gridItem.Bottom && gridItem.Top)
            {
                toLoad = MazeTextures.TopBottom;
            }

            else if (gridItem.Left && gridItem.Right)
            {
                toLoad = MazeTextures.LeftRight;
            }
            
            else if (gridItem.Left && gridItem.Top)
            {
                toLoad = MazeTextures.LeftTop;
            }
            
            else if (gridItem.Left && gridItem.Bottom)
            {
                toLoad = MazeTextures.LeftBottom;
            }
            
            else if (gridItem.Right && gridItem.Top)
            {
                toLoad = MazeTextures.RightTop;
            }
            
            else if (gridItem.Right && gridItem.Bottom)
            {
                toLoad = MazeTextures.RightBottom;
            }
            
            else if (gridItem.Bottom)
            {
                toLoad = MazeTextures.Bottom;
            }
            
            else if (gridItem.Left)
            {
                toLoad = MazeTextures.Left;
            }
            
            else if (gridItem.Right)
            {
                toLoad = MazeTextures.Right;
            }
            
            else if (gridItem.Top)
            {
                toLoad = MazeTextures.Top;
            }

            return toLoad;
        }
    }
}