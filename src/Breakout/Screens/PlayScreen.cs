using Breakout.UI;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Screens
{
    public class PlayScreen : GameScreen
    {
        private readonly Session _session;
        private UserInterface _ui;

        public PlayScreen(Session session)
        {
            _session = session;
        }

        public override bool AcceptsInput
        {
            get { return true; }
        }

        public override void InitializeScreen()
        {
            _ui = new UserInterface(_session);

            InputMap.NewAction("MoveLeft", Keys.Left);
            InputMap.NewAction("MoveRight", Keys.Right);
        }

        public override void LoadContent()
        {
            _session.Level.LoadContent(ScreenManager.Content);
        }

        protected override void DrawScreen(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            _ui.DrawUI(gameTime, spriteBatch);
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            if (_session.IsPaused)
            {
                FreezeScreen();
                ScreenManager.AddScreen(new PauseScreen(this, _session));
            }

            _session.Level.Paddle.StopMoving();
            if (InputMap.ActionPressed("MoveLeft"))
            {
                _session.Level.Paddle.MoveLeft();
            }
            else if (InputMap.ActionPressed("MoveRight"))
            {
                _session.Level.Paddle.MoveRight();
            }

            // TODO: Support pausing
            ////if (InputMap.NewActionPress("Pause"))
            ////{
            ////    _session.Pause();
            ////}

            _session.Update(gameTime);
        }
    }
}