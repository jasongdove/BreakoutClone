using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.UI
{
    public class UserInterface
    {
        private readonly Session _session;
        private readonly Lives _lives;
        private readonly Score _score;
        private Texture2D _background;

        public UserInterface(Session session)
        {
            _session = session;
            _session.SetUI(this);

            _lives = new Lives(_session.Player, _session.ScreenBounds);
            _score = new Score(_session.Player, _session.ScreenBounds);
        }

        // TODO: Add UI Elements here

        public void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("bg5");

            _session.Level.LoadContent(content);
            _lives.LoadContent(content);
            _score.LoadContent(content);
        }

        public void UpdateUI(GameTime gameTime)
        {
            // TODO: Update UI elements here
            _lives.Update(gameTime);
            _score.Update(gameTime);
        }

        public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Draw UI elements here
            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);

            _lives.Draw(gameTime, spriteBatch);
            _score.Draw(gameTime, spriteBatch);

            _session.Level.Draw(gameTime, spriteBatch);
        }
    }
}