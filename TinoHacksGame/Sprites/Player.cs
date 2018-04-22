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
        public const float FASTFALL = 12f;
        /// <summary>
        /// The maximum walking speed.
        /// </summary>
        public const float WALK = 0.5f;
        /// <summary>
        /// The maximum dashing speed.
        /// </summary>
        public const float DASH = 1.5f;
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

        public bool fastFalling = false;
        private bool firstTapDown = false;
        private bool secondTapNotDown = false;
        private float fastFallTimer = 0f;

        private bool dashing = false;
        private int firstTapSide = 0;
        private int secondTapNotSide = 0;
        private float dashTimer = 0f;

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
        /// Updates the <c>Player</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            Size = Texture.Bounds.Size;
            GamePadState gamePadState = GamePad.GetState(index);
            rotation += gamePadState.ThumbSticks.Right.X * MathHelper.Pi / 10;

            //dashing
            float left = gamePadState.ThumbSticks.Left.X;
            if (gamePadState.ThumbSticks.Left.X == -1) {
                if (secondTapNotSide == -1 && dashTimer < 50f) {
                    dashing = true;
                    firstTapSide = 0;
                    secondTapNotSide = 0;
                }
                else firstTapSide = -1;
                dashTimer = 0f;
            }
            else if (gamePadState.ThumbSticks.Left.X == 1) {
                if (secondTapNotSide == 1 && dashTimer < 50f) {
                    dashing = true;
                    firstTapSide = 0;
                    secondTapNotSide = 0;
                }
                else firstTapSide = 1;
                dashTimer = 0f;
            }
            else if (firstTapSide != 0) {
                secondTapNotSide = firstTapSide;
            }
            //left/right movement
            if (left < 0 && Velocity.X > 0) Velocity += new Vector2(-0.25f, 0);
            else if (left > 0 && Velocity.X < 0) Velocity += new Vector2(0.25f, 0);
            else Velocity = new Vector2(Math.Max(Math.Min(dashing ? DASH : WALK, Velocity.X + left * SPEED), -(dashing ? DASH : WALK)), Velocity.Y);
            dashTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //air and ground friction
            if (gamePadState.ThumbSticks.Left.Length() == 0) {
                float coeff = IsFloating ? 0.02f : 0.4f;
                if (Velocity.X >= 0.1 || Velocity.X <= -0.1) Velocity -= new Vector2(Velocity.X * coeff, 0);
                else {
                    Velocity = new Vector2(0.0f, Velocity.Y);
                    dashing = false;
                }
            }

            //ground detection
            foreach (Platform p in state.Platforms) {
                Rectangle rect = GetDrawRectangle();
                Rectangle rect2 = p.GetDrawRectangle();
                if (rect.Intersects(rect2)) {
                    fastFalling = false;
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
                if (AisUP && numJumps < MAXJUMPS) {
                    jumpTimer = 0f;
                    numJumps++;
                    fastFalling = false;
                    AisUP = false;
                    Velocity = new Vector2(Velocity.X, -1.5f);
                    IsFloating = true;
                }
                else if (IsFloating) {
                    Console.WriteLine("lolz" + jumpTimer);
                    if (jumpTimer < 175f) Velocity = new Vector2(Velocity.X, -1.75f);
                    else if (jumpTimer < 200f) Velocity = new Vector2(Velocity.X, -2f);
                }
                jumpTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (gamePadState.IsButtonUp(Buttons.A)) {
                AisUP = true;
                jumpTimer = 200f;
            }
            //fast falling
            if (IsFloating && gamePadState.ThumbSticks.Left.Y == -1) {
                if (secondTapNotDown && fastFallTimer < 50f) {
                    fastFalling = true;
                    firstTapDown = false;
                    secondTapNotDown = false;
                }
                else firstTapDown = true;
                fastFallTimer = 0f;
            }
            else if (firstTapDown) secondTapNotDown = true;
            //gravity
            if (fastFalling && IsFloating) {
                Velocity += new Vector2(0, FASTFALL * GameState.GRAVITY);
            }
            else if (IsFloating) {
                Velocity += new Vector2(0, SLOWFALL * GameState.GRAVITY);
                jumpTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                fastFallTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else {
                numJumps = 0;
                Velocity = new Vector2(Velocity.X, 0.0f);
                fastFallTimer = 0f;
                firstTapDown = false;
                secondTapNotDown = false;
                fastFalling = false;
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
