using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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
        private DebugViewXNA _debug;

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

            // walls
            var borders = new List<Vertices>();
            var bottom = PolygonTools.CreateRectangle(
                ConvertUnits.ToSimUnits(screenBounds.Width),
                0.01f,
                new Vector2(ConvertUnits.ToSimUnits(screenBounds.Width / 2f), ConvertUnits.ToSimUnits(screenBounds.Height)),
                0);
            var left = PolygonTools.CreateRectangle(
                0.01f,
                ConvertUnits.ToSimUnits(screenBounds.Height),
                new Vector2(0, ConvertUnits.ToSimUnits(screenBounds.Height / 2f)),
                0);
            var top = PolygonTools.CreateRectangle(
                ConvertUnits.ToSimUnits(screenBounds.Width),
                0.01f,
                new Vector2(ConvertUnits.ToSimUnits(screenBounds.Width / 2f), 0),
                0);
            var right = PolygonTools.CreateRectangle(
                0.01f,
                ConvertUnits.ToSimUnits(screenBounds.Height),
                new Vector2(ConvertUnits.ToSimUnits(screenBounds.Width), ConvertUnits.ToSimUnits(screenBounds.Height / 2f)),
                0);
            borders.AddRange(new[] { bottom, left, top, right });
            var body = BodyFactory.CreateCompoundPolygon(_world, borders, 1, 1);
            foreach (var fixture in body.FixtureList)
            {
                fixture.Restitution = 1;
                fixture.Friction = 0;
            }

            // paddle shouldn't bounce off of walls
            _gameObjects.Paddle.Body.IgnoreCollisionWith(body);

            _debug = new DebugViewXNA(_world);
            _debug.LoadContent(GraphicsDevice, Content);
            _debug.AppendFlags(FarseerPhysics.DebugViewFlags.Shape);
            _debug.AppendFlags(FarseerPhysics.DebugViewFlags.PolygonPoints);
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

            // Debug
            ////Matrix proj = Matrix.CreateOrthographic(
            ////    ConvertUnits.ToSimUnits(Window.ClientBounds.Width),
            ////    -ConvertUnits.ToSimUnits(Window.ClientBounds.Height),
            ////    0,
            ////    1000000);
            ////Vector3 campos = new Vector3();
            ////campos.X = ConvertUnits.ToSimUnits(-_graphics.PreferredBackBufferWidth / 2f);
            ////campos.Y = ConvertUnits.ToSimUnits(-_graphics.PreferredBackBufferHeight / 2f);
            ////campos.Z = 0;
            ////Matrix tran = Matrix.Identity;
            ////tran.Translation = campos;
            ////Matrix view = tran;

            ////_debug.RenderDebugData(ref proj, ref view);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
