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
    class Enemy
    {
        private Texture2D texture;
        private Vector2 position;
        private Texture2D shadow;

        private float speed;

        private int type;

        private int vehicle;

        private AnimatedTexture textureAnimated;

        public Enemy(Vector2 position, int type, int vehicle)
        {
            this.position = position;

            this.speed = 3f;

            this.type = type;

            this.vehicle = vehicle;

            this.textureAnimated = new AnimatedTexture(3f, new Point(3, 0), new Point(0, 0), new Point(56, 120), 180);
        }

        public void Load(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Car\\" + "carEnemy");
            switch (vehicle)
            {
                case(0):
                    this.textureAnimated.Load(content, "carEnemySheet");
                    this.shadow = content.Load<Texture2D>("Car\\" + "sombraEnemyCar");
                    break;
                case(1):
                    this.textureAnimated.Load(content, "carEnemySheet2");
                    this.shadow = content.Load<Texture2D>("Car\\" + "sombraEnemyTruck");
                    break;
            }
            
        }


        public void UpdateCar(GameTime gameTime, Vector2 positionRoad, bool pushUp, float speedRoad)
        {
            if (type == 0)
            {
                if (this.position.X - positionRoad.X < 100)
                {
                    this.position.X += speed;
                }
                else if (this.position.X - positionRoad.X > 120)
                {
                    this.position.X -= speed;
                }
            }
            else
            {
                if (positionRoad.X - this.position.X < 150)
                {
                    this.position.X -= speed;
                }
                else if (positionRoad.X - this.position.X > 170)
                {
                    this.position.X += speed;
                }
            }
           
           
            speedRoad += 4f;
            if (speedRoad > 5f)
            {
                speedRoad = 5f;
            }
            if (pushUp)
            {
                this.position.Y += speedRoad;
                this.speed = speedRoad;
            }
            else
            {
                if (this.speed > 0)
                {
                    this.speed -= 0.01f;
                    this.position.Y -= 8f;
                }
                else
                {
                    this.position.Y -= 8f;
                }
            }
            this.textureAnimated.UpdateFrame(gameTime);


        }

        public int getType()
        {
            return this.type;
        }

        public Rectangle getRectangleEnemy()
        {
            Rectangle playerRect = new Rectangle(
                (int)position.X + 0,
                (int)position.Y + 0,
                56 - (0 * 2),
                120 - (0 * 2));

            return playerRect;
        }
        public Vector2 getPosition()
        {
            return this.position;
        }

        public void drawEnemy(SpriteBatch batch)
        {
            //batch.Draw(this.texture, this.position, Color.White);
            batch.Draw(shadow, new Vector2(this.position.X + 50, this.position.Y), Color.White);
            textureAnimated.DrawFrame(batch, this.position, 0, 0);
        }
    }

}
