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
        public PlayerSelectState() {

        }

        public override void Initialize(ContentManager Content) {
            base.Initialize(Content);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            
            for(int i = 0; i < 4; i++) {
                GamePadCapabilities capabilities = GamePad.GetCapabilities(i);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device) {
            base.Draw(spriteBatch, device);
        }

    }
}
