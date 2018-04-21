using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TinoHacksGame.States {
    class PlayerSelectState : State {
        private SpriteFont font;

        private int[] playerOption = new int[4];
        private int[] characterOption = new int[4];
        private float[] backButtonHeld = new float[4];
        private float[][] selectTimer = new float[4][];  //player 1 2 3 4, down up left right.
        public PlayerSelectState() {
            for (int i = 0; i < selectTimer.Length; i++) selectTimer[i] = new float[4];
            for (int i = 0; i < playerOption.Length; i++) playerOption[i] = -1;
        }

        public override void Initialize(ContentManager Content) {
            base.Initialize(Content);
            font = Content.Load<SpriteFont>("Font");
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            for (int i = 0; i < 4; i++) {
                GamePadCapabilities capabilities = GamePad.GetCapabilities(i);
                if (capabilities.IsConnected) {
                    Console.WriteLine("Connected: " + i);
                    GamePadState state = GamePad.GetState(i);
                    if (playerOption[i] == -1) playerOption[i] = 0;

                    int selectDelay = 200;
                    if (state.ThumbSticks.Left.Y > 0.75f && selectTimer[i][0] > selectDelay) {
                        playerOption[i] = Math.Max(0, playerOption[i] - 1);
                        selectTimer[i][0] = 0f;
                    }
                    if (state.ThumbSticks.Left.Y < -0.75f && selectTimer[i][1] > selectDelay) {
                        playerOption[i] = Math.Min(2, playerOption[i] + 1);
                        selectTimer[i][1] = 0f;
                    }

                    if(playerOption[i] == 0) {
                        int numChar = 7;
                        if (state.ThumbSticks.Left.X > 0.75f && selectTimer[i][2] > selectDelay) {
                            characterOption[i] = (characterOption[i] + 1) % numChar;
                            selectTimer[i][2] = 0f;
                        }
                        if (state.ThumbSticks.Left.X < -0.75f && selectTimer[i][3] > selectDelay) {
                            characterOption[i] = (characterOption[i] - 1 + numChar) % numChar;
                            selectTimer[i][3] = 0f;
                        }
                    }

                    if (state.IsButtonDown(Buttons.Back)) {
                        if (backButtonHeld[i] > 1000f) GameManager.GetInstance().ChangeScreen(Screens.MENU);
                        backButtonHeld[i] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    if (state.IsButtonUp(Buttons.Back)) {
                        backButtonHeld[i] = 0;
                    }

                    if (state.IsButtonDown(Buttons.A)) {
                        if (playerOption[i] == 0) {
                            //select a character
                        }
                        if (playerOption[i] == 1) {
                            //change the name
                        }
                        if (playerOption[i] == 2) {
                            Console.WriteLine("probably stage select here");
                            GameManager.GetInstance().ChangeScreen(Screens.GAME);
                        }
                    }

                    if (state.IsButtonDown(Buttons.B)) {
                        if (playerOption[i] == 0) {
                            //deselect a character
                        }
                        if (playerOption[i] == 1) {
                            //change the name
                        }
                        if (playerOption[i] == 2) {
                            Console.WriteLine("probably stage select here");
                            GameManager.GetInstance().ChangeScreen(Screens.GAME);
                        }

                    }
                }
                else {
                    playerOption[i] = -1;
                    characterOption[i] = 0;
                    backButtonHeld[i] = 0;
                    selectTimer[i] = new float[4];
                }
            }
            for (int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++) {
                    selectTimer[i][j] += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device) {
            base.Draw(spriteBatch, device);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "P1 Character: " + characterOption[0], new Vector2(400, 100), playerOption[0] == 0 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "P1 Name", new Vector2(400, 200), playerOption[0] == 1 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "P2 Character: " + characterOption[1], new Vector2(600, 100), playerOption[1] == 0 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "P2 Name", new Vector2(600, 200), playerOption[1] == 1 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "P3 Character: " + characterOption[2], new Vector2(800, 100), playerOption[2] == 0 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "P3 Name", new Vector2(800, 200), playerOption[2] == 1 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "P4 Character: " + characterOption[3], new Vector2(1000, 100), playerOption[3] == 0 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "P4 Name", new Vector2(1000, 200), playerOption[3] == 1 ? Color.Black : Color.Wheat);
            spriteBatch.DrawString(font, "Select Stage", new Vector2(700, 300), playerOption[0] == 2 || playerOption[1] == 2 || 
                playerOption[2] == 2 || playerOption[0] == 2 ? Color.Black : Color.Wheat);
            spriteBatch.End();
        }

    }
}
