using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.UI
{
    public class UserInterface
    {
        private readonly Session _session;

        public UserInterface(Session session)
        {
            _session = session;
            _session.SetUI(this);
        }

        // TODO: Add UI Elements here

        public void UpdateUI(GameTime gameTime)
        {
            // TODO: Update UI elements here
        }

        public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Draw UI elements here

            _session.Level.Draw(gameTime, spriteBatch);
        }
    }
}