using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private Texture2D backgroundPic;
        private UIButton[] button = new UIButton[4];
        private Song song;

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
            Texture2D cursorTexture = Content.Load<Texture2D>("Cursor");
            song = Content.Load<Song>("stageSelect");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            cursor = new Cursor(PlayerIndex.One)
            {
                Texture = cursorTexture,
                Size = new Point(cursorTexture.Width, cursorTexture.Height),
                Position = new Vector2(800, 250),
            };
            Texture2D blank = Content.Load<Texture2D>("Blank");
            backgroundPic = Content.Load<Texture2D>("StartScreen");

            button[0] = new UIButton(cursor)
            {
                Position = new Vector2(650, 450),
                Size = new Point(300, 50),
                Text = "Local",
                Texture = blank,
                Font = font,
                Color = Color.Green,
                Scale = 1f,
            };

            button[1] = new UIButton(cursor)
            {
                Position = new Vector2(650, 550),
                Size = new Point(300, 50),
                Text = "Online",
                Texture = blank,
                Font = font,
                Color = Color.Green,
                Scale = 1f
            };

            button[2] = new UIButton(cursor)
            {
                Position = new Vector2(650, 660),
                Size = new Point(300, 50),
                Text = "Settings",
                Texture = blank,
                Font = font,
                Scale = 1f,
                Color = Color.Green
            };

            button[3] = new UIButton(cursor)
            {
                Position = new Vector2(650, 770),
                Size = new Point(300, 50),
                Text = "Exit",
                Texture = blank,
                Font = font,
                Scale = 1f,
                Color = Color.Green,
            };
            button[0].OnPress += OnPress;
            button[1].OnPress += OnPress;
            button[2].OnPress += OnPress;
            button[3].OnPress += OnPress;
        }

        /// <summary>
        /// Invoked whenever a button is pressed.
        /// </summary>
        /// <param name="arg"></param>
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
            else if (arg.Component == button[3])
            {
                GameManager.GetInstance().Exit();
            }
        }

        /// <summary>
        /// Updates the logic and conditional checking for the <c>MenuState</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            cursor.Update(gameTime);

            if (InputManager.GetInstance().IsPressed(Buttons.B, PlayerIndex.One))
                GameManager.GetInstance().ChangeScreen(Screens.TITLE);

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
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
        {
            base.Draw(spriteBatch, device);
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundPic, new Rectangle(0, 0, 1600, 900), Color.White);
            foreach (UIButton b in button)
                b.Draw(spriteBatch);
            cursor.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
