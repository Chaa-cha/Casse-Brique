using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CasseBrique
{
    class Letters : GameObject
    {
        public string Name;
        public bool Falling_Letter;
        public Letters(string letter) : base()
        {
            texture = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("images/letter_" + letter);

            Random rnd = new Random();

            if (letter == "L")
            {
                int x = rnd.Next(10, 700);
                int y = rnd.Next(-100, -texture.Height);

                position = new Vector2(x, y);

                Falling_Letter = true;
                Name = "L";
            }
            if (letter == "I")
            {
                int x = rnd.Next(10, 700);
                int y = rnd.Next(-300, -100);

                position = new Vector2(x, y);

                Falling_Letter = true;
                Name = "I";
            }
            if (letter == "F")
            {
                int x = rnd.Next(10, 700);
                int y = rnd.Next(-500, -300);

                position = new Vector2(x, y);

                Falling_Letter = true;
                Name = "F";
            }
            if (letter == "E")
            {
                int x = rnd.Next(10, 700);
                int y = rnd.Next(-700, -500);

                position = new Vector2(x, y);

                Falling_Letter = true;
                Name = "E";
            }
        }

        public void ResetLetters(string letter)
        {
            Random rnd = new Random();

            if (letter == "L")
            {
                int x = rnd.Next(10, 700);
                int y = rnd.Next(-100, -texture.Height);

                position = new Vector2(x, y);

                Falling_Letter = true;
                Name = "L";
            }
            if (letter == "I")
            {
                int x = rnd.Next(10, 700);
                int y = rnd.Next(-300, -100);

                position = new Vector2(x, y);

                Falling_Letter = true;
                Name = "I";
            }
            if (letter == "F")
            {
                int x = rnd.Next(10, 700);
                int y = rnd.Next(-500, -300);

                position = new Vector2(x, y);

                Falling_Letter = true;
                Name = "F";
            }
            if (letter == "E")
            {
                int x = rnd.Next(10, 700);
                int y = rnd.Next(-700, -500);

                position = new Vector2(x, y);

                Falling_Letter = true;
                Name = "E";
            }
        }

        public override void Update(GameTime gameTime)
        {
            position += new Vector2(0, 2);

            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
