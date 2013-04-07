using System.Collections.Generic;

namespace Breakout
{
    public class GameObjects
    {
        public OldPaddle OldPaddle { get; set; }
        public Ball Ball { get; set; }
        public Walls Walls { get; set; }
        public List<Brick> Bricks { get; set; }
    }
}