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
    class AnimatedTexture
    {
        //Textura
        public Texture2D myTexture;
        //duración del frame
        public float timePerFrame;
        //tamaño del sprite sheet
        public Point sheetSize;
        //Posición del primer frame (fotograma) a dibujar
        public Point currentFrame;
        //Tamaño de cada frame del sprite sheet
        public Point frameSize;
        //Numero de milisegundos desde el último frame dibujado
        public int timeSizeLastFrame;
        //Número de milisegundos para que cambie el frame
        public int millisecondsPerFrame;

        public AnimatedTexture(float pf, Point ss, Point cf, Point fs, int mpf)
        {
            this.timePerFrame = pf;
            this.sheetSize = ss;
            this.currentFrame = cf;
            this.frameSize = fs;
            this.millisecondsPerFrame = mpf;
            this.timeSizeLastFrame = 0;
        }

        public void Load(ContentManager content, string textName)
        {
            myTexture = content.Load<Texture2D>("Car\\" + textName);
        }

        public void UpdateFrame(GameTime gameTime)
        {
            this.timeSizeLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (this.timeSizeLastFrame > this.millisecondsPerFrame)
            {
                this.timeSizeLastFrame -= this.millisecondsPerFrame;
                this.currentFrame.X++;
                if (this.currentFrame.X > this.sheetSize.X)
                {
                    this.currentFrame.X = 0;
                }
            }

        }

        public void ChangeFrame(Point cf)
        {
            this.currentFrame = cf;
        }

        public Point getFrame()
        {
            return this.currentFrame;
        }
        public void DrawFrame(SpriteBatch batch, Vector2 position, int landPositionX, int landPositionY)
        {
            batch.Draw(myTexture, new Vector2(position.X - landPositionX, position.Y - landPositionY),
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y),
                    Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
        }
    }
}
