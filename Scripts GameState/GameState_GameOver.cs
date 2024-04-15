using Microsoft.Xna.Framework;

namespace CasseBrique
{
    public class GameState_GameOver : GameState
    {
        private Backgrounds _backgrounds;

        public GameState_GameOver(CasseBrique casseBrique) : base()
        {
            _backgrounds = new Backgrounds("bg_GameOver");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            _backgrounds.Draw();

            base.Draw();
        }
    }
}
