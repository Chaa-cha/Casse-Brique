using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CasseBrique
{
    public class GameState_MiniGame : GameState
    {
        private Backgrounds _backgrounds;
        private Ship _Ship;

        public bool MiniGameOn = true;
        private bool Letter_E_Get = false;
        private bool No_Bonus = false;
        private bool Get_Bonus = false;

        private int Letters_Get = 0;

        private List<Letters> ListLetters;

        public GameState_MiniGame(CasseBrique casseBrique) : base()
        {
            _backgrounds = new Backgrounds("sea");
            _Ship = new Ship();

            ListLetters = new List<Letters>()
            {
                new Letters("L"),
                new Letters("I"),
                new Letters("F"),
                new Letters("E")
            };
        }

        //============================================================= UPDATE ======================================================================
        public override void Update(GameTime gameTime)
        {
            ISoundManager SoundManager = ServiceLocator.GetService<ISoundManager>();
            IPlayerLife life = ServiceLocator.GetService<IPlayerLife>();
            IScore score = ServiceLocator.GetService<IScore>();

            // ----- BACKGROUND MOVEMENT
            _backgrounds.Update(gameTime);
            _backgrounds.position.Y += 2;

            if (_backgrounds.position.Y >= 0)
            { _backgrounds.position.Y = -100; }

            // ----- BOUNDING BOX SHIP
            _Ship.Update(gameTime);
            Rectangle BoudingBox_Ship = new Rectangle((int)_Ship.position.X, (int)_Ship.position.Y, _Ship.texture.Width, _Ship.texture.Height);

            // ----- BOUNDING BOX LETTERS
            foreach (Letters letter in ListLetters)
            {
                Rectangle BoudingBox_Letters = new Rectangle((int)letter.position.X, (int)letter.position.Y, letter.texture.Width, letter.texture.Height);
                bool Collision_Ship_Letters = BoudingBox_Ship.Intersects(BoudingBox_Letters);

                // ----- LETTERS MOVEMENTS
                if (letter.Falling_Letter == true && Collision_Ship_Letters == false)
                { letter.Update(gameTime); }

                // ----- COLLISION SHIP-LETTERS
                if (Collision_Ship_Letters == true)
                {
                    if (letter.Name == "L" && Collision_Ship_Letters == true)
                    {
                        letter.position = new Vector2(Screen_Width - (letter.texture.Width * 4), 10);
                        letter.Falling_Letter = false;
                        Letters_Get += 1;
                    }

                    if (letter.Name == "I" && Collision_Ship_Letters == true)
                    {
                        letter.position = new Vector2(Screen_Width - (letter.texture.Width * 6), 10);
                        letter.Falling_Letter = false;
                        Letters_Get += 1;
                    }

                    if (letter.Name == "F" && Collision_Ship_Letters == true)
                    {
                        letter.position = new Vector2(Screen_Width - (letter.texture.Width * 2.5f), 10);
                        letter.Falling_Letter = false;
                        Letters_Get += 1;
                    }

                    if (letter.Name == "E" && Collision_Ship_Letters == true)
                    {
                        letter.position = new Vector2(Screen_Width - (letter.texture.Width * 1.5f), 10);
                        letter.Falling_Letter = false;
                        Letters_Get += 1;

                        Letter_E_Get = true;
                    }
                }

                // ----- MINI GAME RESULT
                if (Letter_E_Get == false && letter.Name == "E" && letter.position.Y >= Screen_Height)
                { No_Bonus = true; }

                if (Letters_Get < 4 && Letter_E_Get == true)
                { No_Bonus = true; }

                if (Letters_Get == 4)
                {
                    SoundManager.SoundEffect("One Life");
                    Get_Bonus = true;

                    if (life != null)
                    { life.LifeAdd(1); }

                    if (score != null)
                    { score.ScoreAdd(200); }
                }

                // ----- MINI GAME RESET
                if (No_Bonus == true || Get_Bonus == true)
                {
                    Letter_E_Get = false;
                    Letters_Get = 0;

                    _Ship.newposition -= new Vector2(0, 2);

                    if (_Ship.position.Y + _Ship.texture.Height <= 0)
                    {
                        _Ship.ResetShip();

                        Get_Bonus = false;
                        No_Bonus = false;
                        MiniGameOn = false;
                    }

                    // ----- RESET LETTERS
                    if (letter.Name == "L")
                    { letter.ResetLetters("L"); }

                    if (letter.Name == "I")
                    { letter.ResetLetters("I"); }

                    if (letter.Name == "F")
                    { letter.ResetLetters("F"); }

                    if (letter.Name == "E")
                    { letter.ResetLetters("E"); }
                }
            }
            base.Update(gameTime);
        }

        //=============================================================== DRAW ======================================================================
        public override void Draw()
        {
            _backgrounds.Draw();
            _Ship.Draw();

            foreach (Letters letter in ListLetters)
            { letter.Draw(); }

            base.Draw();
        }
    }
}
