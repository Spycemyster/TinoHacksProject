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
    public class HitBox : Sprite
    {
        public Player player;
        public Vector2 offset;
        public int index;
        public float duration;
        public int dmg;

        public HitBox(GameState state, Player p, Vector2 os, Texture2D sprite, Point size, float d, int damage, float st) : base(state) {
            player = p;
            offset = os;
            index = p.index;
            duration = d;
            dmg = damage;
            this.Size = size;
            Scale = GameState.SCALE;
            this.Texture = sprite;
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
                        p.getHit(dmg, Vector2.Normalize(new Vector2(hRect.X - pRect.X, hRect.Y - pRect.Y)));
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
