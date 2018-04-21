using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoHacksGame.Sprites;

namespace TinoHacksGame.States.UserInterface
{
    /// <summary>
    /// A component within the user interface.
    /// </summary>
    public class UIComponent : Sprite
    {
        /// <summary>
        /// The handler for events regarding <c>UIComponent</c>.
        /// </summary>
        /// <param name="arg"></param>
        public delegate void UIEvent(UIArg arg);

        /// <summary>
        /// When the user enters the <c>UIComponent</c>
        /// </summary>
        public UIEvent OnEnter;

        /// <summary>
        /// When the user is hovering over the <c>UIComponent</c>.
        /// </summary>
        public UIEvent OnHover;

        /// <summary>
        /// When the user leaves the <c>UIComponent</c>.
        /// </summary>
        public UIEvent OnLeave;

        /// <summary>
        /// The text of the <c>UIComponent</c>.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        private Cursor cursor;
        private UIArg arg;
        private bool isEntered;

        /// <summary>
        /// Creates a new instance of <c>UIComponent</c>.
        /// </summary>
        public UIComponent(Cursor cursor)
            : base(null)
        {
            isEntered = false;
            this.cursor = cursor;
            arg = new UIArg(this);
        }

        /// <summary>
        /// Updates the <c>UIComponent</c>'s logic and conditional checking.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (GetDrawRectangle().Contains(cursor.Position))
            {
                OnEnter?.Invoke(arg);

                if (!isEntered)
                    OnHover?.Invoke(arg);

                isEntered = true;

            }
            else
            {
                if (isEntered)
                    OnLeave?.Invoke(arg);

                isEntered = false;
            }
        }

        /// <summary>
        /// Draws the <c>UIComponent</c> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }

    /// <summary>
    /// The argument passed through the ui event.
    /// </summary>
    public class UIArg
    {
        /// <summary>
        /// The component that is affected by the event.
        /// </summary>
        public readonly UIComponent Component;

        /// <summary>
        /// Creates a new instance of the <c>UIArg</c>.
        /// </summary>
        /// <param name="component"></param>
        public UIArg(UIComponent component)
        {
            Component = component;
        }
    }
}
