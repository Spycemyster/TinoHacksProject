using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinoHacksGame.States.UserInterface
{
    /// <summary>
    /// A pressable button for the ui component.
    /// </summary>
    public class UIButton : UIComponent
    {
        /// <summary>
        /// The font of the text.
        /// </summary>
        public SpriteFont Font
        {
            get;
            set;
        }

        private float timer;

        /// <summary>
        /// Creates a new instance of a <c>UIButton</c>.
        /// </summary>
        public UIButton(Cursor cursor) : base(cursor)
        {

        }

        /// <summary>
        /// Updates the <c>UIButton</c>'s logic and conditional checking
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsSelected)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Opacity = (float)(Math.Cos(timer / 500f) / 2f + 0.5f);
            }
            else
            {
                timer = 0f;
                Opacity = Math.Min(1f, Opacity + 0.01f);
            }
        }

        /// <summary>
        /// Draws the <c>UIButton</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Rectangle dr = GetDrawRectangle();
            spriteBatch.Draw(Texture, dr, Color * Opacity);
            Vector2 pos = new Vector2(dr.X + dr.Width / 2 - Font.MeasureString(Text).X / 2,
                dr.Y + dr.Height / 2 - Font.MeasureString(Text).Y / 2);
            spriteBatch.DrawString(Font, Text, pos + new Vector2(1, 1), Color.Black * 0.3f);
            spriteBatch.DrawString(Font, Text, pos, Color.White);
        }
    }
}
