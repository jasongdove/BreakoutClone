using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.UI
{
    public class Lives : GameplayObject
    {
        private readonly Player _player;
        private readonly Rectangle _screenBounds;

        public Lives(Player player, Rectangle screenBounds)
        {
            _player = player;
            _screenBounds = screenBounds;
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("paddleBlu");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_player.Lives <= 0)
            {
                return;
            }

            float scale = 0.5f;
            float y = _screenBounds.Height - Texture.Height * scale - 10;
            for (int i = 0; i < _player.Lives; i++)
            {
                spriteBatch.Draw(
                    Texture,
                    new Vector2(10 + i * (Texture.Width * scale + 2), y),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    1f);
            }
        }
    }
}