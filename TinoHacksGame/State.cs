using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoHacksGame
{
    /// <summary>
    /// A game state that's updated and drawn within the <c>GameManager</c>.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Creates a new instance of a <c>State</c>.
        /// </summary>
        public State()
        {

        }

        /// <summary>
        /// Initializes the <c>State</c>.
        /// </summary>
        /// <param name="Content"></param>
        public virtual void Initialize(ContentManager Content)
        {

        }

        /// <summary>
        /// Updates the <c>State</c>'s conditional checking and logic.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the <c>State</c>.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="device"></param>
        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
        {

        }
    }
}
