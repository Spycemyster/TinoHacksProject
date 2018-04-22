using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoHacksGame.Sprites;

namespace TinoHacksGame
{
    /// <summary>
    /// A stage within the game instance.
    /// </summary>
    public class Stage
    {
        private Texture2D college;

        /// <summary>
        /// The platforms on the stage.
        /// </summary>
        public List<Platform> Platforms;

        /// <summary>
        /// The zone that inflicts damage to the <c>Player</c> if residing within it.
        /// </summary>
        public Rectangle DangerZone
        {
            get;
            private set;
        }

        private Texture2D blank;
        private bool isLeft;
        private int width;
        private float opacity;
        private SpriteFont font;

        private float countDownTimer, timerMax;
        private Random rand;

        /// <summary>
        /// Creates a new instance of a <c>Stage</c>.
        /// </summary>
        /// <param name="platforms"></param>
        /// <param name="college"></param>
        /// <param name="blank"></param>
        public Stage(List<Platform> platforms, Texture2D college, Texture2D blank)
        {
            rand = new Random();
            font = GameManager.GetInstance().Content.Load<SpriteFont>("Font");
            this.blank = blank;
            width = 800;
            DangerZone = new Rectangle(0, 0, width, 900);
            timerMax = 5000f;
            Platforms = platforms;
            isLeft = true;
            this.college = college;
        }

        /// <summary>
        /// Updates the <c>Stage</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            countDownTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            opacity = (float)Math.Sin(countDownTimer / (500f)) * 0.5f + 0.5f;

            if (countDownTimer >= timerMax)
            {
                isLeft = !isLeft;
                timerMax /= 1.05f;
                width = Math.Min((int)(width * 1.05), 1400);
                countDownTimer = 0f;
                CheckDamageEntity();
            }

            if (isLeft)
            {
                DangerZone = new Rectangle(0, 0, width, 900);
            }
            else
                DangerZone = new Rectangle(1600 - width, 0, width, 900);
        }

        /// <summary>
        /// Checks for entities within the Danger Zone and inflicts damage accordingly.
        /// </summary>
        public void CheckDamageEntity()
        {
            foreach (Player p in GameManager.GetInstance().Players)
            {
                if (p.GetDrawRectangle().Intersects(DangerZone))
                {
                    if (isLeft)
                    {
                        p.getHit(100, new Vector2(rand.Next(0, 2), rand.Next(0, 2)), 0f);
                    }
                    else
                        p.getHit(100, new Vector2(rand.Next(-2, 0), rand.Next(0, 2)), 0f);
                }
            }
        }

        /// <summary>
        /// Draws the <c>Stage</c> and all its contents to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(college, new Rectangle(0, 0, 1600, 900), Color.White);
            Rectangle timerRect = new Rectangle(0, 0, (int)(1600 - 1600 * (countDownTimer / timerMax)), 32);

            foreach(Platform p in Platforms)
                p.Draw(spriteBatch);

            spriteBatch.Draw(blank, DangerZone, Color.Red * (opacity + 0.2f) * 0.3f);
            spriteBatch.Draw(blank, DangerZone, Color.White * (countDownTimer / (timerMax - 1000)));
            spriteBatch.Draw(blank, timerRect, Color.DarkRed);
            string text = "" + (int)((timerMax - countDownTimer) / 1000f);
            Vector2 pos = new Vector2(800 - font.MeasureString(text).X / 2, 32f*0);
            spriteBatch.DrawString(font, text, pos, Color.Blue, 0f, font.MeasureString(text) / 2, 2f, SpriteEffects.None, 0f);
        }
    }
}
