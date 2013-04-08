using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;

namespace Breakout
{
    public class Block : GameplayObject
    {
        private readonly World _world;
        private readonly Rectangle _screenBounds;
        private readonly int _x;
        private readonly int _y;
        private readonly Ball _ball;
        private readonly Paddle _paddle;
        private readonly Player _player;

        public Block(World world, Rectangle screenBounds, int x, int y, Player player, Ball ball, Paddle paddle)
        {
            _world = world;
            _screenBounds = screenBounds;
            _x = x;
            _y = y;
            _player = player;
            _ball = ball;
            _paddle = paddle;

            DieTime = TimeSpan.FromSeconds(0.75);
        }

        public override void Initialize()
        {
            if (_world != null && Texture != null)
            {
                var position = new Vector2(
                    ConvertUnits.ToSimUnits((_screenBounds.Width - Texture.Width * 10) / 2f + _x * Texture.Width + Texture.Width / 2f),
                    ConvertUnits.ToSimUnits(50 + Texture.Width * _y / 2f));

                Body = BodyFactory.CreateRectangle(
                    _world,
                    ConvertUnits.ToSimUnits(Texture.Width),
                    ConvertUnits.ToSimUnits(Texture.Height),
                    1,
                    position);

                Body.BodyType = BodyType.Static;
                Body.Restitution = 1;
                Body.UserData = String.Format("Brick ({0},{1})", _x, _y);
                Body.OnCollision += OnCollision;
            }

            base.Initialize();
        }

        private bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            if (fixtureB.Body == _ball.Body)
            {
                Die();
                _player.Score += 100;
            }

            return true;
        }

        public override void Die()
        {
            base.Die();

            Body.BodyType = BodyType.Dynamic;
            Body.Mass = 5000;
            Body.IgnoreCollisionWith(_ball.Body);
            Body.IgnoreCollisionWith(_paddle.Body);
        }

        public override void Dying(GameTime gameTime)
        {
            base.Dying(gameTime);

            Alpha = 1f - DiePercent;
        }

        public override void Dead(GameTime gameTime)
        {
            if (Body != null)
            {
                _world.RemoveBody(Body);
                Body = null;
            }
        }
    }
}