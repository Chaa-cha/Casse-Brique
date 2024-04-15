using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    class Bricks : GameObject
    {
        public int LifeBrick;
        public int CodeBrick;
        public Bricks(int brick) : base()
        {
            texture = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("images/Brick_" + brick);

            if (brick == 1) // GREY
            { CodeBrick = brick; LifeBrick = 1; }

            if (brick == 2) // BLUE CRACK
            { CodeBrick = brick; LifeBrick = 2; }

            if (brick == 3) // BLUE
            { CodeBrick = brick; LifeBrick = 3; }

            if (brick == 4) // GREEN CRACK
            { CodeBrick = brick; LifeBrick = 4; }

            if (brick == 5) // GREEN
            { CodeBrick = brick; LifeBrick = 5; }

            if (brick == 6) // YELLOW CRACK
            { CodeBrick = brick; LifeBrick = 6; }

            if (brick == 7) // YELLOW
            { CodeBrick = brick; LifeBrick = 7; }

            if (brick == 8) // RED CRACK
            { CodeBrick = brick; LifeBrick = 8; }

            if (brick == 9) // RED
            { CodeBrick = brick; LifeBrick = 9; }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
