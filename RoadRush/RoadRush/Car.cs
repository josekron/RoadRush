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
    class Car
    {
        private Texture2D texture;
        private Texture2D texture2;
        private Texture2D shadow;
        private Vector2 position;
        private Texture2D textureCrash;

        private Texture2D pointLeft;
        private Texture2D pointRight;

        private AnimatedTexture textureAnimated;

        private int timeSizeLastFrame;
        private int millisecondsPerFrame;
        private int distanceDrift;
        private bool drift;
        private int driftLeft;
        private bool crash;

        private int timeCrash;
        private Vector2 pathCrash;

        public Car()
        {
            this.position = new Vector2(360, 320);
            this.textureAnimated = new AnimatedTexture(3f, new Point(3, 1), new Point(0, 0), new Point(56, 120), 180);

            this.millisecondsPerFrame = 30;
            this.timeSizeLastFrame = 0;
            this.drift = false;
            this.distanceDrift = 0;
            this.driftLeft = 0;
            this.crash = false;
            this.timeCrash = 0;
            this.pathCrash = new Vector2(0,0);
        }

        public void Load(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Car\\" + "derrape");
            this.texture2 = content.Load<Texture2D>("Car\\" + "derrape2");
            this.pointLeft = content.Load<Texture2D>("Car\\" + "punto");
            this.pointRight = content.Load<Texture2D>("Car\\" + "punto");
            this.shadow = content.Load<Texture2D>("Car\\" + "sombra");
            this.textureCrash = content.Load<Texture2D>("Car\\" + "crash");
            this.textureAnimated.Load(content, "carSheet");
        }

        public void UpdateCar(GameTime gameTime, float speed)
        {
            if (!drift && !crash)
            {
                this.distanceDrift = 0;
                if (speed > 3f)
                {
                    speed = 3f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    this.position.X -= speed;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    this.position.X += speed;
                }
                if (speed > 0f)
                {
                    if (!textureAnimated.getFrame().Y.Equals(1))
                    {
                        textureAnimated.ChangeFrame(new Point(0, 1));
                    }
                }
                else
                {
                    textureAnimated.ChangeFrame(new Point(0, 0));
                }
                textureAnimated.UpdateFrame(gameTime);
            }
            else
            {
                //el nuevo método para driftear
            }
            
        }

        public void UpdateDrift(GameTime gameTime, float speed)
        {
            
            if (drift && !crash)
            {
               
                this.timeSizeLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (this.timeSizeLastFrame > this.millisecondsPerFrame)
                {
                    this.timeSizeLastFrame -= this.millisecondsPerFrame;
                    if (this.distanceDrift < 30)
                    {
                        if (this.driftLeft == 1)
                        {
                            this.position.X -= 1;
                        }
                        else
                        {
                            this.position.X += 1;
                        } 
                        this.distanceDrift++;
                    }
                    else
                    {
                        this.drift = false;
                    }
                }
            }
        }

        public void inCrash(GameTime gameTime)
        {
            if (this.crash)
            {
                this.timeSizeLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (this.timeSizeLastFrame > this.millisecondsPerFrame)
                {
                    this.timeSizeLastFrame -= this.millisecondsPerFrame;
                    if (timeCrash < 50)
                    {
                        timeCrash += 1;
                    }
                    else
                    {
                        this.crash = false;
                        timeCrash = 0;
                        this.position.X = this.pathCrash.X + 180;
                        
                    }
                }
                
            }
            
        }

        public void changeCrash(Vector2 pathCrash)
        {
            if (!this.crash)
            {
                crash = true;
                this.pathCrash = pathCrash;
            }
        }

        public bool getCrash()
        {
            return this.crash;
        }
        public Rectangle getRectangleCar()
        {
            Rectangle playerRect = new Rectangle(
                (int)position.X + 0,
                (int)position.Y + 0,
                55 - (0 * 2),
                120 - (0 * 2));

            return playerRect;
        }

        public void changePosition50()
        {
            position.X += 55;
        }

        public bool getDrift()
        {
            return this.drift;
        }

        public void changeDrift(Vector2 enemy)
        {
            if (!drift)
            {
                this.drift = true;
                if (enemy.X > this.position.X)
                {
                    this.driftLeft = 1;
                    this.position.X -= 70;
                }
                else
                {
                    this.driftLeft = 0;
                }
            }
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public void drawCar(SpriteBatch batch)
        {
            if (!drift && !crash)
            {
                //batch.Draw(shadow, new Vector2(this.position.X + 50, this.position.Y), Color.White);
                textureAnimated.DrawFrame(batch, this.position, 0, 0);      
            }
            else if (!crash)
            {
                if (this.driftLeft == 1)
                {
                    batch.Draw(texture2, this.position, Color.White);
                }
                else
                {
                    batch.Draw(texture, this.position, Color.White);
                }
            }
            else
            {
                batch.Draw(this.textureCrash, this.position, Color.White);
            }
            
            //batch.Draw(pointLeft, this.position, Color.White);
            //batch.Draw(pointRight, new Vector2(this.position.X+55,this.position.Y), Color.White);
            
        }

        public void drawShadow(SpriteBatch batch)
        {
            if (!drift)
            {
                batch.Draw(shadow, new Vector2(this.position.X + 50, this.position.Y), Color.White);
            }
        }
    }
}
