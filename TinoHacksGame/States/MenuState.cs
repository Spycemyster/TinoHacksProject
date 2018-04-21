using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TinoHacksGame.States.UserInterface;

namespace TinoHacksGame.States {
    /// <summary>
    /// The <c>State</c> for where the player will select menu options.
    /// </summary>
    public class MenuState : State {
        private SpriteFont font;
        private int option = 0;
        private float selectTimerDown = 0f;
        private float selectTimerUp = 0f;
        private Cursor cursor;
        private UIButton[] button = new UIButton[3];

        /// <summary>
        /// Creates a new instance of <c>MenuState</c>.
        /// </summary>
        public MenuState()
        {
        }

        /// <summary>
        /// Initializes the <c>MenuState</c>.
        /// </summary>
        /// <param name="Content"></param>
        public override void Initialize(ContentManager Content) {
            base.Initialize(Content);
            font = Content.Load<SpriteFont>("Font");
            cursor = new Cursor(PlayerIndex.One);
            cursor.Texture = Content.Load<Texture2D>("Cursor");
            cursor.Size = new Point(cursor.Texture.Width, cursor.Texture.Height);
            Texture2D blank = Content.Load<Texture2D>("Blank");

            button[0] = new UIButton(cursor);
            button[0].Position = new Vector2(400, 100);
            button[0].Size = new Point(150, 25);
            button[0].Text = "Local";
            button[0].Texture = blank;
            button[0].Font = font;
            button[0].Color = Color.Green;
            button[0].OnPress += OnPress;

            button[1] = new UIButton(cursor);
            button[1].Position = new Vector2(400, 200);
            button[1].Size = new Point(150, 25);
            button[1].Text = "Online";
            button[1].Texture = blank;
            button[1].Font = font;
            button[1].Color = Color.Green;
            button[1].OnPress += OnPress;

            button[2] = new UIButton(cursor);
            button[2].Position = new Vector2(400, 300);
            button[2].Size = new Point(150, 25);
            button[2].Text = "Settings";
            button[2].Texture = blank;
            button[2].Font = font;
            button[2].Color = Color.Green;
            button[2].OnPress += OnPress;
        }

        private void OnPress(UIArg arg)
        {
            if (arg.Component == button[0])
            {
                GameManager.GetInstance().ChangeScreen(Screens.PLAYERSELECT);
            }
            else if (arg.Component == button[1])
            {
                GameManager.GetInstance().ChangeScreen(Screens.ONLINELOBBY);
            }
            else if (arg.Component == button[2])
            {
                GameManager.GetInstance().ChangeScreen(Screens.SETTINGS);
            }
        }

        /// <summary>
        /// Updates the logic and conditional checking for the <c>MenuState</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            //GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            cursor.Update(gameTime);

            foreach (UIButton b in button)
                b.Update(gameTime);

            /*
             * I'm sorry Ryan ;(
             *  - Spencer
             */
            //int selectDelay = 200;
            //if (capabilities.IsConnected) {
            //    GamePadState state = GamePad.GetState(PlayerIndex.One);
            //    if (state.ThumbSticks.Left.Y > 0.75f && selectTimerDown > selectDelay) {
            //        option = Math.Max(0, option - 1);
            //        selectTimerDown = 0f;
            //    }
            //    if (state.ThumbSticks.Left.Y < -0.75f && selectTimerUp > selectDelay) {
            //        option = Math.Min(2, option + 1);
            //        selectTimerUp = 0f;
            //    }

            //    if (state.IsButtonDown(Buttons.A)) {
            //        Screens[] screens = { Screens.PLAYERSELECT, Screens.ONLINELOBBY, Screens.SETTINGS };
            //        GameManager.GetInstance().ChangeScreen(screens[option]);
            //    }
            //}
            //selectTimerDown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //selectTimerUp += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Draws the contents of the <c>MenuState</c>.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="device"></param>
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device) {
            base.Draw(spriteBatch, device);
            spriteBatch.Begin();
            foreach (UIButton b in button)
                b.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "Local", new Vector2(400, 100), option == 0 ? Color.Black : Color.Wheat);
            //spriteBatch.DrawString(font, "Online", new Vector2(400, 200), option == 1 ? Color.Black : Color.Wheat);
            //spriteBatch.DrawString(font, "Settings", new Vector2(400, 300), option == 2 ? Color.Black : Color.Wheat);
            cursor.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
