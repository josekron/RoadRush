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
    class Decoration
    {
        private Texture2D texture;
        private Vector2 position;
        private String name;

        public Decoration(String name, Vector2 position)
        {
            this.name = name;
            this.position = position;
        }

        public void Load(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Road\\" + name);
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public void setPosition(float speedRoad)
        {
            this.position.Y += speedRoad;
        }

        public String getName()
        {
            return this.name;
        }

        public void drawDecoration(SpriteBatch batch)
        {
            batch.Draw(this.texture, this.position, Color.White);
        }
    }
}
