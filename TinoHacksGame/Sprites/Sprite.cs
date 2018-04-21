using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoHacksGame.States;

namespace TinoHacksGame.Sprites
{
    /// <summary>
    /// An object that holds textures and positions.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// The position point of the <c>Sprite</c>.
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// The color the <c>Sprite</c> is drawn at.
        /// </summary>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// The size of the sprite.
        /// </summary>
        public Point Size
        {
            get;
            set;
        }

        /// <summary>
        /// The change in position of the <c>Sprite</c>.
        /// </summary>
        public Vector2 Velocity
        {
            get;
            set;
        }

        /// <summary>
        /// The texture of the <c>Sprite</c>.
        /// </summary>
        public Texture2D Texture
        {
            get;
            set;
        }

        /// <summary>
        /// The opacity that the texture is drawn at.
        /// </summary>
        public float Opacity
        {
            get;
            set;
        }

        /// <summary>
        /// The origin that the <c>Sprite</c> is located at.
        /// </summary>
        public Vector2 Origin
        {
            get;
            set;
        }

        /// <summary>
        /// The game state that the sprite resides in.
        /// </summary>
        protected GameState state;

        /// <summary>
        /// Creates a new instance of a <c>Sprite</c>.
        /// </summary>
        public Sprite(GameState state)
        {
            this.state = state;
            Color = Color.White;
        }

        /// <summary>
        /// The collidable rectangle of the sprite.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetDrawRectangle()
        {
            return new Rectangle((int)(Position.X - Origin.X * GameState.SCALE), 
                (int)(Position.Y - Origin.Y * GameState.SCALE), (int)(Size.X *
                GameState.SCALE), (int)(Size.Y * GameState.SCALE));
        }

        /// <summary>
        /// Updates the <c>Sprite</c>.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Draws the <c>Sprite</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
