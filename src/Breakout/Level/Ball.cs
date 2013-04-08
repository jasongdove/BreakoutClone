using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Ball : GameplayObject
    {
        private const float VerticalVelocity = 12f;
        
        private readonly World _world;
        private readonly Paddle _paddle;
        private readonly Player _player;
        private bool _isAttachedToPaddle;
        private float _speedLimit;
        private DateTime _paddleCollisionTime;

        private SoundEffect _bounce;

        public Ball(World world, Paddle paddle, Player player)
        {
            _world = world;
            _paddle = paddle;
            _player = player;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("ballGrey");
            _bounce = content.Load<SoundEffect>("001");

            base.LoadContent(content);
        }

        public override void Initialize()
        {
            if (_world != null && Texture != null)
            {
                Body = BodyFactory.CreateCircle(_world, ConvertUnits.ToSimUnits(Texture.Width) / 2f, 1);
                Body.BodyType = BodyType.Dynamic;
                Body.IgnoreGravity = true;

                Body.OnCollision += OnCollision;
            }

            Reset();

            base.Initialize();
        }

        private bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            if (!_isAttachedToPaddle)
            {
                if (fixtureB.Body == _paddle.Body)
                {
                    _player.ResetStreak();

                    if (DateTime.Now < _paddleCollisionTime)
                    {
                        return true;
                    }

                    _paddleCollisionTime = DateTime.Now.AddMilliseconds(200);
                }

                // Don't play the bounce sound if the player is going to score
                // That will get handled elsewhere
                if (!String.Equals(fixtureB.Body.UserData as string, "BLOCK"))
                {
                    _bounce.Play(0.75f, 0, 0);
                }
            }

            return true;
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