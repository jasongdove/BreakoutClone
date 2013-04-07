using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
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
        private float _speedLimit;

        public Ball(Texture2D texture, Rectangle screenBounds, World world, GameObjects gameObjects)
            : base(texture, screenBounds)
        {
            _gameObjects = gameObjects;
            _isAttachedToPaddle = true;

            Body = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(Texture.Width) / 2f, 1);
            Body.BodyType = BodyType.Dynamic;
            Body.IgnoreGravity = true;
            Body.OnCollision += Body_OnCollision;

            _speedLimit = 25f;
        }

        private bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            var bottomCentroid = _gameObjects.Walls.Bottom.GetCentroid();
            if (fixtureB.TestPoint(ref bottomCentroid))
            {
                // TODO: Lose a life
                _isAttachedToPaddle = true;
                _gameObjects.Paddle.SetStartPosition();
            }

            return true;
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
                if (Math.Abs(Body.LinearVelocity.Y) < 4f)
                {
                    Body.ApplyLinearImpulse(Math.Sign(Body.LinearVelocity.Y) * ConvertUnits.ToSimUnits(new Vector2(0, 4)));
                }

                if (Math.Abs(Body.LinearVelocity.X) < 1f)
                {
                    Body.ApplyLinearImpulse(Math.Sign(Body.LinearVelocity.X) * ConvertUnits.ToSimUnits(new Vector2(1, 0)));
                }

                Body.LinearVelocity = new Vector2(
                    MathHelper.Clamp(Body.LinearVelocity.X, -_speedLimit, _speedLimit),
                    MathHelper.Clamp(Body.LinearVelocity.Y, -_speedLimit, _speedLimit));
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
    }
}