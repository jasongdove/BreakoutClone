using System;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.UI
{
    public class Score : GameplayObject
    {
        private readonly Player _player;
        private readonly Rectangle _screenBounds;
        private SpriteFont _font;

        public Score(Player player, Rectangle screenBounds)
        {
            _player = player;
            _screenBounds = screenBounds;
        }

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("GameFont");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            string scoreText = String.Format("{0:n0}", _player.Score);
            Vector2 stringSize = _font.MeasureString(scoreText);

            spriteBatch.DrawString(
                _font,
                scoreText,
                new Vector2(_screenBounds.Width - stringSize.X - 10, _screenBounds.Height - stringSize.Y),
                Color.White);
        }
    }
}