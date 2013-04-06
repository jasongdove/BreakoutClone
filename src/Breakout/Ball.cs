using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Ball : Sprite
    {
        private const float VerticalVelocity = 5;

        private readonly GameObjects _gameObjects;
        private bool _isAttachedToPaddle;

        public Ball(Texture2D texture, Rectangle screenBounds, GameObjects gameObjects)
            : base(texture, screenBounds)
        {
            _gameObjects = gameObjects;
            _isAttachedToPaddle = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isAttachedToPaddle && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _isAttachedToPaddle = false;
                
                Velocity = new Vector2(
                    _gameObjects.Paddle.Velocity.X,
                    -VerticalVelocity);
            }

            if (_isAttachedToPaddle)
            {
                Velocity = Vector2.Zero;

                Position = new Vector2(
                    _gameObjects.Paddle.Position.X + _gameObjects.Paddle.Width / 2f - Width / 2f,
                    _gameObjects.Paddle.Position.Y - Height);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        protected override void CheckBounds()
        {
            if (Position.X <= 0)
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }

            if (Position.X + Width >= ScreenBounds.Width)
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }

            if (Position.Y <= 0)
            {
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
        }
    }
}