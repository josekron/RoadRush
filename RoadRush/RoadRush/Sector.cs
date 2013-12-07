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
    class Sector
    {
        private Texture2D texture;
        private int type;
        private Vector2 position;

        private List<Punto> path;

        public Sector(int type, Vector2 position)
        {
            this.type = type;
            this.position = position;

            path = new List<Punto>();
            if (this.type == 0)
            {
                Punto pLeft = new Punto();
                pLeft.addNodo(this.position);
                pLeft.addNodo(new Vector2(this.position.X, this.position.Y + 240));
                path.Add(pLeft);
                Punto pRight = new Punto();
                pRight.addNodo(new Vector2(this.position.X+400, this.position.Y));
                pRight.addNodo(new Vector2(this.position.X + 400, this.position.Y + 240));
                path.Add(pRight);
            }
            else if (this.type == 1)
            {
                Punto pLeft = new Punto();
                pLeft.addNodo(new Vector2(this.position.X-50, this.position.Y));
                pLeft.addNodo(new Vector2(this.position.X, this.position.Y + 240));
                path.Add(pLeft);
                Punto pRight = new Punto();
                pRight.addNodo(new Vector2(this.position.X-50, this.position.Y));
                pRight.addNodo(new Vector2(this.position.X + 400, this.position.Y + 240));
                path.Add(pRight);
            }
            else if (this.type == 2)
            {
                Punto pLeft = new Punto();
                pLeft.addNodo(new Vector2(this.position.X + 50, this.position.Y));
                pLeft.addNodo(new Vector2(this.position.X, this.position.Y + 240));
                path.Add(pLeft);
                Punto pRight = new Punto();
                pRight.addNodo(new Vector2(this.position.X + 50, this.position.Y));
                pRight.addNodo(new Vector2(this.position.X + 400, this.position.Y + 240));
                path.Add(pRight);
            }
            
        }

        public void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Road\\" + "sector"+this.type);
            foreach (Punto p in path)
            {
                p.Load(content);
            }
        }
        public void Load(ContentManager content, int n)
        {
            texture = content.Load<Texture2D>("Road\\" + "sector" + n);
            foreach (Punto p in path)
            {
                p.Load(content);
            }
        }

        
        public Vector2 getPosition()
        {
            return this.position;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;

            if (this.type == 0)
            {
                path[0].changePosition(position,0);
                path[1].changePosition(new Vector2(position.X + 400, position.Y),0);
            }
            else if (this.type == 1)
            {
                path[0].changePosition(position, 1);
                path[1].changePosition(new Vector2(position.X + 400, position.Y),1);
            }
            else if (this.type == 2)
            {
                path[0].changePosition(position, 2);
                path[1].changePosition(new Vector2(position.X + 400, position.Y), 2);
            }
            

        }

        public bool getCollision(Vector2 positionLeft, Vector2 positionRight)
        {
            return path[0].getCollision(positionLeft, true) || path[1].getCollision(positionRight, false);
        }

        public Vector2 getCollision2(Vector2 positionLeft, Vector2 positionRight)
        {
            return path[0].getNodoCero();
        }

        public int getType()
        {
            return this.type;
        }

        public void drawSector(SpriteBatch batch)
        {
            batch.Draw(this.texture, this.position, Color.White);   
        }

        public Vector2 getPositionCeroLeft()
        {
            return path[0].getNodoCero();
        }

        public Vector2 getPositionCeroRight()
        {
            return path[1].getNodoCero();
        }
        public void drawPoints(SpriteBatch batch)
        {
            foreach (Punto p in path)
            {
                if (p != null && p.getTotalNodos() > 0)
                {
                    p.drawPunto(batch);
                }
            }
        }


    }
}
