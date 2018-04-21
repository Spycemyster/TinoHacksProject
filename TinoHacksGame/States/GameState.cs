using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TinoHacksGame.Sprites;

namespace TinoHacksGame.States
{
    /// <summary>
    /// The <c>State</c> in which most of the gameplay will take
    /// place in.
    /// </summary>
    public class GameState : State
    {
        /// <summary>
        /// The gravitational constant for the objects.
        /// </summary>
        public const float GRAVITY = 0.0681f;

        /// <summary>
        /// The scale that the sprites are drawn at.
        /// </summary>
        public const float SCALE = 2f;

        /// <summary>
        /// The players in the game state.
        /// </summary>
        public List<Player> Players
        {
            get;
            private set;
        }

        /// <summary>
        /// The platforms of the game state.
        /// </summary>
        public List<Platform> Platforms
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new instance of <c>GameState</c>.
        /// </summary>
        public GameState()
        {
            Players = new List<Player>();
            Platforms = new List<Platform>();
        }

        /// <summary>
        /// Initializes the <c>GameState</c>.
        /// </summary>
        /// <param name="Content"></param>
        public override void Initialize(ContentManager Content)
        {
            base.Initialize(Content);

            Texture2D placeHolder = Content.Load<Texture2D>("Placeholder");

            Player p1 = new Player(this, PlayerIndex.One)
            {
                Texture = placeHolder,
                Position = new Vector2(100, 0),
            };

            Player p2 = new Player(this, PlayerIndex.Two)
            {
                Texture = placeHolder,
                Position = new Vector2(300, 0),
            };
            Player p3 = new Player(this, PlayerIndex.Three)
            {
                Texture = placeHolder,
                Position = new Vector2(500, 0),
            };
            Player p4 = new Player(this, PlayerIndex.Four)
            {
                Texture = placeHolder,
                Position = new Vector2(700, 0),
            };

            Players.Add(p1);
            Players.Add(p2);
            Players.Add(p3);
            Players.Add(p4);

            Platform plat = new Platform(this)
            {
                Texture = Content.Load<Texture2D>("Blank"),
                Position = new Vector2(50, 850),
                Size = new Point(1000, 30),
            };

            Platforms.Add(plat);
        }

        /// <summary>
        /// Updates the <c>GameState</c>'s conditional checking and logic.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Player p in Players)
                p.Update(gameTime);

            foreach (Platform p in Platforms)
                p.Update(gameTime);
        }

        /// <summary>
        /// Draws the <c>GameState</c>'s contents to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="device"></param>
        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
        {
            base.Draw(spriteBatch, device);

            spriteBatch.Begin();

            foreach (Player p in Players)
                p.Draw(spriteBatch);

            foreach (Platform p in Platforms)
                p.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
