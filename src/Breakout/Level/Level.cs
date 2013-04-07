﻿using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Level
    {
        private readonly List<Block> _blocks = new List<Block>();
        private Texture2D _background;

        public Level(World world, Rectangle screenBounds)
        {
            // TODO: Load level from file/resource
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    _blocks.Add(new Block(world, screenBounds, x, y));
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("bg5");

            var texture = content.Load<Texture2D>("element_blue_rectangle");
            foreach (var block in _blocks)
            {
                block.Texture = texture;
                block.Initialize();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var block in _blocks)
            {
                block.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);

            foreach (var block in _blocks)
            {
                block.Draw(gameTime, spriteBatch);
            }
        }
    }
}