using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoHacksGame.Sprites
{
    /// <summary>
    /// An object that holds textures and positions.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// The position point of the <c>Sprite</c>.
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// The texture of the <c>Sprite</c>.
        /// </summary>
        public Texture2D Texture
        {
            get;
            set;
        }

        /// <summary>
        /// The opacity that the texture is drawn at.
        /// </summary>
        public float Opacity
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of a <c>Sprite</c>.
        /// </summary>
        public Sprite()
        {

        }

        /// <summary>
        /// Updates the <c>Sprite</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the <c>Sprite</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
