using Breakout.Screens;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Breakout : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        private Color _clearColor;

        public Breakout()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 720;
            _graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            var screenBounds = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            var session = new Session(screenBounds);
            _screenManager.AddScreen(new PlayScreen(session));
            //_screenManager.AddScreen(new DebugScreen(session.World, session.ScreenBounds));
            _clearColor = Color.Black;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ////var screenBounds = Window.ClientBounds;

            ////_gameObjects = new GameObjects();

            ////_gameObjects.Paddle = new Paddle(
            ////    Content.Load<Texture2D>("paddleBlu"),
            ////    screenBounds,
            ////    _world);
            
            ////_gameObjects.Ball = new Ball(
            ////    Content.Load<Texture2D>("ballGrey"),
            ////    screenBounds,
            ////    _world,
            ////    _gameObjects);

            ////_gameObjects.Walls = new Walls(_world, screenBounds, _gameObjects);

            ////// paddle shouldn't bounce off of walls
            ////_gameObjects.Paddle.Body.IgnoreCollisionWith(_gameObjects.Walls.Body);

            ////_gameObjects.Bricks = new List<Brick>();
            ////var brickTexture = Content.Load<Texture2D>("element_blue_rectangle");
            ////for (int y = 0; y < 4; y++)
            ////{
            ////    for (int x = 0; x < 10; x++)
            ////    {
            ////        var brick = new Brick(brickTexture, screenBounds, _world, _gameObjects, x, y);
            ////        _gameObjects.Bricks.Add(brick);
            ////    }
            ////}

            ////_background = Content.Load<Texture2D>("bg5");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_clearColor);

            base.Draw(gameTime);
        }
    }
}
