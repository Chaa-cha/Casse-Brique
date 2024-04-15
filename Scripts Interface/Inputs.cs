using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CasseBrique
{
    public interface IInputs
    {
        bool IsPressed(Keys keys);
        bool isDown(Keys keys);
        void Update(GameTime gameTime);
    }

    public class Inputs : IInputs
    {
        private KeyboardState _oldKS;
        public bool PauseGame = true;

        public Inputs()
        { ServiceLocator.RegisterService<IInputs>(this); }

        public void Update(GameTime gameTime)
        { _oldKS = Keyboard.GetState(); }

        public bool IsPressed(Keys keys)
            => Keyboard.GetState().IsKeyDown(keys) && _oldKS.IsKeyUp(keys);

        public bool isDown(Keys keys)
            => Keyboard.GetState().IsKeyDown(keys);
    }
}