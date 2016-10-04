using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame1945.Enums;
using Monogame1945.GameObjects;
using Monogame1945.Scene;

namespace Monogame1945
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public readonly GraphicsDeviceManager graphicsManager;
        private SpriteBatch spriteBatch;
        private GameState CurrentGameState { get; set; }
        private AirPlane plane;
        DrawableGameComponent gamePlayScreen;
        DrawableGameComponent screen;

        public Game1()
        {
            graphicsManager = new GraphicsDeviceManager(this);
            graphicsManager.PreferredBackBufferHeight = 760;
            graphicsManager.PreferredBackBufferWidth = 1020;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            CurrentGameState = GameState.IN_GAME;
            gamePlayScreen = new GameplayScene(this);
           
            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            plane = new AirPlane(this, Content.Load<Texture2D>("player"), graphicsManager.GraphicsDevice, spriteBatch);
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            switch (CurrentGameState)
            {
                case GameState.IN_GAME:
                    if (!(screen is GameplayScene))
                    {

                        screen = gamePlayScreen;
                        Components.Clear();
                        Components.Add(screen);
                    }
                    break;
                case GameState.SETTINGS:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.IN_GAME:
           
                    break;
                case GameState.SETTINGS:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}