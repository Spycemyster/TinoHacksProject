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

namespace TinoHacksGame.States {
    /// <summary>
    /// The <c>State</c> in which most of the gameplay will take
    /// place in.
    /// </summary>
    public class GameState : State {
        /// <summary>
        /// The gravitational constant for the objects.
        /// </summary>
        public const float GRAVITY = 0.03924f;

        /// <summary>
        /// The scale that the sprites are drawn at.
        /// </summary>
        public const float SCALE = 2f;

        /// <summary>
        /// The players in the game state.
        /// </summary>
        public List<Player> Players {
            get;
            private set;
        }

        public Stage currentStage;
        private Texture2D blank;
        private SpriteFont font;
        private bool gameOver = false;
        /// <summary>
        /// Creates a new instance of <c>GameState</c>.
        /// </summary>
        public GameState() {
            Players = GameManager.GetInstance().Players;
            currentStage = GameManager.GetInstance().stage;
            GameManager.GetInstance().hitBoxes = new List<HitBox>();
        }

        /// <summary>
        /// Initializes the <c>GameState</c>.
        /// </summary>
        /// <param name="Content"></param>
        public override void Initialize(ContentManager Content) {
            base.Initialize(Content);

            Texture2D placeHolder = Content.Load<Texture2D>("Placeholder");
            font = Content.Load<SpriteFont>("Font");
            blank = Content.Load<Texture2D>("Blank");
            foreach (Player p in Players) p.state = this;
        }

        /// <summary>
        /// Updates the <c>GameState</c>'s conditional checking and logic.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            for (int i = 0; i < Players.Count; i++) {
                Players[i].Update(gameTime);
                if (Players[i].lives <= 0) {
                    GameManager.GetInstance().Players.RemoveAt(i);
                    i--;
                }
                if (Players.Count <= 1) gameOver = true;
            }

            if (!gameOver) {
                foreach (Platform p in currentStage.Platforms) p.Update(gameTime);
                for (int i = 0; i < GameManager.GetInstance().hitBoxes.Count; i++) {
                    if (GameManager.GetInstance().hitBoxes[i].isActive()) GameManager.GetInstance().hitBoxes[i].Update(gameTime);
                    else {
                        GameManager.GetInstance().hitBoxes.RemoveAt(i);
                        i--;
                    }
                }
                GameManager.GetInstance().stage.Update(gameTime);
            }
            else {
                if (InputManager.GetInstance().IsPressed(Buttons.Start, PlayerIndex.One) || InputManager.GetInstance().IsPressed(Buttons.Start, PlayerIndex.Two) ||
                (InputManager.GetInstance().IsPressed(Buttons.Start, PlayerIndex.Three) || InputManager.GetInstance().IsPressed(Buttons.Start, PlayerIndex.Four)))
                    GameManager.GetInstance().ChangeScreen(Screens.MENU);
            }
            if (InputManager.GetInstance().IsPressed(Buttons.Back, PlayerIndex.One) || InputManager.GetInstance().IsPressed(Buttons.Back, PlayerIndex.Two) ||
                (InputManager.GetInstance().IsPressed(Buttons.Back, PlayerIndex.Three) || InputManager.GetInstance().IsPressed(Buttons.Back, PlayerIndex.Four)))
                GameManager.GetInstance().ChangeScreen(Screens.MENU);
        }

        /// <summary>
        /// Draws the <c>GameState</c>'s contents to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="device"></param>
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device) {
            base.Draw(spriteBatch, device);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            GameManager.GetInstance().stage.Draw(spriteBatch);
            foreach (Player p in Players)
                p.Draw(spriteBatch);
            foreach (Platform p in currentStage.Platforms)
                p.Draw(spriteBatch);
            foreach (HitBox h in GameManager.GetInstance().hitBoxes)
                h.Draw(spriteBatch);

            int border = 32;
            int width = Players.Count * 300 + (Players.Count) * border;

            for (int i = 0; i < Players.Count; i++) {
                Point position = new Point((int)(i * 300 + (i + 0.5) * border + 800 - width / 2), 690);
                Rectangle drawRect = new Rectangle(position, new Point(300, 200));
                spriteBatch.Draw(blank, drawRect, Players[i].Color * 0.3f);

                string text = "%" + (int)Players[i].percentage;
                Vector2 pos = new Vector2(position.X + 150 - font.MeasureString(text).X, position.Y);
                spriteBatch.DrawString(font, text, pos + new Vector2(2, 2), Color.Black * 0.3f, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, text, pos, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }

            if (gameOver) {
                string text = "GAME!";
                spriteBatch.DrawString(font, text, new Vector2(800 - font.MeasureString(text).X * 2, 400) + new Vector2(2, 2), Color.Black, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, text, new Vector2(800 - font.MeasureString(text).X * 2, 400), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
                text = "Press Start to return to the title.";
                spriteBatch.DrawString(font, text, new Vector2(800 - font.MeasureString(text).X / 2, 800) + new Vector2(2, 2), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, text, new Vector2(800 - font.MeasureString(text).X / 2, 800), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }
    }
}
