using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Block : GameplayObject
    {
        private readonly Random _random;
        private readonly World _world;
        private readonly Rectangle _screenBounds;
        private readonly int _x;
        private readonly int _y;

        public Block(World world, Rectangle screenBounds, int x, int y)
        {
            _random = new Random(x + y * 1000);

            _world = world;
            _screenBounds = screenBounds;
            _x = x;
            _y = y;

            DieTime = TimeSpan.FromSeconds(1);
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
            }

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Die if needed
            if (_random.Next(1000) == 66 && Status != ObjectStatus.Dying && Status != ObjectStatus.Dead)
            {
                Die();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Die()
        {
            base.Die();

            Body.BodyType = BodyType.Dynamic;
            Body.Mass = 5000;
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