using GameEngine;
using Microsoft.Xna.Framework;

namespace Breakout.UI
{
    public class MainMenuEntry : MenuEntry
    {
        public MainMenuEntry(MenuScreen menu, string title, string description)
            : base(menu, title)
        {
            EntryDescription = description;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void AnimateHighlighted(GameTime gameTime)
        {
        }
    }
}