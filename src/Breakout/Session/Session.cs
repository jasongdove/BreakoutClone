using Breakout.UI;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    public class Session
    {
        private readonly Rectangle _screenBounds;
        private readonly Level _level;
        private readonly Player _player;
        private readonly World _world;

        private UserInterface _ui;

        public Session(Rectangle screenBounds) // TODO: Get level id
        {
            _screenBounds = screenBounds;
            _world = new World(new Vector2(0, 10));

            _player = new Player();
            _player.Lives = 3;

            _level = new Level(_world, screenBounds, _player);
        }

        public void LoadContent(ContentManager content)
        {
            _level.LoadContent(content);
            _player.LoadContent(content);
        }

        public bool IsPaused { get; private set; }

        public Player Player
        {
            get { return _player; }
        }

        public World World
        {
            get { return _world; }
        }

        public Level Level
        {
            get { return _level; }
        }

        public Rectangle ScreenBounds
        {
            get { return _screenBounds; }
        }

        public void SetUI(UserInterface ui)
        {
            _ui = ui;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsPaused)
            {
                _world.Step(MathHelper.Min((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f, 1 / 33f));

                _ui.UpdateUI(gameTime);

                _level.Update(gameTime);

                if (_level.Ball.Position.Y > _screenBounds.Height)
                {
                    _level.Ball.Reset();
                    _level.Paddle.SetStartPosition();
                    _player.Lives--;
                }

                // TODO: If round.state == finished, new round ??
            }
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }
    }
}