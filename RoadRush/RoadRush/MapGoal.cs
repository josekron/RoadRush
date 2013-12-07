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
    class MapGoal
    {
        private Texture2D textureGoal;
        private Texture2D textureStart;
        private Texture2D textureGo;
        private Texture2D textureRoad;

        private int numSectors;

        private Vector2 positionGo;
        private int nSector;

        public MapGoal(int numSectors)
        {
            this.numSectors = numSectors;
            this.positionGo = new Vector2(30, 360);
            this.nSector = 0;
        }

        public void Load(ContentManager content)
        {
            this.textureGoal = content.Load<Texture2D>("Road\\" + "BanderaGoal");
            this.textureStart = content.Load<Texture2D>("Road\\" + "BanderaStart");
            this.textureGo = content.Load<Texture2D>("Road\\" + "cocheGo");
            this.textureRoad = content.Load<Texture2D>("Road\\" + "carretera");
        }

        public void UpdateMapGoal(GameTime gameTime, int nSector)
        {
            if (nSector != this.nSector)
            {
                this.positionGo.Y -= 1;
            }
            this.nSector = nSector;

            
        }

        public void drawMapGoal(SpriteBatch batch)
        {
            batch.Draw(this.textureRoad, new Vector2(30, 75), Color.White);
            batch.Draw(this.textureGo, this.positionGo, Color.White);
            batch.Draw(this.textureStart, new Vector2(30, 360), Color.White);
            batch.Draw(this.textureGoal, new Vector2(30, 50), Color.White);
            
        }
    }
}
