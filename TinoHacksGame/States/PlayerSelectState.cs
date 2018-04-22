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
        private Texture2D backgroundCollege;
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
            backgroundCollege = Content.Load<Texture2D>("jhu");
        }

        /// <summary>
        /// Updates the <c>PlayerSelectState</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Slide s in slides) s.Update(gameTime);
            
            for (int i = 0; i < 4; i++) {
                GamePadCapabilities capabilities = GamePad.GetCapabilities(i);
                if (capabilities.IsConnected) {
                    GamePadState state = GamePad.GetState(i);
                    //goes back to menu
                    if (state.IsButtonDown(Buttons.Back)) {
                        if (backButtonHeld[i] > 1000f) GameManager.GetInstance().ChangeScreen(Screens.MENU);
                        backButtonHeld[i] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    if (state.IsButtonUp(Buttons.Back)) {
                        backButtonHeld[i] = 0;
                    }
                }
            }

            if (InputManager.GetInstance().IsPressed(Buttons.Start, PlayerIndex.One)) {
                int num = ControllersConnected();
                GameManager.GetInstance().Players = new List<Player>();

                //stagemaking START
                for (int i = 0; i < num; i++) {
                    Player p = new Player(null, i) {
                        Texture = placeHolder,
                        Position = new Vector2(100, 0),
                    };
                    GameManager.GetInstance().Players.Add(p);
                }

                Platform plat = new Platform(null) {
                    Texture = blank,
                    Position = new Vector2(50, 850),
                    Size = new Point(1000, 30),
                };

                Platform plat2 = new Platform(null) {
                    Texture = blank,
                    Position = new Vector2(300, 600),
                    Size = new Point(300, 20),
                };
                List<Platform> platforms = new List<Platform>();
                platforms.Add(plat2);
                platforms.Add(plat);
                GameManager.GetInstance().stage = new Stage(platforms, backgroundCollege);
                //stagemaking END

                GameManager.GetInstance().ChangeScreen(Screens.GAME);
            }
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
            foreach (Slide s in slides) s.Draw(spriteBatch);
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
