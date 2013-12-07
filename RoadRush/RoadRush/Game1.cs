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
    /// <summary>
    /// Tipo principal del juego
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Road road;
        Car car;
        Enemies enemies;
        Fuel fuel;
        SpriteFont font;
        MapGoal mapGoal;
        Menu menu;
        bool optionInstruction;
        Texture2D instructions;
        Texture2D gameover;
        Texture2D youwin;
        Song songGame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Permite al juego realizar cualquier inicialización necesaria antes de empezar a ejecutarse.
        /// En este punto puede consultar los servicios necesarios y cargar el contenido no gráfico
        /// relacionado. Al llamar a base.Initialize, se enumeran e inicializan
        /// los componentes.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: agregue aquí su lógica de inicialización

            road = new Road(300, 3, new Vector2(200,480), "palmera", 100);
            car = new Car();
            enemies = new Enemies(35, road);
            fuel = new Fuel(150);
            mapGoal = new MapGoal(300);
            menu = new Menu();
            this.optionInstruction = false;

            base.Initialize();
        }

        public void InitializeForest()
        {
            // TODO: agregue aquí su lógica de inicialización

            road = new Road(300, 3, new Vector2(200, 480), "roca", 100);
            car = new Car();
            enemies = new Enemies(35, road);
            fuel = new Fuel(150);
            mapGoal = new MapGoal(300);
            menu = new Menu();
            this.optionInstruction = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent se llama una vez por juego y permite cargar
        /// todo el contenido.
        /// </summary>
        protected override void LoadContent()
        {
            // Cree un SpriteBatch nuevo para dibujar texturas.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content para cargar aquí el contenido del juego

            road.Load(Content);
            car.Load(Content);
            enemies.Load(Content);
            fuel.Load(Content);
            font = this.Content.Load<SpriteFont>("Road\\fuel");
            mapGoal.Load(Content);
            menu.Load(Content);
            instructions = Content.Load<Texture2D>("Menu\\" + "instrucciones");
            gameover = Content.Load<Texture2D>("Menu\\" + "gameover");
            youwin = Content.Load<Texture2D>("Menu\\" + "youwin");

            songGame = Content.Load<Song>("Menu\\" + "music");
            MediaPlayer.Play(songGame);


        }

        /// <summary>
        /// UnloadContent se llama una vez por juego y permite descargar
        /// todo el contenido.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: descargue aquí todo el contenido que no pertenezca a ContentManager
        }

        /// <summary>
        /// Permite al juego ejecutar lógica para, por ejemplo, actualizar el mundo,
        /// buscar colisiones, recopilar entradas y reproducir audio.
        /// </summary>
        /// <param name="gameTime">Proporciona una instantánea de los valores de tiempo.</param>
        protected override void Update(GameTime gameTime)
        {
            // Permite salir del juego
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: agregue aquí su lógica de actualización
            if (this.optionInstruction)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    this.optionInstruction = false;
                }
            }
            else if (!menu.getMenuActive())
            {
                if (fuel.getFuel() > 0 && !road.lastSector(car.getPosition()))
                {
                    if (!road.collisionWithPaths(car.getPosition(), new Vector2(car.getPosition().X + 55, car.getPosition().Y))
                        && !car.getCrash())
                    {
                        car.UpdateCar(gameTime, road.getSpeed());
                        if (enemies.collisionEnemies(gameTime, car.getRectangleCar()))
                        {
                            car.changeDrift(enemies.GetCollisionEnemies(gameTime, car.getRectangleCar()));

                        }
                        car.UpdateDrift(gameTime, road.getSpeed());
                        mapGoal.UpdateMapGoal(gameTime, road.collisionWithPathsNumSector(car.getPosition(), new Vector2(car.getPosition().X + 55, car.getPosition().Y)));
                    }
                    else
                    {
                        car.changeCrash(road.collisionWithPaths2(car.getPosition(), new Vector2(car.getPosition().X + 55, car.getPosition().Y)));
                    }
                    car.inCrash(gameTime);
                    road.UpdateRoad(gameTime, car.getCrash());
                    enemies.UpdateEnemies(gameTime, road.pushKeyUp(), road.getSpeed());
                    fuel.UpdateFuel(gameTime);
                }
                else if (road.lastSector(car.getPosition()))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        menu.changeMenu();
                    }
                }
                else if (fuel.getFuel() <= 0)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        menu.changeMenu();
                    }
                }
               
            }
            
            else
            {
                int optionMenu = menu.updateMenu(gameTime);
                if (optionMenu == 1)
                {
                    this.Initialize();
                    menu.changeMenu();

                }
                else if (optionMenu == 2)
                {
                    this.InitializeForest();
                    menu.changeMenu();
                }
                else if (optionMenu == 3)
                {
                    optionInstruction = true;
                }
                else if (optionMenu == 4)
                {
                    Exit();
                }
            }
            
            

            
            
            

            base.Update(gameTime);
        }

        /// <summary>
        /// Se llama cuando el juego debe realizar dibujos por sí mismo.
        /// </summary>
        /// <param name="gameTime">Proporciona una instantánea de los valores de tiempo.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (road.getNameDecoration().Equals("palmera"))
            {
                GraphicsDevice.Clear(Color.SandyBrown);
            }
            else if (road.getNameDecoration().Equals("roca"))
            {
                GraphicsDevice.Clear(Color.ForestGreen);
            }
            

            // TODO: agregue aquí el código de dibujo
            spriteBatch.Begin();

            if (this.optionInstruction)
            {
                spriteBatch.Draw(instructions, new Vector2(0, 0), Color.White);
            }
            else if (!menu.getMenuActive())
            {
                road.drawRoad(spriteBatch);
                car.drawShadow(spriteBatch);
                enemies.drawEnemies(spriteBatch);
                car.drawCar(spriteBatch);
                road.drawDecoration(spriteBatch);

                fuel.drawFuel(spriteBatch, font);
                mapGoal.drawMapGoal(spriteBatch);
                road.drawBoost(spriteBatch, font);

                if (road.lastSector(car.getPosition()))
                {
                    spriteBatch.Draw(youwin, new Vector2(0, 0), Color.White);
                }
                else if (fuel.getFuel() <= 0)
                {
                    spriteBatch.Draw(this.gameover, new Vector2(0, 0), Color.White);
                }

                
            }
            else
            {
                menu.drawMenu(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
