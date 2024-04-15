using Microsoft.Xna.Framework;

namespace CasseBrique
{
    public class GameState
    {
        public int Screen_Width;
        public int Screen_Height;

        public GameState()
        {
            GraphicsDeviceManager g = ServiceLocator.GetService<GraphicsDeviceManager>();
            Screen_Width = g.PreferredBackBufferWidth;
            Screen_Height = g.PreferredBackBufferHeight;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw()
        {

        }
    }
}
