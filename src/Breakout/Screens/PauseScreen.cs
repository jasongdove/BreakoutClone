using GameEngine;
using Microsoft.Xna.Framework;

namespace Breakout.Screens
{
    public class PauseScreen : GameScreen
    {
        private readonly GameScreen _before;
        private readonly Session _session;

        public PauseScreen(GameScreen before, Session session)
        {
            _before = before;
            _session = session;
        }

        public override bool AcceptsInput
        {
            get { return true; }
        }

        public override void InitializeScreen()
        {
        }

        protected override void DrawScreen(GameTime gameTime)
        {
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
        }
    }
}