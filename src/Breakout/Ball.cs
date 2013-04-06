using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Ball : Sprite
    {
        private readonly GameObjects _gameObjects;
        private Vector2 _velocity;
        private bool _isAttachedToPaddle;

        public Ball(Texture2D texture, Rectangle screenBounds, GameObjects gameObjects)
            : base(texture, screenBounds)
        {
            _gameObjects = gameObjects;
            _isAttachedToPaddle = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isAttachedToPaddle)
            {
                Position = new Vector2(
                    _gameObjects.Paddle.Position.X + _gameObjects.Paddle.Width / 2f - Width / 2f,
                    _gameObjects.Paddle.Position.Y - Height);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}