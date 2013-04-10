using System;
using Breakout.UI;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Screens
{
    public class PauseScreen : MenuScreen
    {
        private readonly GameScreen _before;
        private readonly Session _session;
        private readonly string _prevEntry;
        private readonly string _nextEntry;
        private readonly string _selectedEntry;
        private readonly string _cancelMenu;
        private MainMenuEntry _resume;
        private MainMenuEntry _quit;

        public PauseScreen(GameScreen before, Session session)
        {
            _before = before;
            _session = session;

            _prevEntry = "MenuUp";
            _nextEntry = "MenuDown";
            _selectedEntry = "MenuAccept";
            _cancelMenu = "MenuCancel";

            TransitionOnTime = TimeSpan.FromSeconds(0.7);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            Selected = Highlighted = new Color(214, 232, 223);
            Normal = new Color(104, 173, 178);
        }

        public override string PreviousEntryActionName
        {
            get { return _prevEntry; }
        }

        public override string NextEntryActionName
        {
            get { return _nextEntry; }
        }

        public override string SelectedEntryActionName
        {
            get { return _selectedEntry; }
        }

        public override string MenuCancelActionName
        {
            get { return _cancelMenu; }
        }

        public override void InitializeScreen()
        {
            InputMap.NewAction(PreviousEntryActionName, Keys.Up);
            InputMap.NewAction(NextEntryActionName, Keys.Down);
            InputMap.NewAction(SelectedEntryActionName, Keys.Enter);
            InputMap.NewAction(SelectedEntryActionName, MousePresses.LeftMouse);
            InputMap.NewAction(MenuCancelActionName, Keys.Escape);

            _resume = new MainMenuEntry(this, "Resume", "CONTINUE PLAYING THE GAME") { Opacity = 0 };
            _quit = new MainMenuEntry(this, "Quit", "DONE PLAYING FOR NOW?") { Opacity = 0 };

            Removing += PauseScreen_Removing;
            Entering += PauseScreen_Entering;
            Exiting += PauseScreen_Exiting;

            _resume.Selected += ResumeSelect;
            _quit.Selected += QuitSelect;

            MenuEntries.Add(_resume);
            MenuEntries.Add(_quit);

            Viewport view = ScreenManager.Viewport;
            SetDescriptionArea(
                new Rectangle(100, view.Height - 100, view.Width - 100, 50),
                Color.Black,
                new Color(29, 108, 117),
                new Point(10, 0),
                0.5f);

            EnableFade(Color.Black, 0.5f);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Content;
            SpriteFont = content.Load<SpriteFont>("GameFont");

            InitialTitlePosition = TitlePosition = new Vector2(ScreenManager.Viewport.Width / 2f, 50);

            for (int i = 0; i < MenuEntries.Count; i++)
            {
                MenuEntries[i].AddPadding(14, 0);

                if (i == 0)
                {
                    MenuEntries[i].SetPosition(new Vector2(180, 250), true);
                }
                else
                {
                    int offsetY = MenuEntries[i - 1].EntryTexture == null ? SpriteFont.LineSpacing : 8;
                    MenuEntries[i].SetRelativePosition(new Vector2(0, offsetY), MenuEntries[i - 1], true);
                }
            }
        }

        public override void UnloadContent()
        {
            SpriteFont = null;
        }

        public void PauseScreen_Removing(object sender, EventArgs e)
        {
            MenuEntries.Clear();
            _before.ActivateScreen();
            _session.Resume();
        }

        private void PauseScreen_Entering(object sender, TransitionEventArgs e)
        {
            float effect = (float)Math.Pow(e.Percent - 1, 2) * -100;
            foreach (MenuEntry entry in MenuEntries)
            {
                entry.Acceleration = new Vector2(effect, 0);
                entry.Position = entry.DefaultPosition + entry.Acceleration;
                entry.Opacity = e.Percent;
            }

            TitlePosition = InitialTitlePosition + new Vector2(0, effect);
            TitleOpacity = e.Percent;
        }

        private void PauseScreen_Exiting(object sender, TransitionEventArgs e)
        {
            float effect = (float)Math.Pow(e.Percent - 1, 2) * -100;
            foreach (MenuEntry entry in MenuEntries)
            {
                entry.Acceleration = new Vector2(effect, 0);
                entry.Position = entry.DefaultPosition + entry.Acceleration;
                entry.Scale = e.Percent;
                entry.Opacity = e.Percent;
            }

            TitlePosition = InitialTitlePosition - new Vector2(0, effect);
            TitleOpacity = e.Percent;
        }

        private void ResumeSelect(object sender, EventArgs e)
        {
            ExitScreen();
        }
    
        private void QuitSelect(object sender, EventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}