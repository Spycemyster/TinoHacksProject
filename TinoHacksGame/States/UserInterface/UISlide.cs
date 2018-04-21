using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TinoHacksGame.Sprites;

namespace TinoHacksGame.States.UserInterface
{
    /// <summary>
    /// A slide for selecting characters.
    /// </summary>
    public class Slide : Sprite
    {
        /// <summary>
        /// The player controlling this <c>Slide</c>.
        /// </summary>
        public PlayerIndex Index
        {
            get;
            set;

        }
        private int select;
        private GamePadState prevState;
        private Texture2D[] textures;

        /// <summary>
        /// Creates a new instance of <c>UISlide</c>
        /// </summary>
        public Slide(ContentManager Content) : base(null)
        {
            Index = PlayerIndex.One;
            textures = new Texture2D[4];
            textures[0] = Content.Load<Texture2D>("");
            textures[1] = Content.Load<Texture2D>("");
            textures[2] = Content.Load<Texture2D>("");
            textures[3] = Content.Load<Texture2D>("");
        }

        /// <summary>
        /// Updates the logic and conditional checking for the <c>UISlide</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GamePadState currentState = GamePad.GetState(Index);

            if (currentState.ThumbSticks.Left.Y > 0)
            {
                select++;

                if (select > 3)
                    select = 0;
            }
            else if (currentState.ThumbSticks.Left.Y < 0)
            {
                select--;

                if (select < 0)
                    select = 3;
            }

            prevState = GamePad.GetState(Index);
        }

        /// <summary>
        /// Draws the <c>Slide</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(textures[select], GetDrawRectangle(), Color.White);
        }
    }
}
