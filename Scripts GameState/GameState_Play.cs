using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace CasseBrique
{
    public class GameState_Play : GameState
    {
        private Backgrounds _backgrounds;
        private Paddle _Paddle;
        private Ball _Ball;
        private Bricks _Bricks;

        public bool Pause = false;
        public bool GameOver = false;
        public bool NextLevel = false;
        public bool BallStick = true; // public uniquement pour test
        private bool Left = false;
        private bool Right = false;

        public int LevelCleared = 0;
        private int column = 25;
        private int lign = 10;
        public int[,] Lvl;

        private List<Bricks> ListBricks;

        //===========================================================================================================================================
        public GameState_Play(CasseBrique casseBrique) : base()
        {
            _backgrounds = new Backgrounds("bg_Play");
            _Paddle = new Paddle();
            _Ball = new Ball();
            _Bricks = new Bricks(1);

            Lvl = new int[lign, column];

            LoadLevel(1, Lvl);
        }

        // ----- LOADING LEVELS MAPS
        public void LoadLevel(int nLvl, int[,] Lvl)
        {
            string[] line = File.ReadAllLines("Levels/Level " + nLvl + ".txt");

            for (int l = 0; l < lign; l++)
            {
                string Line = line[l];
                for (int c = 0; c < column; c++)
                {
                    Lvl[l, c] = int.Parse(Line.Substring(c, 1));
                }
            }

            ListBricks = new List<Bricks>();
            for (int l = 0; l < lign; l++)
            {
                for (int c = 0; c < column; c++)
                {
                    if (Lvl[l, c] == 1) // GREY BRICK
                    {
                        Bricks bricks = new Bricks(1);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }

                    if (Lvl[l, c] == 2) // BLUE BRICK CRACK
                    {
                        Bricks bricks = new Bricks(2);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }

                    if (Lvl[l, c] == 3) // BLUE BRICK 
                    {
                        Bricks bricks = new Bricks(3);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }

                    if (Lvl[l, c] == 4) // GREEN BRICK CRACK
                    {
                        Bricks bricks = new Bricks(4);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }

                    if (Lvl[l, c] == 5) // GREEN BRICK 
                    {
                        Bricks bricks = new Bricks(5);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }

                    if (Lvl[l, c] == 6) // YELLOW BRICK CRACK
                    {
                        Bricks bricks = new Bricks(6);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }

                    if (Lvl[l, c] == 7) // YELLOW BRICK
                    {
                        Bricks bricks = new Bricks(7);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }

                    if (Lvl[l, c] == 8) // RED BRICK CRACK
                    {
                        Bricks bricks = new Bricks(8);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }

                    if (Lvl[l, c] == 9) // RED BRICK
                    {
                        Bricks bricks = new Bricks(9);
                        bricks.position = new Vector2(_Bricks.position.X + (c * _Bricks.texture.Width), _Bricks.position.Y + (l * _Bricks.texture.Height));
                        ListBricks.Add(bricks);
                    }
                }
            }
        }

        //============================================================= UPDATE ======================================================================
        public override void Update(GameTime gameTime)
        {
            ISoundManager SoundManager = ServiceLocator.GetService<ISoundManager>();
            IPlayerLife life = ServiceLocator.GetService<IPlayerLife>();
            IScore score = ServiceLocator.GetService<IScore>();
            IInputs inputs = ServiceLocator.GetService<IInputs>();

            _Paddle.Update(gameTime);
            _Ball.Update(gameTime);

            // ----- BOUNDING BOX
            Rectangle BoudingBox_Ball = new Rectangle((int)_Ball.position.X, (int)_Ball.position.Y, _Ball.texture.Width, _Ball.texture.Height);
            Rectangle BoudingBox_Paddle = new Rectangle((int)_Paddle.position.X, (int)_Paddle.position.Y, _Paddle.texture.Width, _Paddle.texture.Height);
            bool Collision_Ball_Paddle = BoudingBox_Ball.Intersects(BoudingBox_Paddle);

            // ----- LOSE A LIFE
            if (_Ball.position.Y >= Screen_Height - _Ball.texture.Height)
            {
                SoundManager.SoundEffect("Lose a Life");

                BallStick = true;

                if (life != null)
                { life.LifeRemove(1); }

                _Ball.ResetBall();
                _Paddle.ResetPaddle();
            }

            // ----- BALL LAUNCH DIRECTION
            if (BallStick == true)
            {
                _Ball.position.X = (_Paddle.position.X + _Paddle.texture.Width / 2) - (_Ball.texture.Width / 2);
                _Ball.position.Y = (_Paddle.position.Y - _Ball.texture.Height);

                if (_Paddle.position.X + _Paddle.texture.Width / 2 <= Screen_Width / 2)
                {
                    Left = true;
                    Right = false;
                }

                if (_Paddle.position.X + _Paddle.texture.Width / 2 >= Screen_Width / 2)
                {
                    Left = false;
                    Right = true;
                }

                if (Left == true)
                {
                    _Ball.DirectionLeft = true;
                    _Ball.DirectionRight = false;
                }

                if (Right == true)
                {
                    _Ball.DirectionLeft = false;
                    _Ball.DirectionRight = true;
                }
            }

            // ----- BALL LAUNCH
            if (inputs.isDown(Keys.Space))
            {
                BallStick = false;
                Left = false;
                Right = false;
            }

            // ----- COLLISION BALL-PADDLE
            if (Collision_Ball_Paddle && BallStick == false)
            {
                SoundManager.SoundEffect("collision_paddle");

                _Ball.position.Y = _Paddle.position.Y - _Ball.texture.Height;
                _Ball.speedY = -_Ball.speedY;
            }

            // ----- COLLISION BALL-BRICKS
            for (int i = ListBricks.Count - 1; i >= 0; i--)
            {
                bool FallingBrick = false;

                Bricks bricks = ListBricks[i];
                bricks.Update(gameTime);

                Rectangle BoudingBox_Bricks = new Rectangle((int)bricks.position.X, (int)bricks.position.Y, bricks.texture.Width, bricks.texture.Height);
                bool Collision_Ball_Bricks = BoudingBox_Ball.Intersects(BoudingBox_Bricks);

                if (Collision_Ball_Bricks == true)
                {
                    SoundManager.SoundEffect("collision_brick");

                    _Ball.speedX = -_Ball.speedX;
                    _Ball.speedY = -_Ball.speedY;
                    FallingBrick = true;

                    bricks.LifeBrick -= 1;
                }

                // ----- SCORE BRICKS
                if (FallingBrick == true)
                {
                    // DELETE GREY BRICK
                    if (bricks.LifeBrick <= 0)
                    {
                        ListBricks.Remove(bricks);

                        if (score != null)
                        { score.ScoreAdd(10); }
                    }

                    // DELETE BLUE BRICK CRACK
                    if (bricks.CodeBrick == 2 && bricks.LifeBrick == 1)
                    {
                        ListBricks.Remove(bricks);

                        float posX = bricks.position.X;
                        float posY = bricks.position.Y;

                        bricks = new Bricks(1);
                        bricks.position = new Vector2(posX, posY);
                        ListBricks.Add(bricks);

                        if (score != null)
                        { score.ScoreAdd(10); }
                    }

                    // DELETE BLUE BRICK
                    if (bricks.CodeBrick == 3 && bricks.LifeBrick == 2)
                    {
                        ListBricks.Remove(bricks);

                        float posX = bricks.position.X;
                        float posY = bricks.position.Y;

                        bricks = new Bricks(2);
                        bricks.position = new Vector2(posX, posY);
                        ListBricks.Add(bricks);
                    }

                    // DELETE GREEN BRICK CRACK
                    if (bricks.CodeBrick == 4 && bricks.LifeBrick == 3)
                    {
                        ListBricks.Remove(bricks);

                        float posX = bricks.position.X;
                        float posY = bricks.position.Y;

                        bricks = new Bricks(3);
                        bricks.position = new Vector2(posX, posY);
                        ListBricks.Add(bricks);

                        if (score != null)
                        { score.ScoreAdd(10); }
                    }

                    // DELETE GREEN BRICK
                    if (bricks.CodeBrick == 5 && bricks.LifeBrick == 4)
                    {
                        ListBricks.Remove(bricks);

                        float posX = bricks.position.X;
                        float posY = bricks.position.Y;

                        bricks = new Bricks(4);
                        bricks.position = new Vector2(posX, posY);
                        ListBricks.Add(bricks);
                    }

                    // DELETE YELLOW BRICK CRACK
                    if (bricks.CodeBrick == 6 && bricks.LifeBrick == 5)
                    {
                        ListBricks.Remove(bricks);

                        float posX = bricks.position.X;
                        float posY = bricks.position.Y;

                        bricks = new Bricks(5);
                        bricks.position = new Vector2(posX, posY);
                        ListBricks.Add(bricks);

                        if (score != null)
                        { score.ScoreAdd(10); }
                    }

                    // DELETE YELLOW BRICK
                    if (bricks.CodeBrick == 7 && bricks.LifeBrick == 6)
                    {
                        ListBricks.Remove(bricks);

                        float posX = bricks.position.X;
                        float posY = bricks.position.Y;

                        bricks = new Bricks(6);
                        bricks.position = new Vector2(posX, posY);
                        ListBricks.Add(bricks);
                    }

                    // DELETE RED BRICK CRACK
                    if (bricks.CodeBrick == 8 && bricks.LifeBrick == 7)
                    {
                        ListBricks.Remove(bricks);

                        float posX = bricks.position.X;
                        float posY = bricks.position.Y;

                        bricks = new Bricks(7);
                        bricks.position = new Vector2(posX, posY);
                        ListBricks.Add(bricks);

                        if (score != null)
                        { score.ScoreAdd(10); }
                    }

                    // DELETE RED BRICK
                    if (bricks.CodeBrick == 9 && bricks.LifeBrick == 8)
                    {
                        ListBricks.Remove(bricks);

                        float posX = bricks.position.X;
                        float posY = bricks.position.Y;

                        bricks = new Bricks(8);
                        bricks.position = new Vector2(posX, posY);
                        ListBricks.Add(bricks);
                    }
                }
            }

            // ----- LEVEL CLEARED
            if (ListBricks.Count == 0)
            {
                LevelCleared += 1;

                switch (LevelCleared)
                {
                    case 1:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;

                        LoadLevel(2, Lvl);
                        break;

                    case 2:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;

                        LoadLevel(3, Lvl);
                        break;

                    case 3:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;
                        NextLevel = true;

                        LoadLevel(4, Lvl);
                        break;

                    case 4:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;

                        LoadLevel(5, Lvl);
                        break;

                    case 5:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;

                        LoadLevel(6, Lvl);
                        break;

                    case 6:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;
                        NextLevel = true;

                        LoadLevel(7, Lvl);
                        break;

                    case 7:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;

                        LoadLevel(8, Lvl);
                        break;

                    case 8:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;

                        LoadLevel(9, Lvl);
                        break;

                    case 9:
                        _Ball.ResetBall();
                        _Paddle.ResetPaddle();
                        BallStick = true;
                        break;
                }
            }
            base.Update(gameTime);
        }

        //=============================================================== DRAW ======================================================================
        public override void Draw()
        {
            _backgrounds.Draw();

            foreach (var bricks in ListBricks)
            {
                bricks.Draw();
            }

            _Paddle.Draw();
            _Ball.Draw();

            Debug.WriteLine(ListBricks.Count);

            base.Draw();
        }
    }
}
