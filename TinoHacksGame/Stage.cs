using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoHacksGame.Sprites;

namespace TinoHacksGame
{
    /// <summary>
    /// A stage within the game instance.
    /// </summary>
    public class Stage
    {
        private Texture2D college;

        /// <summary>
        /// The platforms on the stage.
        /// </summary>
        public List<Platform> Platforms;

        /// <summary>
        /// Creates a new instance of a <c>Stage</c>.
        /// </summary>
        /// <param name="platforms"></param>
        /// <param name="college"></param>
        public Stage(List<Platform> platforms, Texture2D college)
        {
            Platforms = platforms;
            this.college = college;
        }

        /// <summary>
        /// Updates the <c>Stage</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the <c>Stage</c> and all its contents to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(college, new Rectangle(0, 0, 1600, 900), Color.White);

            foreach(Platform p in Platforms)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}
