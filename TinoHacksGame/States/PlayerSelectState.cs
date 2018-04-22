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
        public Slide[] slides;
        private Texture2D[] logos;
        private int stage;
        private Song song;

        private Texture2D placeHolder, blank;
        private Texture2D[] backgroundColleges;
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
            song = Content.Load<Song>("characterSelect");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            stage = 0;
            slides = new Slide[4];
            int border = 32;
            int width = 360;
            blank = Content.Load<Texture2D>("Blank");
            int num = ControllersConnected();
            backgroundColleges = new Texture2D[4];
            backgroundColleges[0] = Content.Load<Texture2D>("jhu");
            backgroundColleges[1] = Content.Load<Texture2D>("cambridge");
            backgroundColleges[2] = Content.Load<Texture2D>("deanza");
            backgroundColleges[3] = Content.Load<Texture2D>("stanford");
            logos = new Texture2D[4];
            logos[0] = Content.Load<Texture2D>("John Hopkins Logo");
            logos[1] = Content.Load<Texture2D>("Cambridge Logo");
            logos[2] = Content.Load<Texture2D>("De Anza Logo");
            logos[3] = Content.Load<Texture2D>("Stanford Logo");
            for (int i = 0; i < slides.Length; i++)
            {
                slides[i] = new Slide(this, Content)
                {
                    Index = (PlayerIndex)i,
                    Position = new Vector2(width * i + (i + 1) * border, border),
                    Texture = blank,
                    Size = new Point(width, 400),
                    Select = i,
                };

                if (i + 1 <= num)
                    slides[i].IsActive = true;
            }

            base.Initialize(Content);
            font = Content.Load<SpriteFont>("Font");
            placeHolder = Content.Load<Texture2D>("Placeholder");

            // John Hopkins
            Platform plat = new Platform(null)
            {
                Texture = blank,
                Position = new Vector2(-50, 875),
                Size = new Point(1700, 100),
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
            Vector2[] spawn0 = new Vector2[4];
            spawn0[0] = new Vector2(50, 400);
            spawn0[1] = new Vector2(1550, 400);
            spawn0[2] = new Vector2(300, 400);
            spawn0[3] = new Vector2(1300, 400);
            Stages.Add(new Stage(platforms, backgroundColleges[0], blank, spawn0));

            // Cambridge

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
            Vector2[] spawn1 = new Vector2[4];
            spawn1[0] = new Vector2(175, 200);
            spawn1[1] = new Vector2(1425, 200);
            spawn1[2] = new Vector2(500, 600);
            spawn1[3] = new Vector2(1300, 600);

            List<Platform> platforms2 = new List<Platform>
            {
                plat5,
                plat4,
                plat3,
            };

            Stages.Add(new Stage(platforms2, backgroundColleges[1], blank, spawn1));

            // De Anza

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
            Vector2[] spawn2 = new Vector2[4];
            spawn2[0] = new Vector2(30, 300);
            spawn2[1] = new Vector2(1570, 300);
            spawn2[2] = new Vector2(100, 300);
            spawn2[3] = new Vector2(1500, 300);

            Stages.Add(new Stage(platforms3, backgroundColleges[2], blank, spawn2));

            // Stanford

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
            Vector2[] spawn3 = new Vector2[4];
            spawn3[0] = new Vector2(100, 200);
            spawn3[1] = new Vector2(1500, 200);
            spawn3[2] = new Vector2(100, 800);
            spawn3[3] = new Vector2(1500, 800);

            Stages.Add(new Stage(platforms4, backgroundColleges[3], blank, spawn3));

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
            spriteBatch.Draw(backgroundColleges[stage], new Rectangle(0, 0, 1600, 900), Color.White);
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
