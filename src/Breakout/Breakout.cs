using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Breakout : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameObjects _gameObjects;
        private World _world;

        public Breakout()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 720,
                PreferredBackBufferHeight = 720,
            };

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            if (_world == null)
            {
                _world = new World(Vector2.Zero);
            }
            else
            {
                _world.Clear();
            }

            var screenBounds = Window.ClientBounds;

            _gameObjects = new GameObjects();

            _gameObjects.Paddle = new Paddle(
                Content.Load<Texture2D>("paddleBlu"),
                screenBounds,
                _world);
            
            _gameObjects.Ball = new Ball(
                Content.Load<Texture2D>("ballGrey"),
                screenBounds,
                _world,
                _gameObjects);

            _gameObjects.Walls = new Walls(_world, screenBounds, _gameObjects);

            // paddle shouldn't bounce off of walls
            _gameObjects.Paddle.Body.IgnoreCollisionWith(_gameObjects.Walls.Body);
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

            _world.Step(MathHelper.Min((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f, 1/33f));

            _gameObjects.Paddle.Update(gameTime);
            _gameObjects.Ball.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(70, 92, 141));

            _spriteBatch.Begin();

            _gameObjects.Paddle.Draw(_spriteBatch);
            _gameObjects.Ball.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
