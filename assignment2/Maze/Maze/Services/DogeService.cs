using System;
using System.Collections.Generic;
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

        public GameObject move(GraphicsDeviceManager graphicsDeviceManager, Maze maze, GameObject gameObject, Direction direction, ContentManager content)
        {
            DogeSprite dogeSprite = (DogeSprite) gameObject;
            int lastX = gameObject.Rectangle.X;
            int lastY = gameObject.Rectangle.Y;
            int windowWidth = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Width);
            int windowHeight = Math.Abs(graphicsDeviceManager.GraphicsDevice.Viewport.Height);
            int mazeWidth = (int) Math.Round(windowWidth * 0.3);
            int mazeHeight = (int) Math.Round(windowHeight * 0.3);
            int initX = (int) Math.Round(windowWidth * 0.5) - mazeWidth / 2;
            int initY = (int) Math.Round(windowHeight * 0.5) - mazeHeight;
            int xDiff = mazeWidth / maze.Dimensions;
            int yDiff = mazeWidth / maze.Dimensions;

            BreadCrumb breadCrumb = new BreadCrumb();
            breadCrumb.Rectangle = new Rectangle(gameObject.Rectangle.X + 15, gameObject.Rectangle.Y + 15, xDiff - 30, yDiff - 30);
            breadCrumb.Texture = content.Load<Texture2D>(MazeTextures.Breadcrumb);
            breadCrumb.X = gameObject.X;
            breadCrumb.Y = gameObject.Y;
            bool added = false;


            switch (direction)
            {
                case Direction.Down:
                    if (gameObject.Y < maze.Dimensions-1)
                    {
                        if (!maze.Grid[gameObject.X][gameObject.Y + 1].Top)
                        {
                            lastY += yDiff;
                            gameObject.Y += 1;
                            added = true;
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
                            added = true;
                        }
                    }

                    break;
                case Direction.Left:
                    if (gameObject.X > 0)
                    {
                        if (!maze.Grid[gameObject.X - 1][gameObject.Y].Right)
                        {
                            lastX -= xDiff;
                            gameObject.X -= 1;
                            added = true;
                        }
                    }

                    break;
                case Direction.Right:
                    if (gameObject.X < maze.Dimensions - 1)
                    {
                        if (!maze.Grid[gameObject.X + 1][gameObject.Y].Left)
                        {
                            lastX += xDiff;
                            gameObject.X += 1;
                            added = true;
                        }
                    }

                    break;
            }


            if (added)
            {
                dogeSprite.BestPath = GetBestPath(dogeSprite, maze, content, initX, initY, xDiff, yDiff);
                if (dogeSprite.BestPath?.Count > 0)
                {
                    dogeSprite.Hint = dogeSprite.BestPath[dogeSprite.BestPath.Count-1];
                }
                dogeSprite.BreadCrumbs.Add(breadCrumb);
            }
            gameObject.Rectangle = new Rectangle(lastX, lastY, xDiff, yDiff);
            return gameObject;
        }

        public void Draw(GameObject gameObject, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameObject.Texture, gameObject.Rectangle, Color.White);
            DrawBreadcrumbs((DogeSprite) gameObject, spriteBatch);
            DrawBestPath((DogeSprite) gameObject, spriteBatch);
            DrawHint((DogeSprite) gameObject, spriteBatch);
        }

        private void DrawHint(DogeSprite dogeSprite, SpriteBatch spriteBatch)
        {
            if (dogeSprite.ShowHint)
            {
                if (dogeSprite.Hint != null)
                {
                    spriteBatch.Draw(dogeSprite.Hint.Texture, dogeSprite.Hint.Rectangle, Color.White);
                }
            }
        }
        private void DrawBreadcrumbs(DogeSprite dogeSprite, SpriteBatch spriteBatch)
        {
            if (dogeSprite.ShowBreadCrumbs)
            {
                foreach (var breadCrumb in dogeSprite.BreadCrumbs)
                {
                    if (dogeSprite.X != breadCrumb.X || dogeSprite.Y != breadCrumb.Y)
                    {
                        spriteBatch.Draw(breadCrumb.Texture, breadCrumb.Rectangle, Color.White);
                    }
                }
            }
        }

        private void DrawBestPath(DogeSprite dogeSprite, SpriteBatch spriteBatch)
        {
            if (dogeSprite.ShowBestPath)
            {
                foreach (var item in dogeSprite.BestPath)
                {
                    spriteBatch.Draw(item.Texture, item.Rectangle, Color.White);
                }
            }
        }

        private List<Future> GetBestPath(DogeSprite dogeSprite, Maze maze, ContentManager content, int startX,
            int startY, int xDiff, int yDiff)
        {
            List<Future> futures = new List<Future>();
            List<Tuple<int, int>> tuples = DFS(dogeSprite.X, dogeSprite.Y, maze, new Tuple<int, int>(dogeSprite.X, dogeSprite.Y));
            if (tuples != null)
            {
                foreach (var tuple in tuples)
                {
                    if (dogeSprite.X != tuple.Item1 || dogeSprite.Y != tuple.Item2)
                    {
                        Future toAdd = new Future();
                        toAdd.Rectangle = new Rectangle(startX + tuple.Item1 * xDiff, startY + tuple.Item2 * yDiff,
                            xDiff, yDiff);
                        toAdd.Texture = content.Load<Texture2D>(MazeTextures.Future);
                        futures.Add(toAdd);
                    }
                }
            }

            return futures;
        }

        private List<Tuple<int,int>> DFS(int x, int y, Maze maze, Tuple<int,int> predicesor)
        {
            bool done = false;
            Console.WriteLine("X: " + x + " y: " + y);
            List<Tuple<int, int>> predictions = null;
            
            if (x == maze.Dimensions - 1 && y == maze.Dimensions-1)
            {
                return new List<Tuple<int,int>>();
            }
            if ((predicesor.Item1 != x || predicesor.Item2 != y+1) && !maze.Grid[x][y].Bottom)
            {
                predictions = DFS(x, y+1, maze, new Tuple<int,int>(x,y));
                if (predictions != null)
                {
                    predictions.Add(new Tuple<int, int>(x,y));
                    done = true;
                }
            }

            if ((predicesor.Item1 != x+1 || predicesor.Item2 != y) && !maze.Grid[x][y].Right && !done)
            {
                predictions = DFS(x+1, y, maze, new Tuple<int, int>(x,y));
                if (predictions != null)
                {
                    predictions.Add(new Tuple<int, int>(x,y));
                    done = true;
                }
            }
            
            if ((predicesor.Item1 != x || predicesor.Item2 != y-1) && !maze.Grid[x][y].Top && !done)
            {
                predictions = DFS(x, y-1, maze, new Tuple<int, int>(x,y));
                if (predictions != null)
                {
                    predictions.Add(new Tuple<int, int>(x,y));
                    done = true;
                }

            }
            
            if ((predicesor.Item1 != x-1 || predicesor.Item2 != y) && !maze.Grid[x][y].Left && !done)
            {
                predictions = DFS(x-1, y, maze, new Tuple<int, int>(x,y));
                if (predictions != null)
                {
                    predictions.Add(new Tuple<int, int>(x,y));
                    done = true;
                }
            }
            return predictions;
        }
    }
}