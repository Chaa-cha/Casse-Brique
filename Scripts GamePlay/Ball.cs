using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    class Ball : GameObject
    {
        public bool DirectionLeft = false;
        public bool DirectionRight = false;

        public Ball() : base()
        {
            texture = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("images/ball");

            position = new Vector2(Screen_Width / 2 - texture.Width / 2,
                                   Screen_Height - texture.Height * 2);

            speedX = 4;
            speedY = 4;
        }

        public void ResetBall()
        {
            position = new Vector2(Screen_Width / 2 - texture.Width / 2,
                                   Screen_Height - texture.Height * 2);

            speedX = 4;
            speedY = 4;
        }

        public override void Update(GameTime gameTime)
        {
            // DIRECTION
            if (DirectionLeft)
            { position -= new Vector2(speedX, speedY); }

            if (DirectionRight)
            { position += new Vector2(speedX, speedY); }

            // MAP LIMIT BALL
            if (position.X <= 0)
            {
                position.X = 0;
                speedX = -speedX;
            }

            if (position.X >= Screen_Width - texture.Width)
            {
                position.X = Screen_Width - texture.Width;
                speedX = -speedX;
            }

            if (position.Y <= 0)
            {
                position.Y = 0;
                speedY = -speedY;
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
