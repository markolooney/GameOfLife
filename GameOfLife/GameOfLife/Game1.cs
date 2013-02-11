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

namespace GameOfLife
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        //to access òbjects ect. from game1 
        private static Game1 instance;
        //set up entities as a list as i had originally planned to have a few entities but 
        //i realised i could do it with only one reason i left it was in case i did decide
        //to add more i wouldn't have to add more code to load ect. as it checks each enitity
        public List<GameEntity> entities = new List<GameEntity>();
        public int time { get; set; }
        
        GameEntity cell;

        public List<GameEntity> Entities
        {
            get { return entities; }
        }
        
        public static Game1 Instance
        {
            get
            {
                return instance;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;
            //this is setting the original speed of the generations 
            time = 50;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 320;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cell = new Cell();
            entities.Add(cell);
            base.Initialize();
            this.IsMouseVisible = true;
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //goes through list and loads content
            entities.ForEach(x => x.LoadContent());
           
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //goes through list and updates content
            entities.ForEach(x => x.Update(gameTime));
            // TODO: Add your update logic here
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / time);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
             spriteBatch.Begin();
             //goes through list and draws content
             entities.ForEach(x => x.Draw(gameTime));

             spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
