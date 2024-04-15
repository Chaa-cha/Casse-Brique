using Microsoft.Xna.Framework;

namespace CasseBrique
{
    public class GameState_Menu : GameState
    {
        private Backgrounds _backgrounds;
        private GameState gameState;

        public GameState_Menu(CasseBrique casseBrique) : base()
        {
            _backgrounds = new Backgrounds("bg_Menu");
        }

        public GameState_Menu(GameState gameState)
        {
            this.gameState = gameState;
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
