using Breakout.UI;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

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

            _level = new Level(_world, screenBounds);

            _player = new Player();
            _player.Lives = 5;
        }

        public bool IsPaused { get; private set; }

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

                // TODO: Update paddle, ball, etc
                _level.Update(gameTime);

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