using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;

namespace Breakout
{
    public class Ball : GameplayObject
    {
        private const float VerticalVelocity = 12f;
        
        private readonly World _world;
        private readonly Paddle _paddle;
        private bool _isAttachedToPaddle;
        private float _speedLimit;

        public Ball(World world, Paddle paddle)
        {
            _world = world;
            _paddle = paddle;
        }

        public override void Initialize()
        {
            if (_world != null && Texture != null)
            {
                Body = BodyFactory.CreateCircle(_world, ConvertUnits.ToSimUnits(Texture.Width) / 2f, 1);
                Body.BodyType = BodyType.Dynamic;
                Body.IgnoreGravity = true;
            }

            Reset();

            base.Initialize();
        }

        public void Reset()
        {
            _isAttachedToPaddle = true;
            _speedLimit = 25f;
        }

        public void Fire()
        {
            if (_isAttachedToPaddle)
            {
                _isAttachedToPaddle = false;

                Body.ApplyLinearImpulse(new Vector2(
                    _paddle.Body.LinearVelocity.X < 0
                        ? MathHelper.Clamp(_paddle.Body.LinearVelocity.X, -0.12f, 0)
                        : MathHelper.Clamp(_paddle.Body.LinearVelocity.X, 0, 0.12f),
                    -ConvertUnits.ToSimUnits(VerticalVelocity)));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_isAttachedToPaddle)
            {
                Body.LinearVelocity = Vector2.Zero;
                Body.Position = new Vector2(
                    ConvertUnits.ToSimUnits(_paddle.Position.X + _paddle.Texture.Width / 2f),
                    ConvertUnits.ToSimUnits(_paddle.Position.Y - Texture.Height / 2f));
            }
            else
            {
                if (Math.Abs(Body.LinearVelocity.Y) < 4f)
                {
                    Body.ApplyLinearImpulse(Math.Sign(Body.LinearVelocity.Y) * ConvertUnits.ToSimUnits(new Vector2(0, 4)));
                }

                ////if (Math.Abs(Body.LinearVelocity.X) < 1f)
                ////{
                ////    Body.ApplyLinearImpulse(Math.Sign(Body.LinearVelocity.X) * ConvertUnits.ToSimUnits(new Vector2(1, 0)));
                ////}

                Body.LinearVelocity = new Vector2(
                    MathHelper.Clamp(Body.LinearVelocity.X, -_speedLimit, _speedLimit),
                    MathHelper.Clamp(Body.LinearVelocity.Y, -_speedLimit, _speedLimit));
            }

            base.Update(gameTime);
        }
    }
}