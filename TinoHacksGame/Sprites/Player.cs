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
        public const float FASTFALL = 14f;
        /// <summary>
        /// The maximum walking speed.
        /// </summary>
        public const float WALK = 0.5f;
        /// <summary>
        /// The maximum dashing speed.
        /// </summary>
        public const float DASH = 1.25f;
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

        private Texture2D walkRightTexture, walkLeftTexture, dashRightTexture, dashLeftTexture,
            idleTexture, idleLeftTexture, fastFallTexture, fallRightTexture, jumpLeftTexture,
            jumpRightTexture, fallLeftTexture, blank;

        public bool AisUP = true;
        private bool wasLeft = false;

        /// <summary>
        /// Whether the <c>Player</c> is fast falling.
        /// </summary>
        public bool FastFalling = false;
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
        private float walkAnimationTimer, dashAnimationTimer, jumpAnimationTimer;
        private int walkAnimationFrameNumber, dashAnimationFrameNumber, jumpAnimationFrame;
        private bool isJumping;

        /// <summary>
        /// The player number.
        /// </summary>
        public int index;

        public int lives = 10;
        public int percentage = 0;
        public float stunTimer = 0f;
        /// <summary>
        /// Creates a new instance of <c>Player</c>
        /// </summary>
        public Player(GameState state, int index) : base(state) {
            this.index = index;
            Scale = GameState.SCALE;
            IsFloating = false;
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

            // animations
            if (walkLeftTexture == null) {
                walkLeftTexture = state.Content.Load<Texture2D>("Move_Left");
                walkRightTexture = state.Content.Load<Texture2D>("Move_Right");
                idleTexture = state.Content.Load<Texture2D>("Idle");
                dashLeftTexture = state.Content.Load<Texture2D>("Dash_Left");
                dashRightTexture = state.Content.Load<Texture2D>("Dash_Right");
                jumpRightTexture = state.Content.Load<Texture2D>("Jump_Right");
                jumpLeftTexture = state.Content.Load<Texture2D>("Jump_Left");
                blank = state.Content.Load<Texture2D>("Blank");
                fallRightTexture = state.Content.Load<Texture2D>("Fall");
                fallLeftTexture = state.Content.Load<Texture2D>("Fall_Left");
                idleLeftTexture = state.Content.Load<Texture2D>("Idle_Left");
            }

            float left = gamePadState.ThumbSticks.Left.X;
            //walk and jump and dash animations
            if (Math.Abs(left) > 0) {
                walkAnimationTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (walkAnimationTimer >= 250f) {
                    walkAnimationTimer = 0f;
                    walkAnimationFrameNumber = (walkAnimationFrameNumber + 1) % 4;
                }
            }
            if (left > 0) wasLeft = false;
            else if (left < 0) wasLeft = true;
            if (isJumping) {
                jumpAnimationTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (jumpAnimationTimer > 200f)
                    jumpAnimationFrame = 1;
            }
            if (dashing) {
                dashTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (dashTimer >= 100f) {
                    dashTimer = 0f;
                    if (dashAnimationFrameNumber == 0)
                        dashAnimationFrameNumber = 1;
                    else
                        dashAnimationFrameNumber = 0;
                }
            }

            //dashing
            if (gamePadState.ThumbSticks.Left.X == -1 && stunTimer == 0) {
                if (secondTapNotSide == -1 && dashTimer < 50f) {
                    dashing = true;
                    firstTapSide = 0;
                    secondTapNotSide = 0;
                }
                else firstTapSide = -1;
                dashTimer = 0f;
            }
            else if (gamePadState.ThumbSticks.Left.X == 1 && stunTimer == 0) {
                if (secondTapNotSide == 1 && dashTimer < 50f) {
                    dashing = true;
                    firstTapSide = 0;
                    secondTapNotSide = 0;
                }
                else firstTapSide = 1;
                dashTimer = 0f;
            }
            else if (firstTapSide != 0 && stunTimer == 0) {
                secondTapNotSide = firstTapSide;
            }
            //left/right movement
            if (stunTimer == 0) {
                if (left < 0 && Velocity.X > 0)
                    Velocity += new Vector2(-0.25f, 0);
                else if (left > 0 && Velocity.X < 0)
                    Velocity += new Vector2(0.25f, 0);
                else if (left > 0 && Velocity.X >= 0 && Velocity.X <= (dashing ? DASH : WALK))
                    Velocity = new Vector2(Math.Max(Math.Min(dashing ? DASH : WALK, Velocity.X + left * SPEED), -(dashing ? DASH : WALK)), Velocity.Y);
                else if (left < 0 && Velocity.X <= 0 && Velocity.X >= -(dashing ? DASH : WALK))
                    Velocity = new Vector2(Math.Max(Math.Min(dashing ? DASH : WALK, Velocity.X + left * SPEED), -(dashing ? DASH : WALK)), Velocity.Y);
            }
            dashTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //air and ground friction
            if (gamePadState.ThumbSticks.Left.Length() == 0) {
                float coeff = IsFloating ? 0.02f : 0.4f;
                if (Velocity.X >= 0.1 || Velocity.X <= -0.1) Velocity -= new Vector2(Velocity.X * coeff, 0);
                else {
                    Velocity = new Vector2(0.0f, Velocity.Y);
                    dashing = false;
                    firstTapSide = 0;
                    secondTapNotSide = 0;
                }
            }

            //ground detection
            foreach (Platform p in state.currentStage.Platforms) {
                Rectangle rect = GetDrawRectangle();
                Rectangle rect3 = new Rectangle(rect.X, rect.Y, rect.Width / 2, rect.Height - 10);
                Rectangle rect2 = p.GetDrawRectangle();
                if (rect3.Intersects(rect2)) {
                    if (rect3.Bottom <= rect2.Bottom && Velocity.Y > 0) {
                        Position = new Vector2(Position.X, rect2.Top - Origin.Y * GameState.SCALE - 1);
                        FastFalling = false;
                        IsFloating = false;
                        numJumps = 0;
                        jumpTimer = 0f;
                    }
                    break;
                }
                else
                    IsFloating = true;
            }

            //jump
            if (gamePadState.IsButtonDown(Buttons.A) && stunTimer == 0) {
                isJumping = true;
                if (AisUP && numJumps < MAXJUMPS) {
                    jumpTimer = 0f;
                    numJumps++;
                    FastFalling = false;
                    AisUP = false;
                    Velocity = new Vector2(Velocity.X, -1.5f);
                    IsFloating = true;
                }
                else if (IsFloating) {
                    if (jumpTimer < 125f) Velocity = new Vector2(Velocity.X, -1.55f);
                    else if (jumpTimer < 150f) Velocity = new Vector2(Velocity.X, -1.55f);
                }
                jumpTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (gamePadState.IsButtonUp(Buttons.A) && stunTimer == 0) {
                AisUP = true;
                jumpTimer = 200f;
                isJumping = false;
            }
            //fast falling
            if (IsFloating && gamePadState.ThumbSticks.Left.Y == -1 && stunTimer == 0) {
                if (secondTapNotDown && fastFallTimer < 50f) {
                    FastFalling = true;
                    firstTapDown = false;
                    secondTapNotDown = false;
                }
                else firstTapDown = true;
                fastFallTimer = 0f;
            }
            else if (firstTapDown && stunTimer == 0) secondTapNotDown = true;
            //gravity
            if (FastFalling && IsFloating) {
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
                FastFalling = false;
            }
            if (stunTimer == 0 && InputManager.GetInstance().IsPressed(Buttons.X, (PlayerIndex)index)) attack(gamePadState.ThumbSticks.Left, IsFloating);
            stunTimer = Math.Max(0f, stunTimer - (float)gameTime.ElapsedGameTime.TotalMilliseconds);

        }

        public void getHit(int dmg, Vector2 knockback, float stunTime) {
            percentage += dmg;
            Velocity = knockback * (float)Math.Log10(percentage);
            Velocity = new Vector2(Velocity.X, -Velocity.Y * 1.2f);

            stunTimer = stunTime * (float)Math.Log10(percentage) * 20f;
            Console.WriteLine(percentage + " " + Velocity + " " + stunTimer);
        }

        public void attack(Vector2 dir, bool inAir) {
            inAir = inAir && Math.Abs(Velocity.Y) > 0.1f;
            if (!inAir) {
                if (dir.X <= -0.75f) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(-Size.X - 50, 0), blank, new Point(50, 5), 20f, 10, 5f));
                else if (dir.X >= 0.75f) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(Size.X / 2, 0), blank, new Point(50, 5), 20f, 10, 5f));
                else if (dir.Y >= 0.75f) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(Size.X / 2, 0), blank, new Point(50, 5), 20f, 10, 5f));
                else if (dir.Y <= -0.75f) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(-50, 25), blank, new Point(50, 10), 20f, 10, 5f));
                else if (wasLeft) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(-Size.X - 25, 0), blank, new Point(25, 10), 20f, 10, 5f));
                else GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(Size.X / 2, 0), blank, new Point(25, 10), 20f, 10, 5f));
            }
            else {
                if (dir.X <= -0.75f) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(-Size.X - 50, 0), blank, new Point(50, 5), 20f, 10, 5f));
                else if (dir.X >= 0.75f) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(Size.X / 2, 0), blank, new Point(50, 5), 20f, 10, 5f));
                else if (dir.Y >= 0.75f) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(Size.X / 2, 0), blank, new Point(50, 5), 20f, 10, 5f));
                else if (dir.Y <= -0.75f) GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(Size.X / 2, 0), blank, new Point(50, 5), 20f, 10, 5f));
                else GameManager.GetInstance().hitBoxes.Add(new HitBox(null, this, new Vector2(-25, -25), blank, new Point(50, 50), 20f, 10, 5f));
            }
        }

        /// <summary>
        /// Draws the <c>Player</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            if (idleTexture != null)
                Origin = new Vector2(idleTexture.Width / 2f, idleTexture.Height / 2f);

            bool left = Velocity.X < 0f;

            Texture2D drawnTexture = idleTexture;

            if (wasLeft)
                drawnTexture = idleLeftTexture;

            if (dashing) {
                if (left)
                    drawnTexture = dashLeftTexture;
                else
                    drawnTexture = dashRightTexture;
            }
            else if (Math.Abs(Velocity.X) > 0) {
                if (!left)
                    drawnTexture = walkRightTexture;
                else
                    drawnTexture = walkLeftTexture;
            }

            if (isJumping) {
                if (!wasLeft)
                    drawnTexture = jumpRightTexture;
                else
                    drawnTexture = jumpLeftTexture;
            }
            else if (Velocity.Y > 0.1f) {
                //if (!FastFalling)
                //    drawnTexture = slowFallTexture;
                //else
                //    drawnTexture = fastFallTexture;

                if (!wasLeft)
                    drawnTexture = fallRightTexture;
                else
                    drawnTexture = fallLeftTexture;
            }

            if (drawnTexture != null) {
                //spriteBatch.Draw(blank, GetDrawRectangle(), Color.White * 0.4f);
                if (drawnTexture == walkLeftTexture || drawnTexture == walkRightTexture)
                    spriteBatch.Draw(drawnTexture, Position, new Rectangle(29 * walkAnimationFrameNumber, 0, 29, 44), Color.White, rotation,
                        Origin, GameState.SCALE, SpriteEffects.None, 0f);
                else if (drawnTexture == dashLeftTexture || dashRightTexture == drawnTexture)
                    spriteBatch.Draw(drawnTexture, Position, new Rectangle(34 * dashAnimationFrameNumber, 0, 34, 50), Color.White, rotation,
                        Origin, GameState.SCALE, SpriteEffects.None, 0f);
                else if (drawnTexture == jumpLeftTexture || drawnTexture == jumpRightTexture)
                    spriteBatch.Draw(drawnTexture, Position, new Rectangle(28 * jumpAnimationFrame, 0, 28, 42), Color.White, rotation,
                        Origin, GameState.SCALE, SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(drawnTexture, Position, null, Color.White, rotation,
                        Origin, GameState.SCALE, SpriteEffects.None, 0f);
            }

        }

        /// <summary>
        /// Overriding the <c>GetDrawRectangle</c>.
        /// </summary>
        /// <returns></returns>
        public override Rectangle GetDrawRectangle() {
            return new Rectangle((int)(Position.X - Origin.X * Scale),
                (int)(Position.Y - Origin.Y * Scale), (int)(Size.X *
                Scale / 2f), (int)(Size.Y * Scale));
        }
    }

}
