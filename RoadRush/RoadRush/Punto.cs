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
    class Punto
    {
        private List<Vector2> nodos = new List<Vector2>();
        private Int32 totalNodos = 0;

        private Texture2D punto;
        private Texture2D triangulo;

        public Punto()
        {
            nodos = new List<Vector2>();
        }

        public List<Vector2> Nodos
        {
            get
            {
                return nodos;
            }
        }
        public void changePosition(Vector2 position, int type)
        {
            if (type == 0)
            {
                nodos[0] = position;
                nodos[1] = new Vector2(position.X, position.Y + 240);
            }
            else if (type == 1)
            {
                nodos[0] = new Vector2(position.X-50, position.Y);
                nodos[1] = new Vector2(position.X,position.Y+240);
            }
            else if (type == 2)
            {
                nodos[0] = new Vector2(position.X + 50, position.Y);
                nodos[1] = new Vector2(position.X, position.Y + 240);
            }
            
        }
        public void addNodo(Vector2 position)
        {
            nodos.Add(position);
            totalNodos++;
        }

        public int getTotalNodos()
        {
            return this.totalNodos;
        }
        public Vector2 getNodoCero()
        {
            return nodos[0];
        }
        public void Load(ContentManager content)
        {
            this.punto = content.Load<Texture2D>("Car\\" + "punto");
            this.triangulo = content.Load<Texture2D>("Car\\" + "triangulo");
        }
        public bool getCollision(Vector2 position, bool left)
        {
            for (int n = 0; n < this.totalNodos; n++)
            {
                Vector2 tVec;
                tVec = nodos[n];
                if (tVec != Vector2.Zero)
                {
                    /*if (tVec.X>positionLeft.X)
                    {
                       // return true;
                    }*/
                }
                if (n < totalNodos - 1)
                {
                    Vector2 nVec;
                    nVec = nodos[n + 1];
                    for (int x = 0; x < 20; x++)
                    {
                        Vector2 temp = Vector2.Lerp(tVec, nVec, (float)x / 20.0f);
                        if (left)
                        {
                            if (temp.X > position.X && Math.Abs(temp.Y - position.Y) <= 5)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (temp.X < position.X && Math.Abs(temp.Y - position.Y) <= 5)
                            {
                                return true;
                            }
                        }
                        
                    }
                }
            }
            return false;
        }

        
        public void drawPunto(SpriteBatch batch)
        {
            //dibujar cada nodo
            for (int n = 0; n < this.totalNodos; n++)
            {
                Vector2 tVec;
                tVec = nodos[n];
                if (tVec != Vector2.Zero)
                {
                    batch.Draw(triangulo, tVec, Color.White);
                }
                // dibujar algunos valores interpolados
                /*if (n < totalNodos - 1)
                {
                    Vector2 nVec;
                    nVec = nodos[n + 1];
                    for (int x = 0; x < 20; x++)
                    {
                        Vector2 temp = Vector2.Lerp(tVec, nVec, (float)x / 20.0f);
                        batch.Draw(punto, temp, Color.White);
                    }
                }*/
            }

        }

    }

}
