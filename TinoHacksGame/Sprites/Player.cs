using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TinoHacksGame.States;

namespace TinoHacksGame.Sprites {
    /// <summary>
    /// A player sprite within the game.
    /// </summary>
    public class Player : Sprite {
        /// <summary>
        /// The maximum slowfall speed.
        /// </summary>
        public const float SLOWFALL = 2.5f;
        /// <summary>
        /// The maximum fastfall speed.
        /// </summary>
        public const float FASTFALL = 4f;
        /// <summary>
        /// The maximum walking speed.
        /// </summary>
        public const float WALK = 2.5f;
        /// <summary>
        /// The maximum dashing speed.
        /// </summary>
        public const float DASH = 4f;
        /// <summary>
        /// The maximum number of jumps.
        /// </summary>
        public const int MAXJUMPS = 2;
        public int numJumps = 0;
        /// <summary>
        /// If the player is on the ground or not.
        /// </summary>
        public bool IsFloating {
            get;
            set;
        }

        public bool AisUP = true;

        /// <summary>
        /// The speed in which the player moves at.
        /// </summary>
        public const float SPEED = 0.02f;

        private float rotation;
        private float jumpTimer;

        /// <summary>
        /// The player number.
        /// </summary>
        public int index;

        /// <summary>
        /// Creates a new instance of <c>Player</c>
        /// </summary>
        public Player(GameState state, int index) : base(state) {
            this.index = index;
            IsFloating = true;
        }

        /// <summary>
        /// Updates the <c>Player</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            Size = Texture.Bounds.Size;
            GamePadState gamePadState = GamePad.GetState(index);
            rotation += gamePadState.ThumbSticks.Right.X * MathHelper.Pi / 10;

            //left/right movement
            if (Velocity.X <= WALK && Velocity.X >= -WALK) {
                if(Velocity.X > 0 && gamePadState.ThumbSticks.Left.X < 0) {
                    Velocity += new Vector2(-0.2f, 0);
                }
                else if (Velocity.X < 0 && gamePadState.ThumbSticks.Left.X > 0) {
                    Velocity += new Vector2(0.2f, 0);
                }
                else Velocity += gamePadState.ThumbSticks.Left * SPEED;

                if (Velocity.X > WALK) Velocity = new Vector2(WALK, Velocity.Y);
                if (Velocity.X < -WALK) Velocity = new Vector2(-WALK, Velocity.Y);
            }
            
            
            //ground detection
            foreach (Platform p in state.Platforms) {
                Rectangle rect = GetDrawRectangle();
                Rectangle rect2 = p.GetDrawRectangle();

                if (rect.Intersects(rect2)) {
                    Position = new Vector2(Position.X, rect2.Top - Origin.Y * GameState.SCALE);
                    IsFloating = false;
                    break;
                }
                else {
                    IsFloating = true;
                }
            }

            //friction
            if (gamePadState.ThumbSticks.Left.Length() == 0 && !IsFloating) {
                if ((Velocity.X >= 0.1 || Velocity.X <= -0.1))
                    Velocity -= new Vector2(Velocity.X * 0.4f, Velocity.Y);
                else //prevents player from sliding forever
                    Velocity = new Vector2(0.0f, Velocity.Y);
            }

            //jump
            if (gamePadState.IsButtonDown(Buttons.A) && AisUP && numJumps < MAXJUMPS) {
                numJumps++;
                AisUP = false;
                Velocity = new Vector2(Velocity.X, -1.5f);
                IsFloating = true;
            }
            if (gamePadState.IsButtonUp(Buttons.A)) AisUP = true;

            if (IsFloating) {
                Velocity += new Vector2(0, GameState.GRAVITY);
            }
            else {
                numJumps = 0;
                Velocity = new Vector2(Velocity.X, 0.0f);
            }
        }

        /// <summary>
        /// Draws the <c>Player</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            spriteBatch.Draw(Texture, Position, null, Color.White, rotation,
                Origin, GameState.SCALE, SpriteEffects.None, 0f);
        }
    }

}
