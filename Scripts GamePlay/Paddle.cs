using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CasseBrique
{
    class Paddle : GameObject
    {
        public Paddle() : base()
        {
            texture = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("images/paddle");

            position = new Vector2(Screen_Width / 2 - texture.Width / 2,
                                   Screen_Height - texture.Height);
        }

        public void ResetPaddle()
        {
            position = new Vector2(Screen_Width / 2 - texture.Width / 2,
                                   Screen_Height - texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            IInputs inputs = ServiceLocator.GetService<IInputs>();

            // ----- MOVEMENT PADDLE
            if (inputs.isDown(Keys.Left))
            { position -= new Vector2(5, 0); }

            if (inputs.isDown(Keys.Right))
            { position += new Vector2(5, 0); }

            // ----- MAP LIMIT PADDLE
            if (position.X <= 0)
            { position.X = 0; }

            if (position.X >= Screen_Width - texture.Width)
            { position.X = Screen_Width - texture.Width; }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
