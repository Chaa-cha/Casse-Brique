using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CasseBrique
{
    public class CasseBrique : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _mainFont;
        private SpriteFont _pauseFont;

        private GameState _currentState;
        private GameState_Menu _gameState_Menu;
        private GameState_Play _gameState_Play;
        private GameState_MiniGame _gameState_MiniGame;
        private GameState_GameOver _gameState_GameOver;
        private GameState_Win _gameState_Win;

        private SoundManager _soundManager = new SoundManager();
        private PlayerLife _playerLife = new PlayerLife();
        private Score _score = new Score();
        private Inputs _Inputs = new Inputs();

        private Texture2D PauseRectangle;

        public int Screen_Width;
        public int Screen_Height;

        float TimerCredits = 0;
        float Alpha = 1;
        int CreditsY = 0;

        int LevelSkip = 1;

        public bool PauseGame = false;

        public CasseBrique()
        {
            _graphics = new GraphicsDeviceManager(this);
            ServiceLocator.RegisterService<GraphicsDeviceManager>(_graphics);
            ServiceLocator.RegisterService<ContentManager>(Content);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = ("Sea Breaker");

            Screen_Width = _graphics.PreferredBackBufferWidth;
            Screen_Height = _graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ServiceLocator.RegisterService<SpriteBatch>(_spriteBatch);
            _mainFont = Content.Load<SpriteFont>("fonts/Marly Tail");
            _pauseFont = Content.Load<SpriteFont>("fonts/Marly Tail - Pause");

            _gameState_Menu = new GameState_Menu(this);
            _gameState_Play = new GameState_Play(this);
            _gameState_MiniGame = new GameState_MiniGame(this);
            _gameState_GameOver = new GameState_GameOver(this);
            _gameState_Win = new GameState_Win(this);

            _currentState = _gameState_Menu;

            ISoundManager SoundManager = ServiceLocator.GetService<ISoundManager>();
            SoundManager.SoundPlay("Music_Menu");

            PauseRectangle = new Texture2D(GraphicsDevice, 1, 1);
            PauseRectangle.SetData(new[] { Color.White });
        }

        //============================================================= UPDATE ======================================================================
        protected override void Update(GameTime gameTime)
        {
            IScore score = ServiceLocator.GetService<IScore>();
            IPlayerLife life = ServiceLocator.GetService<IPlayerLife>();
            ISoundManager SoundManager = ServiceLocator.GetService<ISoundManager>();
            IInputs inputs = ServiceLocator.GetService<IInputs>();

            if (inputs.IsPressed(Keys.Escape))
            { Exit(); }

            // ----- LEVEL SKIP FOR TEST
            if (_currentState == _gameState_Play && inputs.IsPressed(Keys.A))
            {
                LevelSkip = LevelSkip + 1;

                if (LevelSkip <= 9)
                {
                    _gameState_Play.NextLevel = true;
                    _gameState_Play.BallStick = true;
                    _gameState_Play.LevelCleared = _gameState_Play.LevelCleared + 1;
                    _gameState_Play.LoadLevel(LevelSkip, _gameState_Play.Lvl);
                }
                else
                {
                    _currentState = _gameState_Win;
                    LevelSkip = 1;
                }
            }

            // ----- PAUSE GAME
            if (inputs.IsPressed(Keys.P) && _currentState == _gameState_Play && PauseGame == false)
            { PauseGame = true; }
            else if (inputs.IsPressed(Keys.P) && _currentState == _gameState_Play && PauseGame)
            { PauseGame = false; }

            // ----- MENU
            if (_currentState == _gameState_Menu && inputs.IsPressed(Keys.Enter))
            {
                _currentState = _gameState_Play;
                SoundManager.SoundPlay("Music_Play");
            }

            // ----- MINI GAME START - LEVEL 3 CLEARED
            if (_currentState == _gameState_Play && _gameState_Play.LevelCleared == 3 && _gameState_Play.NextLevel == true)
            {
                _currentState = _gameState_MiniGame;
                SoundManager.SoundPlay("Music_MiniGame");
            }

            // ----- MINI GAME START - LEVEL 6 CLEARED
            if (_currentState == _gameState_Play && _gameState_Play.LevelCleared == 6 && _gameState_Play.NextLevel == true)
            {
                _currentState = _gameState_MiniGame;
                SoundManager.SoundPlay("Music_MiniGame");
            }

            // ----- LAST LEVEL CLEARED - WIN
            if (_currentState == _gameState_Play && _gameState_Play.LevelCleared == 9)
            { _currentState = _gameState_Win; }

            if (_currentState == _gameState_Win)
            {
                TimerCredits += 0.1f;
                CreditsY += 1;

                // ----- RESET GAME
                if (inputs.IsPressed(Keys.Enter))
                {
                    TimerCredits = 0;
                    CreditsY = 0;

                    life.ResetLife();
                    score.ResetScore();
                    SoundManager.SoundPlay("Music_Play");

                    _currentState = _gameState_Play;
                    _gameState_Play.LoadLevel(1, _gameState_Play.Lvl);
                    _gameState_Play.LevelCleared = 0;
                }
            }

            // ----- MINI GAME CLEARED
            if (_currentState == _gameState_MiniGame && _gameState_MiniGame.MiniGameOn == false)
            {
                _gameState_Play.NextLevel = false;
                _gameState_MiniGame.MiniGameOn = true;
                _currentState = _gameState_Play;

                SoundManager.SoundPlay("Music_Play");
            }

            // ----- GAMEOVER
            bool GameOver = life.Get();

            if (GameOver.Equals(true))
            {
                LevelSkip = 1;

                _currentState = _gameState_GameOver;
                SoundManager.SoundPlay("Music_Gameover");

                // ----- RESET GAME
                if (inputs.IsPressed(Keys.Enter))
                {
                    life.ResetLife();
                    score.ResetScore();
                    SoundManager.SoundPlay("Music_Play");

                    _currentState = _gameState_Play;
                    _gameState_Play.LoadLevel(1, _gameState_Play.Lvl);
                    _gameState_Play.LevelCleared = 0;
                }
            }

            inputs.Update(gameTime);

            if (PauseGame == false)
            { _currentState.Update(gameTime); }

            base.Update(gameTime);
        }

        //=============================================================== DRAW ======================================================================
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _currentState.Draw();

            // ----- PRESS ENTER MENU
            if (_currentState == _gameState_Menu)
            {
                Alpha -= 0.01f;

                if (Alpha <= 0.5f)
                { Alpha = 1; }

                if (Alpha >= 1)
                { Alpha -= 0.01f; }

                _spriteBatch.DrawString(_pauseFont, "Press Enter", new Vector2(Screen_Width / 2 - (_pauseFont.MeasureString("Press Enter").X / 2),
                    Screen_Height - 100), Color.RoyalBlue * Alpha);
            }

            // ----- LIFE + SCORE
            if (_currentState == _gameState_Play || _currentState == _gameState_MiniGame)
            {
                _spriteBatch.DrawString(_mainFont, "Life : " + _playerLife.GetLifePoint() + "\nScore : " + _score.Get(),
                    new Vector2(10, 0), Color.White);
            }

            // ----- LEVEL NUMBER
            if (_currentState == _gameState_Play)
            { _spriteBatch.DrawString(_mainFont, "Level : " + (_gameState_Play.LevelCleared + 1), new Vector2(700, 0), Color.White); }


            // ----- PAUSE
            if (_currentState == _gameState_Play && PauseGame)
            {
                _spriteBatch.Draw(PauseRectangle, new Rectangle(0, 0, Screen_Width, Screen_Height), Color.Gray * 0.5f);

                _spriteBatch.DrawString(_pauseFont, "PAUSE", new Vector2((Screen_Width / 2) - (_pauseFont.MeasureString("PAUSE").X / 2),
                    (Screen_Height / 2) - (_pauseFont.MeasureString("PAUSE").Y / 2)),
                    Color.White);
            }

            // ----- CREDITS
            if (_currentState == _gameState_Win && TimerCredits >= 10)
            {
                _spriteBatch.Draw(PauseRectangle, new Rectangle(0, 0, Screen_Width, Screen_Height), Color.Black * 0.5f);

                string CreditsThanks; string C1; string C2; string C3; string C4;
                string C5; string C6; string C7; string C8; string C9;

                CreditsThanks = "Thank you for playing !\n";
                C1 = "\n\n- Musics -";
                C2 = "\n\nalkakrab";
                C3 = "\n\n\n\n- Sound Effects -";
                C4 = "\n\n\nkronbits  :  kronbits.itch.io/freesfx";
                C5 = "\n\n\n\n\nsemkou";
                C6 = "\n\n\n\n\n- Sprites and Backgrounds -";
                C7 = "\n\n\n\n\n\nJamie Cross";
                C8 = "\n\n\n\n\n\nlornn";
                C9 = "\n\n\n\n\n\n\n\nAnd you !";

                _spriteBatch.DrawString(_pauseFont, CreditsThanks, new Vector2(Screen_Width / 2 - (_pauseFont.MeasureString(CreditsThanks).X / 2),
                                (Screen_Height + 100) - CreditsY), Color.AntiqueWhite);

                _spriteBatch.DrawString(_mainFont, C1, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C1).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(CreditsThanks).Y) - CreditsY), Color.DeepSkyBlue);

                _spriteBatch.DrawString(_mainFont, C2, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C2).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(C1).Y) - CreditsY), Color.AntiqueWhite);

                _spriteBatch.DrawString(_mainFont, C3, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C3).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(C2).Y) - CreditsY), Color.DeepSkyBlue);

                _spriteBatch.DrawString(_mainFont, C4, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C4).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(C3).Y) - CreditsY), Color.AntiqueWhite);

                _spriteBatch.DrawString(_mainFont, C5, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C5).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(C4).Y) - CreditsY), Color.AntiqueWhite);

                _spriteBatch.DrawString(_mainFont, C6, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C6).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(C5).Y) - CreditsY), Color.DeepSkyBlue);

                _spriteBatch.DrawString(_mainFont, C7, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C7).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(C6).Y) - CreditsY), Color.AntiqueWhite);

                _spriteBatch.DrawString(_mainFont, C8, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C8).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(C7).Y) - CreditsY), Color.AntiqueWhite);

                _spriteBatch.DrawString(_mainFont, C9, new Vector2(Screen_Width / 2 - (_mainFont.MeasureString(C9).X / 2),
                                (Screen_Height + 100 + _mainFont.MeasureString(C8).Y) - CreditsY), Color.AntiqueWhite);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}