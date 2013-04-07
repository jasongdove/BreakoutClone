using FarseerPhysics.Dynamics;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public abstract class Sprite : IGameObject
    {
        private readonly Texture2D _texture;
        private readonly Rectangle _screenBounds;

        protected Sprite(Texture2D texture, Rectangle screenBounds)
        {
            _texture = texture;
            _screenBounds = screenBounds;
        }

        public Vector2 Position
        {
            get { return ConvertUnits.ToDisplayUnits(Body.Position); }
        }

        public Vector2 Velocity
        {
            get { return Body.LinearVelocity; }
        }

        public int Width
        {
            get { return _texture.Width; }
        }

        public int Height
        {
            get { return _texture.Height; }
        }

        public Body Body { get; protected set; }

        protected Rectangle ScreenBounds
        {
            get { return _screenBounds; }
        }

        protected Texture2D Texture
        {
            get { return _texture; }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}