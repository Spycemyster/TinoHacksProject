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

        /// <summary>
        /// The state of which the slide is done selecting.
        /// </summary>
        public bool FinishedSelecting
        {
            get;
            set;
        }

        /// <summary>
        /// The activeness of the player controlling the <c>Slide</c>.
        /// </summary>
        public bool IsActive
        {
            get;set;
        }

        private int select;
        private float timer;
        private GamePadState prevState;
        private Texture2D blank, mugshot;

        /// <summary>
        /// Creates a new instance of <c>UISlide</c>
        /// </summary>
        public Slide(ContentManager Content) : base(null)
        {
            FinishedSelecting = false;
            Index = PlayerIndex.One;
            Origin = Vector2.Zero;
            Scale = 1f;
            blank = Content.Load<Texture2D>("Blank");
            mugshot = Content.Load<Texture2D>("Profile");
            IsActive = false;
        }

        /// <summary>
        /// Updates the logic and conditional checking for the <c>UISlide</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            GamePadState currentState = GamePad.GetState(Index);
            if (InputManager.GetInstance().IsPressed(Buttons.A, Index))
                FinishedSelecting = !FinishedSelecting;

            if (InputManager.GetInstance().IsPressed(Buttons.B, Index))
            {
                if (!FinishedSelecting)
                    GameManager.GetInstance().ChangeScreen(Screens.MENU);
                else
                    FinishedSelecting = false;
            }
            //if (currentState.ThumbSticks.Left.Y > 0 && timer > time)
            if (!FinishedSelecting && InputManager.GetInstance().IsPressed(Buttons.RightShoulder, Index))
            {
                select++;

                if (select > 8)
                    select = 0;

                timer = 0f;
            }
            else if (!FinishedSelecting && InputManager.GetInstance().IsPressed(Buttons.LeftShoulder, Index))
            //else if (currentState.ThumbSticks.Left.Y < 0 && timer > time)
            {
                select--;

                if (select < 0)
                    select = 8;

                timer = 0f;
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

            if (IsActive)
            {
                switch (select)
                {
                    case 0:
                        Color = Color.Red;
                        break;
                    case 1:
                        Color = Color.Orange;
                        break;
                    case 2:
                        Color = Color.Yellow;
                        break;
                    case 3:
                        Color = Color.Green;
                        break;
                    case 4:
                        Color = Color.Blue;
                        break;
                    case 5:
                        Color = Color.Purple;
                        break;
                    case 6:
                        Color = Color.White;
                        break;
                    case 7:
                        Color = Color.Brown;
                        break;
                    case 8:
                        Color = Color.Black;
                        break;
                }

                spriteBatch.Draw(Texture, GetDrawRectangle(), Color.White);
                spriteBatch.Draw(blank, GetDrawRectangle(), Color);
                spriteBatch.Draw(mugshot, GetDrawRectangle(), Color.White);
                if (FinishedSelecting)
                {
                    spriteBatch.Draw(blank, GetDrawRectangle(), Color.White * 0.5f);
                }
            }
            else
            {
                spriteBatch.Draw(mugshot, GetDrawRectangle(), Color.White * 0.5f);
                spriteBatch.Draw(blank, GetDrawRectangle(), Color.DarkGray * 0.7f);
            }
        }
    }
}
