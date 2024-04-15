using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    class Backgrounds : GameObject
    {
        public Backgrounds(string bgName) : base()
        {
            texture = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("images/" + bgName);
            position = position = new Vector2(0, 0);
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
