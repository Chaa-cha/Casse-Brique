using Microsoft.Xna.Framework;

namespace CasseBrique
{
    public class GameState_Win : GameState
    {
        private Backgrounds _backgrounds;

        public GameState_Win(CasseBrique casseBrique) : base()
        {
            _backgrounds = new Backgrounds("bg_Win");
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
