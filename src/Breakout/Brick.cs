using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Brick : Sprite
    {
        private readonly World _world;
        private readonly GameObjects _gameObjects;
        private bool _isActive;

        public Brick(Texture2D texture, Rectangle screenBounds, World world, GameObjects gameObjects, int x, int y)
            : base(texture, screenBounds)
        {
            _world = world;
            _gameObjects = gameObjects;
            Body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(Texture.Width), ConvertUnits.ToSimUnits(Texture.Height), 1);
            Body.BodyType = BodyType.Static;
            Body.Restitution = 1;

            // brick grid is 10 wide
            Body.Position = ConvertUnits.ToSimUnits((screenBounds.Width - Texture.Width * 10) + Texture.Width * x, 100 + Texture.Height * y);

            Body.OnCollision += Body_OnCollision;

            _isActive = true;
        }

        private bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureB.Body == _gameObjects.Ball.Body)
            {
                _world.RemoveBody(Body);
                _isActive = false;
            }

            return true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                spriteBatch.Draw(
                    Texture,
                    new Vector2(Position.X - Width / 2f, Position.Y - Height / 2f),
                    Color.White);
            }
        }

        protected override void CheckBounds()
        {
        }
    }
}