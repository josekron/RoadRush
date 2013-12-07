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
    class Enemies
    {
        private List<Enemy> enemies;
        private int numEnemies;
        Road road;

        public Enemies(int numEnemies, Road road)
        {
            this.road = road;
            this.numEnemies = numEnemies;

            enemies = new List<Enemy>();
            int offset = -1;
            Random rd = new Random();
            for (int i = 0; i < this.numEnemies; i++)
            {
                //enemies.Add(new Enemy(new Vector2(300, 800*offset),rd.Next(0,1)));
                int n = rd.Next(0, 4);
                //rd = new Random();
                int m = rd.Next(0, 4);
                if (n < 2)
                {
                    if (m < 2)
                    {
                        enemies.Add(new Enemy(new Vector2(300, 300 * offset), 1, 1));
                    }
                    else
                    {
                        enemies.Add(new Enemy(new Vector2(300, 300 * offset), 0, 1));
                    }
                    
                }
                else
                {
                    if (m < 2)
                    {
                        enemies.Add(new Enemy(new Vector2(300, 300 * offset), 1, 0));
                    }
                    else
                    {
                        enemies.Add(new Enemy(new Vector2(300, 300 * offset), 0, 0));
                    }
                }
                
                offset-=2;
            }
        }

        public void Load(ContentManager content)
        {
            for (int i = 0; i < this.numEnemies; i++)
            {
                enemies[i].Load(content);
            }
        }
        public void UpdateEnemies(GameTime gameTime, bool pushUp, float speedRoad)
        {
            for (int i = 0; i < this.numEnemies; i++)
            {
                enemies[i].UpdateCar(gameTime, road.collisionPathEnemy(enemies[i].getPosition(), enemies[i].getType()), pushUp, speedRoad);
            }
            
        }

        public bool collisionEnemies(GameTime gameTime, Rectangle rectCar)
        {
            for (int i = 0; i < this.numEnemies; i++)
            {
                if (rectCar.Intersects(enemies[i].getRectangleEnemy()))
                {
                    return true;
                }
            }
            return false;
        }

        public Vector2 GetCollisionEnemies(GameTime gameTime, Rectangle rectCar)
        {
            for (int i = 0; i < this.numEnemies; i++)
            {
                if (rectCar.Intersects(enemies[i].getRectangleEnemy()))
                {
                    return enemies[i].getPosition();
                }
            }
            return new Vector2(0,0);
        }

        public void drawEnemies(SpriteBatch batch)
        {
            for (int i = 0; i < this.numEnemies; i++)
            {
                enemies[i].drawEnemy(batch);
            }
        }

        
    }
}
