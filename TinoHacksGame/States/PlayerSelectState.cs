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
using TinoHacksGame.States.UserInterface;

namespace TinoHacksGame.States
{
    /// <summary>
    /// The state in which the players select their character.
    /// </summary>
    public class PlayerSelectState : State
    {
        private SpriteFont font;
        private Boolean playerOneAButtonUp;  //lulz
        private Boolean[] characterSelected = new Boolean[4];  //whether or not the player selected a character
        private int[] playerOption = new int[4];  //character select, name select, stage select
        private int[] characterOption = new int[4]; //what character each player displays
        private float[] backButtonHeld = new float[4];  //for going back to main menu
        private float[][] selectTimer = new float[4][];  //player 1 2 3 4, down up left right.
        private Slide[] slides;

        private Texture2D placeHolder, blank;
        /// <summary>
        /// Creates a new instance of the <c>PlayerSelectState</c>.
        /// </summary>
        public PlayerSelectState()
        {
            for (int i = 0; i < selectTimer.Length; i++) selectTimer[i] = new float[4];
            for (int i = 0; i < playerOption.Length; i++) playerOption[i] = -1;
        }

        /// <summary>
        /// Initializes the <c>PlayerSelectState</c> and the content.
        /// </summary>
        /// <param name="Content"></param>
        public override void Initialize(ContentManager Content)
        {
            slides = new Slide[4];
            int border = 32;
            int width = 360;
            blank = Content.Load<Texture2D>("Blank");
            for (int i = 0; i < slides.Length; i++)
            {
                slides[i] = new Slide(Content)
                {
                    Index = (PlayerIndex)i,
                    Position = new Vector2(width * i + (i + 1) * border, border),
                    Texture = blank,
                    Size = new Point(width, 400),
                };
            }

            base.Initialize(Content);
            font = Content.Load<SpriteFont>("Font");
            placeHolder = Content.Load<Texture2D>("Placeholder");
        }

        /// <summary>
        /// Updates the <c>PlayerSelectState</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //if (GamePad.GetState(0).IsButtonUp(Buttons.A)) playerOneAButtonUp = true;

            foreach (Slide s in slides)
                s.Update(gameTime);

            if (InputManager.GetInstance().IsPressed(Buttons.Start, PlayerIndex.One))
            {
                int num = ControllersConnected();
                GameManager.GetInstance().Players = new List<Player>();

                for (int i = 0; i < num; i++)
                {
                    Player p = new Player(null, i)
                    {
                        Texture = placeHolder,
                        Position = new Vector2(100, 0),
                    };
                    GameManager.GetInstance().Players.Add(p);
                }
                GameManager.GetInstance().ChangeScreen(Screens.GAME);
            }

            //for (int i = 0; i < 4; i++) {
            //    GamePadCapabilities capabilities = GamePad.GetCapabilities(i);
            //    if (capabilities.IsConnected) {
                    
            //        GamePadState state = GamePad.GetState(i);
            //        if (playerOption[i] == -1)
            //            playerOption[i] = 0;

            //        //selecting an option
            //        int selectDelay = 200;
            //        if (state.ThumbSticks.Left.Y > 0.75f && selectTimer[i][0] > selectDelay) {
            //            playerOption[i] = Math.Max(0, playerOption[i] - 1);
            //            selectTimer[i][0] = 0f;
            //        }
            //        if (state.ThumbSticks.Left.Y < -0.75f && selectTimer[i][1] > selectDelay) {
            //            playerOption[i] = Math.Min(2, playerOption[i] + 1);
            //            selectTimer[i][1] = 0f;
            //        }


            //        //scrolling through the characters
            //        if (playerOption[i] == 0 && !characterSelected[i])
            //        {
            //            int numChar = 4;

            //            if (state.ThumbSticks.Left.X > 0.75f && selectTimer[i][2] > selectDelay)
            //            {
            //                characterOption[i] = (characterOption[i] + 1) % numChar;
            //                selectTimer[i][2] = 0f;
            //            }

            //            if (state.ThumbSticks.Left.X < -0.75f && selectTimer[i][3] > selectDelay)
            //            {
            //                characterOption[i] = (characterOption[i] - 1 + numChar) % numChar;
            //                selectTimer[i][3] = 0f;
            //            }
            //        }


            //        //goes back to menu
            //        if (state.IsButtonDown(Buttons.Back))
            //        {
            //            if (backButtonHeld[i] > 1000f) GameManager.GetInstance().ChangeScreen(Screens.MENU);
            //            backButtonHeld[i] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //        }
            //        if (state.IsButtonUp(Buttons.Back))
            //        {
            //            backButtonHeld[i] = 0;
            //        }


            //        if (state.IsButtonDown(Buttons.A))
            //        {
            //            //selects a player
            //            if (playerOption[i] == 0 && checkPlayerOneConditions(i))
            //            {
            //                characterSelected[i] = true;
            //                for (int j = 0; j < characterOption.Length; j++)
            //                {
            //                    if (i != j && characterSelected[j] && characterOption[j] == characterOption[i]) characterSelected[i] = false;
            //                }
            //            }
            //            if (playerOption[i] == 1)
            //            {
            //                //change the name
            //            }
            //            if (playerOption[i] == 2)
            //            {
            //                int num = 0;
            //                for (int j = 0; j < 4; j++) if (characterSelected[j]) num++;
            //                if (num == ControllersConnected()) {
            //                    GameManager.GetInstance().Players = new List<Player>();
            //                    for (int j = 0; j < num; j++) {
            //                        Player p = new Player(null, j) {
            //                            Texture = placeHolder,
            //                            Position = new Vector2(100, 0),
            //                        };
            //                        GameManager.GetInstance().Players.Add(p);
            //                    }
            //                    GameManager.GetInstance().ChangeScreen(Screens.GAME);
            //                }
            //            }
            //        }

            //        if (state.IsButtonDown(Buttons.B))
            //        {
            //            //deselects a player
            //            if (playerOption[i] == 0) {
            //                characterSelected[i] = false;
            //            }
            //        }
            //    }
            //    else {
            //        playerOption[i] = -1;
            //        characterOption[i] = 0;
            //        backButtonHeld[i] = 0;
            //        selectTimer[i] = new float[4];
            //    }
            //}
            //for (int i = 0; i < 4; i++) {
            //    for (int j = 0; j < 4; j++) {
            //        selectTimer[i][j] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //    }
            //}
        }

        /// <summary>
        /// Draws the contents of the <c>PlayerSelectState</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="device"></param>
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
        {
            base.Draw(spriteBatch, device);
            spriteBatch.Begin();

            //for (int i = 0; i < 4; i++)
            //{
            //    Color cText = Color.Gray;
            //    if (characterSelected[i] && playerOption[i] == 0) cText = Color.DarkGreen;
            //    else if (characterSelected[i]) cText = Color.Black;
            //    else if (playerOption[i] == 0) cText = Color.LightGreen;
            //    else if (GamePad.GetCapabilities(i).IsConnected) cText = Color.White;

            //    spriteBatch.DrawString(font, "P" + (i + 1) + " Character: " + characterOption[i], new Vector2(400 + (i * 200), 100), cText);
            //    spriteBatch.DrawString(font, "P" + (i + 1) + " Name", new Vector2(400 + (i * 200), 200), 
            //        playerOption[i] == 1 ? Color.Black : (GamePad.GetCapabilities(i).IsConnected ? Color.Wheat : Color.Gray));
            //}
            //spriteBatch.DrawString(font, "Select Stage", new Vector2(700, 300), playerOption[0] == 2 || playerOption[1] == 2 ||
            //    playerOption[2] == 2 || playerOption[0] == 2 ? Color.Black : Color.Wheat);

            foreach (Slide s in slides)
                s.Draw(spriteBatch);

            spriteBatch.Draw(blank, new Rectangle(32, 464, 1536, 404), Color.Red);
            
            spriteBatch.End();
        }

        private int ControllersConnected()
        {
            int num = 0;
            for (int i = 0; i < 4; i++) {
                GamePadCapabilities capabilities = GamePad.GetCapabilities(i);
                if (capabilities.IsConnected) num++;
            }
            return num;
        }

        private Boolean checkPlayerOneConditions(int index) {
            if (index != 0 || playerOneAButtonUp) return true;
            return false;
        }
    }
}
