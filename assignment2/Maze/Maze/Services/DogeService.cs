using System;
using Maze.InputTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Maze.Services
{
    public class DogeService: ISpriteService
    {
        public GameObject renderSprite(GraphicsDeviceManager graphicsDeviceManager, Maze maze, ContentManager content)
        {
            int windowWidth = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Width);
            int windowHeight = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Height);
            int mazeWidth = (int) Math.Round(windowWidth * 0.3);
            int mazeHeight = (int) Math.Round(windowHeight * 0.3);
            int initX = (int) Math.Round(windowWidth * 0.5) - mazeWidth / 2;
            int initY = (int) Math.Round(windowHeight * 0.5) - mazeHeight;
            int drawXAt = initX;
            int drawYAt = initY;
            int xDiff = mazeWidth / maze.Dimensions;
            int yDiff = mazeWidth / maze.Dimensions;

            GameObject gameObject = new DogeSprite();
            gameObject.Rectangle = new Rectangle(drawXAt,drawYAt,xDiff,yDiff);
            gameObject.Texture = content.Load<Texture2D>(MazeTextures.Doge);
            gameObject.X = 0;
            gameObject.Y = 0;
            return gameObject;
        }

        public GameObject move(GraphicsDeviceManager graphicsDeviceManager, Maze maze, GameObject gameObject, Direction direction)
        {
            int lastX = gameObject.Rectangle.X;
            int lastY = gameObject.Rectangle.Y;
            int windowWidth = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Width);
            int windowHeight = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Height);
            int mazeWidth = (int) Math.Round(windowWidth * 0.3);
            int mazeHeight = (int) Math.Round(windowHeight * 0.3);
            int xDiff = mazeWidth / maze.Dimensions;
            int yDiff = mazeWidth / maze.Dimensions;


            switch (direction)
            {
                case Direction.Down:
                    if (gameObject.Y < maze.Dimensions-1)
                    {
                        if (!maze.Grid[gameObject.X][gameObject.Y + 1].Top)
                        {
                            lastY += yDiff;
                            gameObject.Y += 1;
                        }
                    }

                    break;
                case Direction.Up:
                    if (gameObject.Y > 0)
                    {
                        if (!maze.Grid[gameObject.X][gameObject.Y - 1].Bottom)
                        {
                            lastY -= yDiff;
                            gameObject.Y -= 1;
                        }
                    }

                    break;
                case Direction.Left:
                    if (gameObject.X > 0)
                    if (!maze.Grid[gameObject.X-1][gameObject.Y].Right)
                    {
                        lastX -= xDiff;
                        gameObject.X -= 1;
                    }

                    break;
                case Direction.Right:
                    if (gameObject.X < maze.Dimensions - 1)
                    {
                        if (!maze.Grid[gameObject.X + 1][gameObject.Y].Left)
                        {
                            lastX += xDiff;
                            gameObject.X += 1;
                        }
                    }

                    break;
                default:
                    break;
            }

            gameObject.Rectangle = new Rectangle(lastX, lastY, xDiff, yDiff);
            return gameObject;
        }

        public void Draw(GameObject gameObject, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameObject.Texture, gameObject.Rectangle, Color.White);
        }
    }
}