using System;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Screens
{
    public class PauseScreen : MenuScreen
    {
        private const string MenuCancelActionName = "MenuCancel";

        private readonly GameScreen _before;
        private readonly Session _session;

        public PauseScreen(GameScreen before, Session session)
        {
            _before = before;
            _session = session;

            TransitionOnTime = TimeSpan.FromSeconds(1);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            Removing += PauseScreenRemoving;
        }

        public override bool AcceptsInput
        {
            get { return true; }
        }

        public override void InitializeScreen()
        {
            InputMap.NewAction(MenuCancelActionName, Keys.Escape);

            EnableFade(Color.Black, 0.8f);
        }

        public override void HandleInput()
        {
            if (InputMap.NewActionPress(MenuCancelActionName))
            {
                MenuCancel();
            }
        }

        protected override void DrawScreen(GameTime gameTime)
        {
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
        }

        public void PauseScreenRemoving(object sender, EventArgs e)
        {
            _before.ActivateScreen();
            _session.Resume();
        }
    }
}