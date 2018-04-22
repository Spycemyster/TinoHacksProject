using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoHacksGame.Sprites;
using TinoHacksGame.States;

namespace TinoHacksGame
{
    /// <summary>
    /// Single-Ton object that manages different states of the game. Each state is
    /// stored into a different class and is individually updated and drawn to the screen.
    /// </summary>
    public class GameManager
    {
        #region Single-Ton instance Code
        private static GameManager inst;

        public List<Player> Players {
            get;
            set;
        }
        public Stage stage {
            get;
            set;
        }
        public List<HitBox> hitBoxes {
            get;
            set;
        }

        /// <summary>
        /// Retrieves the single ton instance of the <c>GameManager</c>.
        /// </summary>
        /// <returns></returns>
        public static GameManager GetInstance()
        {
            if (inst == null) inst = new GameManager();
            return inst;
        }

        /// <summary>
        /// Creates a new instance of the <c>GameManager</c>.
        /// </summary>
        private GameManager()
        {
        }
        #endregion

        /// <summary>
        /// The current state of the game.
        /// </summary>
        public State CurrentState
        {
            get;
            private set;
        }

        /// <summary>
        /// The content manager instance for the <c>GameManager</c>
        /// </summary>
        public ContentManager Content
        {
            get { return content; }
        }

        private ContentManager content;
        private GraphicsDevice graphicsDevice;
        private Game1 game;

        /// <summary>
        /// Performs first time startup initialization and contenting loading.
        /// </summary>
        /// <param name="game"></param>
        public void Initialize(Game1 game)
        {
            this.game = game;
            graphicsDevice = game.GraphicsDevice;
            content = new ContentManager(game.Content.ServiceProvider, "Content");

            ChangeScreen(Screens.TITLE);
        }
        
        /// <summary>
        /// Exits the game.
        /// </summary>
        public void Exit()
        {
            game.Exit();
        }

        /// <summary>
        /// Changes the <c>State</c> of the game.
        /// </summary>
        /// <param name="screen"></param>
        public void ChangeScreen(Screens screen)
        {
            // switches the screen to the specified screen.
            switch (screen)
            {
                case Screens.GAME:
                    CurrentState = new GameState();
                    break;

                case Screens.MENU:
                    CurrentState = new MenuState();
                    break;

                case Screens.PLAYERSELECT:
                    CurrentState = new PlayerSelectState();
                    break;

                case Screens.TITLE:
                    CurrentState = new TitleState();
                    break;
            }

            CurrentState.Initialize(Content);
        }

        /// <summary>
        /// Updates the <c>GameManager</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            InputManager.GetInstance().Update(gameTime);
            CurrentState.Update(gameTime);
        }

        /// <summary>
        /// Draws the <c>GameManager</c>'s current state to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentState.Draw(spriteBatch, graphicsDevice);
        }
    }

    /// <summary>
    /// The different screen states within the <c>GameManager</c>.
    /// </summary>
    public enum Screens
    {
        /// <summary>
        /// The state where the title is displayed.
        /// </summary>
        TITLE,

        /// <summary>
        /// The state where most of the gameplay will occur on.
        /// </summary>
        GAME,

        /// <summary>
        /// The state where the player chooses menu options from.
        /// </summary>
        MENU,

        /// <summary>
        /// The state where the player chooses a characters.
        /// </summary>
        PLAYERSELECT,

        /// <summary>
        /// The state where the player chooses an online lobby.
        /// </summary>
        ONLINELOBBY,

        /// <summary>
        /// The state where the player changes their settings.
        /// </summary>
        SETTINGS
    }
}
