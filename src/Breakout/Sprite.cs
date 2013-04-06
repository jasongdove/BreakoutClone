﻿using Microsoft.Xna.Framework;
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

        public Vector2 Position { get; protected set; }

        public int Width
        {
            get { return _texture.Width; }
        }

        public int Height
        {
            get { return _texture.Height; }
        }

        protected Rectangle ScreenBounds
        {
            get { return _screenBounds; }
        }

        protected Texture2D Texture
        {
            get { return _texture; }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}