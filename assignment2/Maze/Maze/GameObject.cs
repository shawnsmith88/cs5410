using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maze
{
    public class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Rectangle Rectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}