using System;
using Maze.InputTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Maze.Services
{
    public class MoonRocketSpriteService : ISpriteService
    {
        public GameObject renderSprite(GraphicsDeviceManager graphicsDeviceManager, Maze maze, ContentManager content)
        {
            int windowWidth = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Width);
            int windowHeight = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Height);
            int mazeWidth = (int) Math.Round(windowWidth * 0.3);
            int mazeHeight = (int) Math.Round(windowHeight * 0.3);
            int initX = (int) Math.Round(windowWidth * 0.5) - mazeWidth / 2;
            int initY = (int) Math.Round(windowHeight * 0.5) - mazeHeight;
            int xDiff = mazeWidth / maze.Dimensions;
            int yDiff = mazeWidth / maze.Dimensions;
            int drawXAt = initX + xDiff * (maze.Dimensions - 1);
            int drawYAt = initY + yDiff * (maze.Dimensions - 1);

            GameObject gameObject = new MoonRocketSprite();
            gameObject.Rectangle = new Rectangle(drawXAt,drawYAt,xDiff,yDiff);
            gameObject.Texture = content.Load<Texture2D>(MazeTextures.MoonRocket);
            return gameObject;
        }

        public GameObject move(GraphicsDeviceManager graphicsDeviceManager, Maze maze, GameObject gameObject, Direction direction, ContentManager content)
        {
            //finish line will not be moving
            return gameObject;
        }

        public void Draw(GameObject gameObject, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameObject.Texture, gameObject.Rectangle, Color.White);
        }
    }
}