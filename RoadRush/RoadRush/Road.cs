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
    class Road
    {
        private List<Sector> road;
        private int numSectors;
        private int typeSectors;

        private Vector2 position;

        private float speed;
        private float speedStop;
        private float timeSpeed;

        private float boost;
        private Texture2D textureBoost;

        private List<Decoration> decoration;
        private int numDecoration;
        private String nameDecoration;

        public Road(int numSectors, int typeSectors, Vector2 position, String nameDecoration, int numDecoration)
        {
            this.numSectors = numSectors;
            this.typeSectors = typeSectors;
            this.position = position;
            this.speed = 0f;
            this.speedStop = 0f;
            this.timeSpeed = 1;
            this.boost = 10f;

            road = new List<Sector>();
            Random rd = new Random();
            int offsetSector=0;
            int curves = 0;


            for (int i = 0; i < 5; i++)//los primeros 5 sectores son rectos
            {
                road.Add(new Sector(0, new Vector2(this.position.X - offsetSector, this.position.Y - (240 * i))));
            }

            int rects = 0;
            int n = rd.Next(0,this.typeSectors);
            if (n == 1)
            {
                curves--;
            }
            else if(n==2)
            {
                curves++;
            }
            for (int i = 5; i < this.numSectors; i++)
            {
                road.Add(new Sector(n, new Vector2(this.position.X-offsetSector, this.position.Y - (240 * i))));
                switch (n)
                {
                    case (1):
                        offsetSector += 50;
                        break;
                    case (2):
                        offsetSector -= 50;
                        break;
                }
                if (rects > 0)
                {
                    n = 0;
                    rects--;
                }
                else
                {
                    n = rd.Next(this.typeSectors);
                    while (n == 1 && curves == -3)
                    {
                        n = rd.Next(this.typeSectors);
                    }
                    while (n == 2 && curves == 3)
                    {
                        n = rd.Next(this.typeSectors);
                    }

                    if (n == 1)
                    {
                        curves--;
                    }
                    else if (n == 2)
                    {
                        curves++;
                    }
                    if (n == 0)
                    {
                        rects = 6;
                    }
                }             
            }
            road.Add(new Sector(0, new Vector2(this.position.X - offsetSector, this.position.Y - (240 * numSectors))));
            road.Add(new Sector(0, new Vector2(this.position.X - offsetSector, this.position.Y - (240 * numSectors+1))));

            this.nameDecoration = nameDecoration;
            this.numDecoration = numDecoration;
            this.decoration = new List<Decoration>();
            int counter = 250;
            for (int i = 0; i < this.numDecoration; i++)
            {
                decoration.Add(new Decoration(this.nameDecoration, new Vector2(-20, counter)));
                counter = counter - rd.Next(240, 800);
            }
        }

        public bool lastSector(Vector2 positionCar)
        {
            Sector sector = road[this.numSectors-1];
            return Math.Abs(sector.getPosition().Y-positionCar.Y) < 100;
        }
        public void Load(ContentManager content)
        {

            for (int i = 0; i < this.numSectors; i++)
            {
                road[i].Load(content);
            }
            road[this.numSectors-1].Load(content, 3);
            road[this.numSectors].Load(content);
            road[this.numSectors + 1].Load(content);
            foreach (Decoration deco in decoration)
            {
                deco.Load(content);
            }
            this.textureBoost = content.Load<Texture2D>("Road\\" + "nitro");
        }

        public void UpdateRoad(GameTime gameTime, bool crash)
        {
            if (crash)
            {
                speed = 0f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.speedStop = 0.001f;
                if (this.speed < 14f)
                {
                    this.speed += speedStop * timeSpeed;
                    timeSpeed++;
                }
                else
                {
                    timeSpeed = 1;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.speedStop = 0.01f;
                if (this.speed > 0f)
                {
                    this.speed -= speedStop * timeSpeed;
                    timeSpeed++;
                }
                else
                {
                    this.speed = 0f;
                }
            }
            else
            {
                this.speedStop = 0.0005f;
                if (this.speed > 0f)
                {
                    this.speed -= speedStop * timeSpeed;
                    timeSpeed++;
                }
                else
                {
                    this.speed = 0f;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && (Keyboard.GetState().IsKeyDown(Keys.X) && this.boost > 0f) && !crash)
            {
                foreach (Sector sector in road)
                {
                    Vector2 positionAct = sector.getPosition();
                    sector.setPosition(new Vector2(positionAct.X, positionAct.Y + this.speed+100f));
                }
                this.boost -= 0.05f;
            }
            else
            {
                foreach (Sector sector in road)
                {
                    Vector2 positionAct = sector.getPosition();
                    sector.setPosition(new Vector2(positionAct.X, positionAct.Y + this.speed));
                }
            }

            for (int i = 0; i < this.numDecoration; i++)
            {
                decoration[i].setPosition(this.speed);
            }


            
        }

        public bool collisionWithPaths(Vector2 positionLeft, Vector2 positionRight)
        {
            bool collision = false;
            foreach (Sector sector in road)
            {
                if (Math.Abs(position.Y - sector.getPosition().Y) < 400)
                {
                    collision = sector.getCollision(positionLeft, positionRight);
                    if (collision)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Vector2 collisionWithPaths2(Vector2 positionLeft, Vector2 positionRight)
        {
            foreach (Sector sector in road)
            {
                if (Math.Abs(position.Y - sector.getPosition().Y) < 400)
                {
                    return sector.getCollision2(positionLeft, positionRight);
                }
            }
            return new Vector2(0,0);
        }
        public int collisionWithPathsNumSector(Vector2 positionLeft, Vector2 positionRight)
        {
            int n = 0;
            foreach (Sector sector in road)
            {
                if (Math.Abs(position.Y - sector.getPosition().Y) < 400)
                {
                    return n;
                }
                n++;
            }
            return n;
        }

       
        public bool pushKeyUp()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Up);
        }

        public float getSpeed()
        {
            return this.speed;
        }

        public int collisionWithEnemy(Vector2 positionEnemy)
        {
            foreach (Sector sector in road)
            {
                if (Math.Abs(sector.getPosition().Y - positionEnemy.Y) <= 240)
                {
                    return sector.getType();
                }
            }
            return -1;
        }

        public Vector2 collisionPathEnemy(Vector2 positionEnemy, int type)
        {
            if (type == 0)
            {
                foreach (Sector sector in road)
                {
                    if (Math.Abs(sector.getPosition().Y - positionEnemy.Y) <= 240)
                    {
                        return sector.getPositionCeroLeft();
                    }
                }
            }
            else
            {
                foreach (Sector sector in road)
                {
                    if (Math.Abs(sector.getPosition().Y - positionEnemy.Y) <= 240)
                    {
                        return sector.getPositionCeroRight();
                    }
                }
            }
            
            return positionEnemy;
        }

        public String getNameDecoration()
        {
            return this.nameDecoration;
        }

        public void drawRoad(SpriteBatch batch)
        {
            int pos = 0;
            foreach (Sector sector in road)
            {
                sector.drawSector(batch);
                sector.drawPoints(batch);
                pos-=240;
            }
        }

        public void drawDecoration(SpriteBatch batch)
        {
            foreach (Decoration deco in decoration)
            {
                deco.drawDecoration(batch);
            }
        }

        public void drawBoost(SpriteBatch batch, SpriteFont font)
        {
            batch.Draw(this.textureBoost, new Vector2(720, 300), Color.White);
            int b = (int)boost;
            batch.DrawString(font, b.ToString(), new Vector2(715, 330), Color.Black);
        }
    }
}
