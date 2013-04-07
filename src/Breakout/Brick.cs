using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Brick : Sprite
    {
        private readonly World _world;
        private readonly GameObjects _gameObjects;
        private bool _isActive;
        private bool _isFalling;
        private float _alpha;

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
            Body.IgnoreCollisionWith(gameObjects.Paddle.Body);

            _isActive = true;
            _alpha = 1.0f;
            _isFalling = false;
        }

        private bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureB.Body == _gameObjects.Ball.Body)
            {
                Body.BodyType = BodyType.Dynamic;
                Body.Mass = 5000;
                Body.IgnoreCollisionWith(_gameObjects.Ball.Body);
                _isFalling = true;
            }

            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isFalling)
            {
                _alpha -= 1 / 45f;
                if (_alpha <= 0)
                {
                    _world.RemoveBody(Body);
                    _isActive = false;
                    _isFalling = false;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                spriteBatch.Draw(
                    Texture,
                    new Vector2(Position.X - Width / 2f, Position.Y - Height / 2f),
                    Color.White * _alpha);
            }
        }
    }
}