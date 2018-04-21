using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoHacksGame
{
    /// <summary>
    /// Single-Ton object that manages different states of the game. Each state is
    /// stored into a different class and is individually updated and drawn to the screen.
    /// </summary>
    public class GameManager
    {
        private static GameManager inst;
        /// <summary>
        /// Retrieves the single ton instance of the <c>GameManager</c>.
        /// </summary>
        /// <returns></returns>
        public static GameManager GetInstance()
        {
            if (inst == null)
                inst = new GameManager();

            return inst;
        }

        /// <summary>
        /// Creates a new instance of the <c>GameManager</c>.
        /// </summary>
        private GameManager()
        {

        }

        /// <summary>
        /// Updates the <c>GameManager</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the <c>GameManager</c>'s current state to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
