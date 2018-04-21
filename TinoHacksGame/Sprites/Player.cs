using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TinoHacksGame.States;

namespace TinoHacksGame.Sprites
{
    /// <summary>
    /// A player sprite within the game.
    /// </summary>
    public class Player : Sprite
    {
        /// <summary>
        /// If the player is on the ground or not.
        /// </summary>
        public bool IsFloating
        {
            get;
            set;
        }

        /// <summary>
        /// The speed in which the player moves at.
        /// </summary>
        public const float SPEED = 0.01f;

        /// <summary>
        /// The player number.
        /// </summary>
        public PlayerIndex index;

        /// <summary>
        /// Creates a new instance of <c>Player</c>
        /// </summary>
        public Player(GameState state, PlayerIndex index) : base(state) 
        {
            this.index = index;
            IsFloating = true;
        }

        /// <summary>
        /// Updates the <c>Player</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Size = Texture.Bounds.Size;
            IsFloating = true;
            if(Velocity.X < 2.5)
                Velocity += GamePad.GetState((int)index).ThumbSticks.Left * SPEED;
         
            foreach (Platform p in state.Platforms)
            {
                Rectangle rect = GetDrawRectangle();
                Rectangle rect2 = p.GetDrawRectangle();

                if (rect.Intersects(rect2))
                {
                    Position = new Vector2(Position.X, rect2.Top - Origin.Y * GameState.SCALE);
                    IsFloating = false;
                    break;
                }
            }

            if (GamePad.GetState((int)index).ThumbSticks.Left.Length() == 0 && !IsFloating)
            {
                if ((Velocity.X >= 0.1 || Velocity.X <= -0.1))
                    Velocity -= new Vector2(Velocity.X * 0.05f, Velocity.Y);
                else //prevents player from sliding forever
                    Velocity = new Vector2(0.0f, Velocity.Y);
            }

            if (!IsFloating && GamePad.GetState((int)index).IsButtonDown(Buttons.A))
            {
                Velocity = new Vector2(Velocity.X, -1.3f);
                IsFloating = true;
            }

            if (IsFloating)
            {
                Velocity += new Vector2(0, GameState.GRAVITY);
            }
            else
            {
                Velocity = new Vector2(Velocity.X, 0.0f);
            }
        }

        /// <summary>
        /// Draws the <c>Player</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, 
                Origin, GameState.SCALE, SpriteEffects.None, 0f);
        }
    }
    
}
