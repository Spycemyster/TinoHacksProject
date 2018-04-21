using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoHacksGame.Sprites;

namespace TinoHacksGame.States.UserInterface
{
    /// <summary>
    /// A selective cursor for the game.
    /// </summary>
    public class Cursor : Sprite
    {
        /// <summary>
        /// The player that is controlling the <c>Cursor</c>.
        /// </summary>
        public PlayerIndex Index;

        /// <summary>
        /// Creates a new instance of <c>Cursor</c>.
        /// </summary>
        public Cursor(PlayerIndex index) : base(null)
        {
            Index = index;
        }

        /// <summary>
        /// Updates the <c>Cursor</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Position += GamePad.GetState((int)Index).ThumbSticks.Left;
        }

        /// <summary>
        /// Draws the <c>Cursor</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Origin = new Vector2(0, 0);
            spriteBatch.Draw(Texture, GetDrawRectangle(), Color);
        }
    }
}
