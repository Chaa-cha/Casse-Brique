using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CasseBrique
{
    class Ship : GameObject
    {
        public Ship() : base()
        {
            texture = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("images/ship");

            position = new Vector2(Screen_Width / 2 - texture.Width / 2,
                                   Screen_Height - texture.Height);

            newposition = position;
            smooth = 0.8f;
            speedX = 0.1f;
        }

        public void ResetShip()
        {
            direction = Vector2.Zero;

            position = new Vector2(Screen_Width / 2 - texture.Width / 2,
                                   Screen_Height - texture.Height);

            newposition = position;
            smooth = 0.8f;
            speedX = 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            IInputs inputs = ServiceLocator.GetService<IInputs>();

            // ----- MOVEMENT SHIP
            if (inputs.isDown(Keys.Left))
            { direction -= Vector2.UnitX; }

            if (inputs.isDown(Keys.Right))
            { direction += Vector2.UnitX; }

            newposition += direction * speedX;
            position = Vector2.Lerp(position, newposition, smooth);

            // ----- MAP LIMIT SHIP
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
