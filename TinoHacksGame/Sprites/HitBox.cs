using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinoHacksGame.States;

namespace TinoHacksGame.Sprites
{
    class HitBox : Sprite
    {
        public Player player;
        public Vector2 offset;
        public int index;
        public float duration;
        public int dmg;
        public float stuntime;

        public HitBox(GameState state, Player p, Vector2 os, Sprite sprite, float d, int damage, float st) : base(state) {
            player = p;
            offset = os;
            index = p.index;
            duration = d;
            dmg = damage;
            stuntime = st;
            Scale = GameState.SCALE;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (!isActive()) return;
            Position = new Vector2(player.Position.X + offset.X, player.Position.Y + offset.Y);
            foreach(Player p in GameManager.GetInstance().Players) {
                if(p.index != index) {
                    Rectangle pRect = p.GetDrawRectangle();
                    Rectangle hRect = GetDrawRectangle();
                    if (pRect.Intersects(hRect)) {
                        p.getHit(dmg, new Vector2(hRect.X - pRect.X, hRect.Y - pRect.Y), stuntime);
                    }
                }
            }
            duration = duration - (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            if (!isActive()) return;

            Origin = Vector2.Zero;
            spriteBatch.Draw(Texture, GetDrawRectangle(), Color.White);
        }

        public override Rectangle GetDrawRectangle() {
            return new Rectangle((int)(Position.X - Origin.X * Scale),
                (int)(Position.Y - Origin.Y * Scale), (int)(Size.X *
                Scale), (int)(Size.Y * Scale));
        }

        public bool isActive() {
            return duration > 0;
        }
    }
}
