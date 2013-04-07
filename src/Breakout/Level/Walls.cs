using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;

namespace Breakout
{
    public class Walls : GameplayObject
    {
        private readonly World _world;
        private readonly Rectangle _screenBounds;
        private readonly Ball _ball;
        private readonly Paddle _paddle;

        public Walls(World world, Rectangle screenBounds, Ball ball, Paddle paddle)
        {
            _world = world;
            _screenBounds = screenBounds;
            _ball = ball;
            _paddle = paddle;
        }

        public override void Initialize()
        {
            if (_world != null)
            {
                var borders = new List<Vertices>();
                var bottom = PolygonTools.CreateRectangle(
                    ConvertUnits.ToSimUnits(_screenBounds.Width),
                    0.01f,
                    new Vector2(ConvertUnits.ToSimUnits(_screenBounds.Width / 2f), ConvertUnits.ToSimUnits(_screenBounds.Height + _ball.Texture.Height)),
                    0);
                var left = PolygonTools.CreateRectangle(
                    0.01f,
                    ConvertUnits.ToSimUnits(_screenBounds.Height),
                    new Vector2(0, ConvertUnits.ToSimUnits(_screenBounds.Height / 2f)),
                    0);
                var top = PolygonTools.CreateRectangle(
                    ConvertUnits.ToSimUnits(_screenBounds.Width),
                    0.01f,
                    new Vector2(ConvertUnits.ToSimUnits(_screenBounds.Width / 2f), 0),
                    0);
                var right = PolygonTools.CreateRectangle(
                    0.01f,
                    ConvertUnits.ToSimUnits(_screenBounds.Height),
                    new Vector2(ConvertUnits.ToSimUnits(_screenBounds.Width), ConvertUnits.ToSimUnits(_screenBounds.Height / 2f)),
                    0);
                borders.AddRange(new[] { bottom, left, top, right });
                Body = BodyFactory.CreateCompoundPolygon(_world, borders, 1, 1);
                foreach (var fixture in Body.FixtureList)
                {
                    fixture.Restitution = 1;
                    fixture.Friction = 0;
                }

                // Paddle should not bounce off walls
                _paddle.Body.IgnoreCollisionWith(Body);
            }

            base.Initialize();
        }
    }
}