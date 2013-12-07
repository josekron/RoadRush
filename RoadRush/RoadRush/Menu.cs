using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RoadRush
{
    class Menu
    {
        private Texture2D textureMenu;
        private Texture2D textureHand;
        private Texture2D textureDif;
        private Vector2 positionHand;
        private bool menuActive;
        private bool difficulty;
        private int optionDif;

        private AnimatedTexture texture;

        private enum StateMenu { newGame, instructions, exit};

        StateMenu option;

        //keyboard
        private KeyboardState lastKeyBoardState = Keyboard.GetState();
        private KeyboardState currentKeyBoardState;

        public Menu()
        {
            positionHand = new Vector2(290, 300);
            this.menuActive = true;
            this.option = StateMenu.newGame;
            this.texture = new AnimatedTexture(3f, new Point(3, 5), new Point(0, 1), new Point(20, 20), 180);
            this.difficulty = false;
            this.optionDif = 0;
        }

        public void Load(ContentManager content)
        {
            textureMenu = content.Load<Texture2D>("Menu\\" + "fondoMenu");
            textureHand = content.Load<Texture2D>("Menu\\" + "corazon");
            textureDif = content.Load<Texture2D>("Menu\\" + "dificultad");
            texture.Load(content, "exploradorSheet");
        }

        public int updateMenu(GameTime gameTime)
        {
            texture.UpdateFrame(gameTime);
            if (menuActive)
            {
                if (!this.difficulty)
                {
                    currentKeyBoardState = Keyboard.GetState();
                    if (currentKeyBoardState != lastKeyBoardState)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            switch (option)
                            {
                                case (StateMenu.newGame):
                                    difficulty = true;
                                    break;
                                //return 1;
                                //break;
                                case (StateMenu.instructions):
                                    return 3;
                                //break;
                                case (StateMenu.exit):
                                    return 4;
                                //break;
                            }
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            if (option.Equals(StateMenu.exit))
                            {
                                option = StateMenu.newGame;
                            }
                            else
                            {
                                option++;
                            }
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            if (option.Equals(StateMenu.newGame))
                            {
                                option = StateMenu.exit;
                            }
                            else
                            {
                                option--;
                            }
                        }
                    }
                }
                else
                {
                    currentKeyBoardState = Keyboard.GetState();
                    if (currentKeyBoardState != lastKeyBoardState)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            switch (optionDif)
                            {
                                case (0):
                                    difficulty = false;
                                    return 1;
                                //break;
                                case (1):
                                    difficulty = false;
                                    return 2;
                                //break;
                            }
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            if (optionDif == 0)
                            {
                                this.optionDif = 1;
                            }
                            else
                            {
                                this.optionDif = 0;
                            }
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            if (optionDif == 0)
                            {
                                this.optionDif = 1;
                            }
                            else
                            {
                                this.optionDif = 0;
                            }
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.A))
                        {
                            this.optionDif = 0;
                            this.difficulty = false;
                        }
                    }
                    
                }
                
                lastKeyBoardState = currentKeyBoardState;

            }
            if (!this.difficulty)
            {
                switch (option)
                {
                    case (StateMenu.newGame):
                        positionHand = new Vector2(310, 285);
                        break;
                    case (StateMenu.instructions):
                        positionHand = new Vector2(305, 320);
                        break;
                    case (StateMenu.exit):
                        positionHand = new Vector2(298, 361);
                        break;
                }
            }
            else
            {
                switch (optionDif)
                {
                    case (0):
                        positionHand = new Vector2(335, 315);
                        break;
                    case (1):
                        positionHand = new Vector2(335, 348);
                        break;
                }
            }
            
            return -1;

        }
        public bool getMenuActive()
        {
            return this.menuActive;
        }

        public void changeMenu()
        {
            if (menuActive)
            {
                menuActive = false;
            }
            else
            {
                menuActive = true;
            }
        }
        public void drawMenu(SpriteBatch batch)
        {
            batch.Draw(textureMenu, new Vector2(0, 0), Color.White);
           // batch.Draw(textureHand, positionHand, Color.White);
            texture.DrawFrame(batch, this.positionHand, 0, 0);
            if (this.difficulty)
            {
                batch.Draw(textureDif, new Vector2(0, 0), Color.White);
                texture.DrawFrame(batch, this.positionHand, 0, 0);
            }
        }
    }
}
