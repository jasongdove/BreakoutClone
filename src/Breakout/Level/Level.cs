using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Level
    {
        private readonly List<Block> _blocks = new List<Block>();
        private readonly Paddle _paddle;
        private readonly Ball _ball;
        private readonly Walls _walls;

        public Level(World world, Rectangle screenBounds, Player player)
        {
            _paddle = new Paddle(world, screenBounds);
            _ball = new Ball(world, _paddle);
            _walls = new Walls(world, screenBounds, _ball, _paddle);

            // TODO: Load level from file/resource
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    _blocks.Add(new Block(world, screenBounds, x, y, player, _ball, _paddle));
                }
            }
        }

        public Paddle Paddle
        {
            get { return _paddle; }
        }

        public Ball Ball
        {
            get { return _ball; }
        }

        public void LoadContent(ContentManager content)
        {
            _paddle.Texture = content.Load<Texture2D>("paddleBlu");
            _paddle.Initialize();

            _ball.Texture = content.Load<Texture2D>("ballGrey");
            _ball.Initialize();

            _walls.Initialize();

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

            _paddle.Update(gameTime);
            _ball.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var block in _blocks)
            {
                block.Draw(gameTime, spriteBatch);
            }

            _paddle.Draw(gameTime, spriteBatch);
            _ball.Draw(gameTime, spriteBatch);
        }
    }
}