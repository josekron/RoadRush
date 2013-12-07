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
    class Fuel
    {
        private int fuel;
        private Texture2D texture;

        private int timeDecrementLife;
        private int millisecondsPerFrame;

        public Fuel(int fuel)
        {
            this.fuel = fuel;
            this.timeDecrementLife = 0;
            this.millisecondsPerFrame = 500;
        }

        public void Load(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Road\\" + "cuadrado");
        }

        public void UpdateFuel(GameTime gameTime)
        {

            this.timeDecrementLife += gameTime.ElapsedGameTime.Milliseconds;
            if (this.timeDecrementLife > this.millisecondsPerFrame)
            {
                this.timeDecrementLife -= this.millisecondsPerFrame;
                this.fuel--;
            }
        }

        public int getFuel()
        {
            return this.fuel;
        }

        public void drawFuel(SpriteBatch batch, SpriteFont font)
        {
            batch.Draw(this.texture, new Vector2(720, 400), Color.White);
            batch.DrawString(font, "FUEL", new Vector2(723, 375), Color.Red);
            if (this.fuel >= 100)
            {
                batch.DrawString(font, fuel.ToString(), new Vector2(727, 410), Color.Black);
            }
            else
            {
                batch.DrawString(font, fuel.ToString(), new Vector2(732, 410), Color.Black);
            }
            
        }
    }
}
