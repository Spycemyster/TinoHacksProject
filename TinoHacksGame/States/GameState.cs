using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TinoHacksGame.States
{
    /// <summary>
    /// The <c>State</c> in which most of the gameplay will take
    /// place in.
    /// </summary>
    public class GameState : State
    {
        /// <summary>
        /// The gravitational constant for the objects.
        /// </summary>
        public const float GRAVITY = 3f;

        /// <summary>
        /// Creates a new instance of <c>GameState</c>.
        /// </summary>
        public GameState()
        {

        }

        /// <summary>
        /// Initializes the <c>GameState</c>.
        /// </summary>
        /// <param name="Content"></param>
        public override void Initialize(ContentManager Content)
        {
            base.Initialize(Content);
        }

        /// <summary>
        /// Updates the <c>GameState</c>'s conditional checking and logic.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the <c>GameState</c>'s contents to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="device"></param>
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
        {
            base.Draw(spriteBatch, device);
        }
    }
}
