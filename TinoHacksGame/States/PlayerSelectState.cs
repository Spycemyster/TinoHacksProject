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
    class PlayerSelectState : State {
        private int[] playerOption = new int[4];
        private float backButtonHeld = 0f;
        public PlayerSelectState() {

        }

        public override void Initialize(ContentManager Content) {
            base.Initialize(Content);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            
            for(int i = 0; i < 4; i++) {
                GamePadCapabilities capabilities = GamePad.GetCapabilities(i);
                if (capabilities.IsConnected) {
                    GamePadState state = GamePad.GetState(i);

                    if (state.IsButtonDown(Buttons.Back)) {
                        backButtonHeld += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device) {
            base.Draw(spriteBatch, device);
        }

    }
}
