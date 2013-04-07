using Microsoft.Xna.Framework;

namespace GameEngine
{
    public abstract class GameScreen
    {
        private bool _isContentLoaded;
        private bool _isContentUnloaded;
        private bool _isInitialized;

        public bool IsContentLoaded
        {
            get { return _isContentLoaded; }
        }

        public bool IsContentUnloaded
        {
            get { return _isContentUnloaded; }
        }

        public ScreenState State
        {
            get;
            protected set;
        }

        public bool IsActive
        {
            // TODO: TransitionOn/TransitionOff
            get { return State == ScreenState.Active; }
        }

        public ScreenManager ScreenManager { get; set; }

        public abstract bool AcceptsInput { get; }

        public InputMap InputMap { get; private set; }

        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            InputMap = new InputMap();
            _isInitialized = true;
        }

        public void Update(GameTime gameTime)
        {
            InputSystem.Update(gameTime);

            if (State == ScreenState.Frozen || State == ScreenState.Inactive)
            {
                return;
            }

            // TODO: TransitionOn/TransitionOff states

            if (State != ScreenState.Active && State != ScreenState.Hidden)
            {
                return;
            }

            UpdateScreen(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (State == ScreenState.Inactive || State == ScreenState.Hidden)
            {
                return;
            }

            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            DrawScreen(gameTime);
            spriteBatch.End();
        }

        public void FreezeScreen()
        {
            State = ScreenState.Frozen;
        }

        public virtual void LoadContent()
        {
            _isContentLoaded = true;
        }

        public virtual void UnloadContent()
        {
            _isContentUnloaded = true;
        }

        public virtual void HandleInput()
        {
        }

        public abstract void InitializeScreen();

        protected abstract void UpdateScreen(GameTime gameTime);

        protected abstract void DrawScreen(GameTime gameTime);
    }
}