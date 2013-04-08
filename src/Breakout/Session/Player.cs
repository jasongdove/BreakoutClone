using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    public class Player
    {
        private SoundEffect _ding;

        private int _streak;

        public int Score { get; private set; }
        public int Lives { get; set; }

        public void LoadContent(ContentManager content)
        {
            _ding = content.Load<SoundEffect>("002");
        }

        public void ResetStreak()
        {
            _streak = 0;
        }

        public void IncrementScore()
        {
            Score += 100 + (_streak * 25);
            _ding.Play();//1f, 0.0f + _streak * 0.05f, 0);

            _streak++;
        }
    }
}