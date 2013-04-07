using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Ball : Sprite
    {
        private const float VerticalVelocity = 12f;

        private readonly GameObjects _gameObjects;
        private bool _isAttachedToPaddle;

        public Ball(Texture2D texture, Rectangle screenBounds, World world, GameObjects gameObjects)
            : base(texture, screenBounds)
        {
            _gameObjects = gameObjects;
            _isAttachedToPaddle = true;

            Body = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(Texture.Width) / 2f, 1);
            Body.BodyType = BodyType.Dynamic;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isAttachedToPaddle && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _isAttachedToPaddle = false;

                Body.ApplyLinearImpulse(new Vector2(
                    _gameObjects.Paddle.Body.LinearVelocity.X < 0
                        ? MathHelper.Clamp(_gameObjects.Paddle.Body.LinearVelocity.X, -0.12f, 0)
                        : MathHelper.Clamp(_gameObjects.Paddle.Body.LinearVelocity.X, 0, 0.12f),
                    -ConvertUnits.ToSimUnits(VerticalVelocity)));
            }

            if (_isAttachedToPaddle)
            {
                Body.LinearVelocity = Vector2.Zero;
                Body.Position = new Vector2(
                    ConvertUnits.ToSimUnits(_gameObjects.Paddle.Position.X + _gameObjects.Paddle.Width / 2f),
                    ConvertUnits.ToSimUnits(_gameObjects.Paddle.Position.Y - Height / 2f));
            }
            else
            {
                if (Math.Abs(Body.LinearVelocity.Y - 0f) < float.Epsilon)
                {
                    Body.ApplyLinearImpulse(ConvertUnits.ToSimUnits(new Vector2(0, 1)));
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                new Vector2(Position.X - Width / 2f, Position.Y - Height / 2f),
                Color.White);
        }

        protected override void CheckBounds()
        {
            ////if (Position.X <= 0)
            ////{
            ////    Velocity = new Vector2(-Velocity.X, Velocity.Y);
            ////}

            ////if (Position.X + Width >= ScreenBounds.Width)
            ////{
            ////    Velocity = new Vector2(-Velocity.X, Velocity.Y);
            ////}

            ////if (Position.Y <= 0)
            ////{
            ////    Velocity = new Vector2(Velocity.X, -Velocity.Y);
            ////}
        }
    }
}