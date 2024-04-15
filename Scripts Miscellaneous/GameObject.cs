using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    public class GameObject
    {
        public int Screen_Width;
        public int Screen_Height;

        public Vector2 position;
        public Vector2 newposition;
        public Vector2 direction = new Vector2(0, 0);
        public Texture2D texture;

        public float speedX;
        public float speedY;

        public float smooth;

        public GameObject()
        {
            GraphicsDeviceManager _graphic = ServiceLocator.GetService<GraphicsDeviceManager>();

            Screen_Width = _graphic.PreferredBackBufferWidth;
            Screen_Height = _graphic.PreferredBackBufferHeight;

            position = new Vector2();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw()
        {
            if (texture != null)
            {
                ServiceLocator.GetService<SpriteBatch>().Draw(texture, position, Color.White);
            }
        }
    }
}
