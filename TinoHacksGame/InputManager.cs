using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoHacksGame
{
    /// <summary>
    /// Handles all input done by the user(s) on the controller.
    /// </summary>
    public class InputManager
    {
        private GamePadState[] prevStates, currentStates;
        /// <summary>
        /// Retrieves the singleton instance of the <c>InputManager</c>.
        /// </summary>
        /// <returns></returns>
        public static InputManager GetInstance()
        {
            if (instance == null)
                instance = new InputManager();

            return instance;
        }

        private InputManager()
        {
            currentStates = new GamePadState[4];
            prevStates = new GamePadState[4];
        }

        private static InputManager instance;

        /// <summary>
        /// Updates the logic and conditional checking for the <c>InputManager</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < currentStates.Length; i++)
                prevStates[i] = currentStates[i];
            for (int i = 0; i < currentStates.Length; i++)
                currentStates[i] = GamePad.GetState((PlayerIndex)i);
        }

        /// <summary>
        /// Whether the given button is pressed.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsPressed(Buttons button, PlayerIndex index)
        {
            return prevStates[(int)index].IsButtonUp(button) && currentStates[(int)index].IsButtonDown(button);
        }
    }
}
