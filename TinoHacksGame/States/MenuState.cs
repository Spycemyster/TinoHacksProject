using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TinoHacksGame.States
{
    /// <summary>
    /// The <c>State</c> for where the player will select menu options.
    /// </summary>
    public class MenuState : State
    {
        /// <summary>
        /// Creates a new instance of <c>MenuState</c>.
        /// </summary>
        public MenuState()
        {

        }
        public override void Initialize(ContentManager Content) {
            base.Initialize(Content);
            
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            if (capabilities.IsConnected) {
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                if (state.ThumbSticks.Left.X < -0.5f) Console.WriteLine("yeet");
                if (state.ThumbSticks.Left.X > 0.5f) Console.WriteLine("yote");
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device) {
            base.Draw(spriteBatch, device);
        }
    }
}
