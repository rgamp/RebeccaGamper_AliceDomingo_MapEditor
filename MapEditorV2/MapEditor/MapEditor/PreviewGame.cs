using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projet;
using MapEditor;

namespace Projet
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PreviewGame : Game
    {
        Texture2D Vide;
        Texture2D Mur;
        Texture2D Personnage;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private KeyboardState oldKBState;
        private MapEditor mapEditor;

        //Fenêtre
        public const int windowWidth = 800;
        public const int windowHeight = 600;

        //Position du background
        public Vector2 World;
        public Texture2D Texturebg;

        /* private Matrix modelMatrix; // world
         private Matrix view;
         private Matrix projection;*/

        int[,] mapData = new int[,]
      {
            { 1,1,1,1,1,1,1,1,1,1 },
            { 1,0,2,0,0,0,0,1,1,1 },
            { 1,0,0,0,0,2,0,0,0,1 },
            { 1,1,0,1,1,1,0,1,0,1 },
            { 1,1,0,1,1,1,2,1,2,1 },
            { 1,1,0,1,1,1,0,1,1,1 },
            { 1,0,0,2,0,0,0,0,0,1 },
            { 1,0,1,1,1,1,0,1,0,1 },
            { 1,2,0,0,0,1,0,0,2,1 },
            { 1,0,1,1,1,1,1,1,1,1 }

      };
     

        public PreviewGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Fenêtre
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
           
            

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // world position  

            World = new Vector2(0, 0);
            
            // TODO: Add your initialization logic here

            mapEditor = new MapEditor(this, 5, ref mapData);
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Vide = Content.Load<Texture2D>("tile_0");
            Mur = Content.Load<Texture2D>("tile_1");
            Personnage = Content.Load<Texture2D>("tile_2");

          
            for (int i = 0; i < 5; i++)
            {
                mapEditor.AddTile(i, Content.Load<Texture2D>("tile_" + i));
            }

            mapEditor.UpdateGrid();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState newKBState = Keyboard.GetState();
            if (newKBState.IsKeyDown(Keys.Tab)&& !oldKBState.IsKeyDown(Keys.Tab))
            {
                mapEditor.Active();
                IsMouseVisible = mapEditor.isActive;
            }
            // TODO: Add your update logic here

            mapEditor.Update();

            oldKBState = newKBState;

            //test
           /* view = Matrix.CreateLookAt(
               cameraPosition,
               cameraPosition + (forwardNormal * 10f),
               Vector3.Up
               );*/

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
       

            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            // spriteBatch.Draw(Mur, new Vector2(100, 100), Color.White);
            /*spriteBatch.Begin();
            spriteBatch.Draw(Mur, new Rectangle(0, 0, 800, 600), Color.White);
            spriteBatch.End();*/

            //this.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

          
            int mapPosX = 300;
            int mapPosZ= 125;
            for (int line = 0; line < 10; line++)
            {
                for (int column = 0; column < 10; column++)
                {
                    int id = mapData[line, column];
                    int x = mapPosX + (column * 33);
                    int z = mapPosZ + (line * 33);
                    if (id == 0)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(Vide, new Rectangle(x,z,32,32), Color.White);
                        spriteBatch.End();
                        /*
                        Matrix wallMatrix = Matrix.CreateWorld(new Vector3(x, 0, z), Vector3.Forward, Vector3.Up);
                        DrawModel(Mur, wallMatrix, view, projection);*/
                    }
                    else if (id == 1)
                    {

                        spriteBatch.Begin();
                        spriteBatch.Draw(Mur, new Rectangle(x, z, 32, 32), Color.White);
                        spriteBatch.End();
                    }
                    else if (id == 2)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(Personnage, new Rectangle(x, z, 32, 32), Color.White);
                        spriteBatch.End();
                    }
                }
            }

            spriteBatch.Begin();
            mapEditor.Draw(spriteBatch);
            spriteBatch.End();
            
            // Set the depth buffer on again
            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            
            base.Draw(gameTime);
        }
    }
}
