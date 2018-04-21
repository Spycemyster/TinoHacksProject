using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoHacksGame.States;

namespace TinoHacksGame.Sprites
{
    /// <summary>
    /// A collidable platform
    /// </summary>
    public class Platform : Sprite
    {

        /// <summary>
        /// Creates a new instance of a <c>Platform</c>.
        /// </summary>
        public Platform(GameState state) 
            : base(state)
        {
            Size = new Point(100, 10);
        }

        /// <summary>
        /// Draws the platform to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Origin = Texture.Bounds.Size.ToVector2() / 2;

            //spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Origin, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Texture, GetDrawRectangle(), Color.White);
        }
    }
}
