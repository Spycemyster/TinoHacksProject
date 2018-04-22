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
        public const float WALK = 1f;
        /// <summary>
        /// The maximum dashing speed.
        /// </summary>
        public const float DASH = 2.5f;
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

        private bool isWalking, isDashing;
        private Vector2 lastPressed;

        public bool AisUP = true;

        /// <summary>
        /// The speed in which the player moves at.
        /// </summary>
        public const float SPEED = 0.1f;

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
        /// Checks if the boi was dashing
        /// </summary>
        /// <returns></returns>
        public bool CheckIfDashing()
        {
            GamePadState gamePadState = GamePad.GetState(index);
            if(gamePadState.ThumbSticks.Left.Equals(lastPressed))
            {
                return true;
            }
            return false;
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
            float left = gamePadState.ThumbSticks.Left.X;
            
            if (gamePadState.ThumbSticks.Left.X < 0 && Velocity.X > 0) Velocity += new Vector2(-0.25f, 0);
            else if (gamePadState.ThumbSticks.Left.X > 0 && Velocity.X < 0) Velocity += new Vector2(0.25f, 0);
            else
            {
                if (!CheckIfDashing())
                {
                    lastPressed = gamePadState.ThumbSticks.Left;
                    Velocity = new Vector2(Math.Max(Math.Min(WALK, Velocity.X + left * SPEED), -WALK), Velocity.Y);

                }
            }

            //air and ground friction
            if (gamePadState.ThumbSticks.Left.Length() == 0) {
                isWalking = false;
                isDashing = false;
                float coeff = IsFloating ? 0.02f : 0.4f;
                if ((Velocity.X >= 0.1 || Velocity.X <= -0.1)) Velocity -= new Vector2(Velocity.X * coeff, 0);
                else Velocity = new Vector2(0.0f, Velocity.Y);
            }

            //ground detection
            foreach (Platform p in state.Platforms) {
                Rectangle rect = GetDrawRectangle();
                Rectangle rect2 = p.GetDrawRectangle();

                
                if (rect.Intersects(rect2)) {
                    Position = new Vector2(Position.X, rect2.Top - Origin.Y * GameState.SCALE);
                    IsFloating = false;
                    numJumps = 0;
                    jumpTimer = 0f;
                    break;
                }
                else {
                    IsFloating = true;
                }
            }

            //jump
            if (gamePadState.IsButtonDown(Buttons.A)) {
                Console.WriteLine(numJumps);
                if (AisUP && numJumps < MAXJUMPS) {
                    jumpTimer = 0f;
                    numJumps++;
                    AisUP = false;
                    Velocity = new Vector2(Velocity.X, -1.5f);
                    IsFloating = true;
                }
                else if (IsFloating) {
                    if (jumpTimer < 75f) Velocity = new Vector2(Velocity.X, -1.5f);
                    else if (jumpTimer < 100f) Velocity = new Vector2(Velocity.X, -1.75f);
                }
                jumpTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (gamePadState.IsButtonUp(Buttons.A)) {
                AisUP = true;
                
            }

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
