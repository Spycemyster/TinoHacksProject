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
        private List<Stage> Stages = new List<Stage>();
        private SpriteFont font;
        private Boolean playerOneAButtonUp;  //lulz
        private Boolean[] characterSelected = new Boolean[4];  //whether or not the player selected a character
        private int[] playerOption = new int[4];  //character select, name select, stage select
        private int[] characterOption = new int[4]; //what character each player displays
        private float[] backButtonHeld = new float[4];  //for going back to main menu
        private float[][] selectTimer = new float[4][];  //player 1 2 3 4, down up left right.
        private Slide[] slides;
        private Texture2D[] logos;
        private int stage;

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
            stage = 0;
            slides = new Slide[4];
            int border = 32;
            int width = 360;
            blank = Content.Load<Texture2D>("Blank");
            int num = ControllersConnected();
            logos = new Texture2D[4];
            logos[0] = Content.Load<Texture2D>("John Hopkins Logo");
            logos[1] = Content.Load<Texture2D>("Cambridge Logo");
            logos[2] = Content.Load<Texture2D>("De Anza Logo");
            logos[3] = Content.Load<Texture2D>("Stanford Logo");
            for (int i = 0; i < slides.Length; i++)
            {
                slides[i] = new Slide(Content)
                {
                    Index = (PlayerIndex)i,
                    Position = new Vector2(width * i + (i + 1) * border, border),
                    Texture = blank,
                    Size = new Point(width, 400),
                };

                if (i + 1 <= num)
                    slides[i].IsActive = true;
            }

            base.Initialize(Content);
            font = Content.Load<SpriteFont>("Font");
            placeHolder = Content.Load<Texture2D>("Placeholder");

            // John Hopkins
            backgroundCollege = Content.Load<Texture2D>("jhu");

            Platform plat = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(-150, 875),
                Size = new Point(1900, 100),
                Scale = 1f,
                Color = Color.LawnGreen,
            };

            Platform plat2 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(350, 550),
                Size = new Point(700, 34),
                Scale = 1f,
                Color = Color.Green,
            };
            List<Platform> platforms = new List<Platform>
            {
                plat2,
                plat
            };
            Stages.Add(new Stage(platforms, backgroundCollege, blank));

            // Cambridge
            backgroundCollege = Content.Load<Texture2D>("cambridge");

            Platform plat3 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(0, 800),
                Size = new Point(400, 150),
                Scale = 1f,
                Color = Color.LightSkyBlue,
            };
            Platform plat4 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(1200, 800),
                Size = new Point(400, 150),
                Scale = 1f,
                Color = Color.LightSkyBlue,
            };
            Platform plat5 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(300, 490),
                Size = new Point(900, 50),
                Scale = 1f,
                Color = Color.LightSkyBlue,
            };

            List<Platform> platforms2 = new List<Platform>
            {
                plat5,
                plat4,
                plat3,
            };

            Stages.Add(new Stage(platforms2, backgroundCollege, blank));

            // De Anza
            backgroundCollege = Content.Load<Texture2D>("deanza");

            Platform plat6 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(0, 800),
                Size = new Point(1600, 300),
                Scale = 1f,
                Color = Color.Brown,
            };
            List<Platform> platforms3 = new List<Platform>();
            platforms3.Add(plat6);

            Stages.Add(new Stage(platforms3, backgroundCollege, blank));

            // Stanford
            backgroundCollege = Content.Load<Texture2D>("stanford");

            Platform plat7 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(0, 800),
                Size = new Point(500, 300),
                Scale = 1f,
                Color = Color.RosyBrown,
            };
            Platform plat8 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(1100, 800),
                Size = new Point(500, 300),
                Scale = 1f,
                Color = Color.RosyBrown,
            };
            Platform plat9 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(700, 800),
                Size = new Point(200, 300),
                Scale = 1f,
                Color = Color.RosyBrown,
            };
            Platform plat10 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(0, 550),
                Size = new Point(400, 30),
                Scale = 1f,
                Color = Color.ForestGreen,
            };
            Platform plat11 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(1200, 550),
                Size = new Point(400, 30),
                Scale = 1f,
                Color = Color.ForestGreen,
            };

            Platform plat12 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(1200, 300),
                Size = new Point(400, 30),
                Scale = 1f,
                Color = Color.BlueViolet,
            };

            Platform plat13 = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(0, 300),
                Size = new Point(400, 30),
                Scale = 1f,
                Color = Color.BlueViolet,
            };

            List<Platform> platforms4 = new List<Platform>();
            platforms4.Add(plat7);
            platforms4.Add(plat8);
            platforms4.Add(plat9);
            platforms4.Add(plat10);
            platforms4.Add(plat11);
            platforms4.Add(plat12);
            platforms4.Add(plat13);

            Stages.Add(new Stage(platforms4, backgroundCollege, blank));

            GameManager.GetInstance().stage = Stages[3];
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

            if (InputManager.GetInstance().IsPressed(Buttons.DPadLeft, PlayerIndex.One))
            {
                stage--;
                if (stage < 0)
                    stage = 3;
            }
            else if (InputManager.GetInstance().IsPressed(Buttons.DPadRight, PlayerIndex.One))
            {
                stage++;
                if (stage > 3)
                    stage = 0;
            }

            if (InputManager.GetInstance().IsPressed(Buttons.Start, PlayerIndex.One)) {
                int num = ControllersConnected();
                GameManager.GetInstance().Players = new List<Player>();

                //stagemaking START
                GameManager.GetInstance().stage = Stages[stage];
                for (int i = 0; i < num; i++) {
                    Player p = new Player(null, i, slides[i].Color) {
                        Texture = placeHolder,
                        Position = new Vector2(100, 0),
                    };
                    GameManager.GetInstance().Players.Add(p);
                }
                
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
            spriteBatch.Draw(backgroundCollege, new Rectangle(0, 0, 1600, 900), Color.White);
            foreach (Slide s in slides) s.Draw(spriteBatch);
            spriteBatch.Draw(blank, new Rectangle(32, 464, 1536, 404), Color.White);
            spriteBatch.Draw(logos[stage], new Rectangle(32, 464, 1536, 404), Color.White);
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
