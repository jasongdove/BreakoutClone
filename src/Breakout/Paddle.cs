using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Paddle : IGameObject
    {
        private const int HorizontalVelocity = 6;
        private readonly Texture2D _texture;
        private readonly Rectangle _screenBounds;
        private Vector2 _position;

        public Paddle(Texture2D texture, Rectangle screenBounds)
        {
            _texture = texture;
            _screenBounds = screenBounds;
            SetStartingPosition();
        }

        private void SetStartingPosition()
        {
            _position = new Vector2(
                _screenBounds.Width / 2f - _texture.Width / 2f,
                _screenBounds.Height - 50);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _position.X -= HorizontalVelocity;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _position.X += HorizontalVelocity;
            }

            _position.X = MathHelper.Clamp(_position.X, 0, _screenBounds.Width - _texture.Width);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}