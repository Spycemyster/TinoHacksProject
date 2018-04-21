using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TinoHacksGame.States {
    /// <summary>
    /// The <c>State</c> for where the player will select menu options.
    /// </summary>
    public class MenuState : State {
        private SpriteFont font;
        private int option = 0;
        private float selectTimerDown = 0f;
        private float selectTimerUp = 0f;
        /// <summary>
        /// Creates a new instance of <c>MenuState</c>.
        /// </summary>
        public MenuState() {

        }
        public override void Initialize(ContentManager Content) {
            base.Initialize(Content);
            font = Content.Load<SpriteFont>("Font");
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            int selectDelay = 100;
            if (capabilities.IsConnected) {
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                if (state.ThumbSticks.Left.Y > 0.75f && selectTimerDown > selectDelay) {
                    option = Math.Max(0, option - 1);
                    selectTimerDown = 0f;
                }
                if (state.ThumbSticks.Left.Y < -0.75f && selectTimerUp > selectDelay) {
                    option = Math.Min(2, option + 1);
                    selectTimerUp = 0f;
                }
                
                if (state.IsButtonDown(Buttons.A)) {
                    Screens[] screens = { Screens.PLAYERSELECT, Screens.ONLINELOBBY, Screens.SETTINGS };
                    GameManager.GetInstance().ChangeScreen(screens[option]);
                }
            }
            selectTimerDown += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            selectTimerUp += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device) {
            base.Draw(spriteBatch, device);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Local", new Vector2(400, 100), option == 0 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "Online", new Vector2(400, 200), option == 1 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "Settings", new Vector2(400, 300), option == 2 ? Color.Black : Color.Wheat);
            spriteBatch.End();
        }
    }
}
