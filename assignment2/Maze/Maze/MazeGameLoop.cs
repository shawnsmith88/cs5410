using System;
using System.Collections.Generic;
using System.Data;
using Maze.InputTypes;
using Maze.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maze
{
    public class MazeGameLoop : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Maze _maze;
        private IGenerationAlgorithm _generationAlgorithm;
        private State _state;
        private Texture2D _texture;
        private MazeService _mazeService;
        private List<Maze> _mazes;
        private List<List<GameObject>> _gameObjects;
        private IInputService _inputService;
        private int _targetMazeIndex;
        private ISpriteService _spriteService;
        private ISpriteService _finishLineService;
        private Direction _nextDirection;
        private GameObject _character;
        private InGameOptions _inGameOptions;

        public MazeGameLoop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _generationAlgorithm = new PrimGenerationAlgorithm();
            _mazeService = new MazeService();
            _spriteService = new DogeService();
            _finishLineService = new MoonRocketSpriteService();
            _gameObjects = new List<List<GameObject>>();
            _mazes = new List<Maze>();
            for (int i = 0; i < 4; i++)
            {
                _mazes.Add(_generationAlgorithm.GenerateMaze(5*i+5));
            }
            
            _inputService = new KeyboardInputService();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < _mazes.Count; i++)
            {
                _gameObjects.Add(new List<GameObject>());
                _mazeService.render(_mazes[i],_graphics,this.Content, _gameObjects[i]);
                _character = _spriteService.renderSprite(_graphics, _mazes[i], this.Content);
                _gameObjects[i].Add(_finishLineService.renderSprite(_graphics, _mazes[i], this.Content));
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            processInput();
            if (_state == State.InGame)
            {
                _spriteService.HandleInGameOptions(_character,_inGameOptions);
                _character = _spriteService.move(_graphics,_mazes[_targetMazeIndex],_character, _nextDirection, this.Content);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();

            // TODO: Add your drawing code here
            if (_state == State.Menu)
            {
                //Draw message for help screen
            }

            if (_state == State.InGame)
            {
                //Draw game
                DrawMaze();
            }

            if (_state == State.HighScores)
            {
                //Draw message for high scores
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void processInput()
        {
            if (_state == State.Menu)
            {
                MenuOptions menuInput = _inputService.getMenuInput();
                switch (menuInput)
                {
                    case MenuOptions.Credits:
                        break;
                    case MenuOptions.HighScores:
                        break;
                    case MenuOptions.None:
                        break;
                    default:
                        Init_Maze(menuInput);
                        break;
                }
            }

            if (_state == State.InGame)
            {
                _inGameOptions = _inputService.getInGameOptions();
                _nextDirection = _inputService.getInputDirection();
            }
        }

        private void DrawMaze()
        {
            //draw maze
            
            _mazeService.Draw(_gameObjects[_targetMazeIndex], _spriteBatch);
            _spriteService.Draw(_character, _spriteBatch);
        }
        
        private void Init_Maze(MenuOptions menuOptions)
        {
            switch (menuOptions)
            {
                case MenuOptions.StartGame5x5:
                    _targetMazeIndex = 0;
                    break;
                case MenuOptions.StartGame10x10:
                    _targetMazeIndex = 1;
                    break;
                case MenuOptions.StartGame15x15:
                    _targetMazeIndex = 2;
                    break;
                case MenuOptions.StartGame20x20:
                    _targetMazeIndex = 3;
                    break;
            }
            // _maze = _generationAlgorithm.GenerateMaze(dimensions);
            _state = State.InGame;
        }

    }
}
