using System;
using Maze.InputTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Maze.Services
{
    public interface ISpriteService
    {
        GameObject renderSprite(GraphicsDeviceManager graphicsDeviceManager, Maze maze, ContentManager content);

        GameObject move(GraphicsDeviceManager graphicsDeviceManager, Maze maze, GameObject gameObject,
            Direction direction, ContentManager content);
        void Draw(GameObject gameObject, SpriteBatch spriteBatch);
    }
}