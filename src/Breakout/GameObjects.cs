using System.Collections.Generic;

namespace Breakout
{
    public class GameObjects
    {
        public Paddle Paddle { get; set; }
        public Ball Ball { get; set; }
        public Walls Walls { get; set; }
        public List<Brick> Bricks { get; set; }
    }
}