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

namespace TinoHacksGame.States
{
    class TitleState : State
    {
        private double timer;
        private float opacity;
        private Texture2D texture, blank;

        /// <summary>
        /// Creates a new instance of <c>TitleState</c>.
        /// </summary>
        public TitleState()
        {

        }

        /// <summary>
        /// Initializes the texture of the <c>TitleState</c>.
        /// </summary>
        /// <param name="Content"></param>
        public override void Initialize(ContentManager Content)
        {
            base.Initialize(Content);

            texture = Content.Load<Texture2D>("StartScreen");
            blank = Content.Load<Texture2D>("Blank");
            Song song = Content.Load<Song>("title");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            opacity = (float)(Math.Sin(timer / 1000f) / 2f + 0.5f) * 0.8f;

            if (InputManager.GetInstance().IsPressed(Buttons.A, PlayerIndex.One))
                GameManager.GetInstance().ChangeScreen(Screens.MENU);
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
        {
            base.Draw(spriteBatch, device);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle(0, 0, 1600, 900), Color.White);
            spriteBatch.Draw(blank, new Rectangle(0, 0, 1600, 900), Color.White * opacity);
            spriteBatch.End();
        }
    }
}
