using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Paddle : Sprite
    {
        private const int HorizontalVelocity = 6;

        public Paddle(Texture2D texture, Rectangle screenBounds)
            : base(texture, screenBounds)
        {
            SetStartingPosition();
        }

        private void SetStartingPosition()
        {
            Position = new Vector2(
                ScreenBounds.Width / 2f - Width / 2f,
                ScreenBounds.Height - 50);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X - HorizontalVelocity, Position.Y);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X + HorizontalVelocity, Position.Y);
            }

            Position = new Vector2(
                MathHelper.Clamp(Position.X, 0, ScreenBounds.Width - Width),
                Position.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}