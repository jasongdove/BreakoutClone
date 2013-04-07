using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;

namespace Breakout
{
    public class Walls
    {
        public Walls(World world, Rectangle screenBounds, GameObjects gameObjects)
        {
            var borders = new List<Vertices>();
            Bottom = PolygonTools.CreateRectangle(
                ConvertUnits.ToSimUnits(screenBounds.Width),
                0.01f,
                new Vector2(ConvertUnits.ToSimUnits(screenBounds.Width / 2f), ConvertUnits.ToSimUnits(screenBounds.Height + gameObjects.Ball.Height)),
                0);
            var left = PolygonTools.CreateRectangle(
                0.01f,
                ConvertUnits.ToSimUnits(screenBounds.Height),
                new Vector2(0, ConvertUnits.ToSimUnits(screenBounds.Height / 2f)),
                0);
            var top = PolygonTools.CreateRectangle(
                ConvertUnits.ToSimUnits(screenBounds.Width),
                0.01f,
                new Vector2(ConvertUnits.ToSimUnits(screenBounds.Width / 2f), 0),
                0);
            var right = PolygonTools.CreateRectangle(
                0.01f,
                ConvertUnits.ToSimUnits(screenBounds.Height),
                new Vector2(ConvertUnits.ToSimUnits(screenBounds.Width), ConvertUnits.ToSimUnits(screenBounds.Height / 2f)),
                0);
            borders.AddRange(new[] { Bottom, left, top, right });
            Body = BodyFactory.CreateCompoundPolygon(world, borders, 1, 1);
            foreach (var fixture in Body.FixtureList)
            {
                fixture.Restitution = 1;
                fixture.Friction = 0;
            }
        }

        public Body Body { get; private set; }
        public Vertices Bottom { get; private set; }
    }
}