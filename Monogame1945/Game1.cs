using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame1945.Enums;
using Monogame1945.GameObjects;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace Monogame1945
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        private Camera2D camera;
        private GameState CurrentGameState { get; set; }
        private ParticleEffect particleEffect { get; set; }
        private AirPlane plane;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
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

            var viewportAdapter = new BoxingViewportAdapter(Window, graphicsDeviceManager, 800, 480);
            camera = new Camera2D(viewportAdapter);

            var sprinkleTexture = Content.Load<Texture2D>("sprinkle");
            ParticleInit(new TextureRegion2D(sprinkleTexture));

            plane = new AirPlane(Content.Load<Texture2D>("player"), graphicsDeviceManager.GraphicsDevice, spriteBatch,
                particleEffect);
        }

        private void ParticleInit(TextureRegion2D textureRegion)
        {
            particleEffect = new ParticleEffect
            {
                Emitters = new[]
                {
                    new ParticleEmitter(500, TimeSpan.FromSeconds(2.5), Profile.Point())
                    {
                        TextureRegion = textureRegion,
                        Parameters = new ParticleReleaseParameters
                        {
                            Speed = new RangeF(50, 0f),
                            Quantity = 3,
                            Rotation = new RangeF(-1f, 1f),
                            Scale = new RangeF(6.0f, 8.0f)
                        },
                        Modifiers = new IModifier[]
                        {
                            new ColorInterpolator2
                            {
                                InitialColor = new HslColor(0.9f, 0.33f, 0.24f),
                                FinalColor = new HslColor(.6f, 1f, .9f)
                            },
                            new RotationModifier {RotationRate = -2.1f},
                            new RectangleContainerModifier {Width = 800, Height = 480},
                            new LinearGravityModifier {Direction = Axis.Up, Strength = 30f}
                        }
                    }
                }
            };
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
        /// <param name="gt">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gt)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            UpdateComponents(gt);
            base.Update(gt);
        }

        private void UpdateComponents(GameTime gt)
        {
            switch (CurrentGameState)
            {
                case GameState.IN_GAME:
                    particleEffect.Update((float) gt.ElapsedGameTime.TotalSeconds);
                    plane.Update(gt);
                    break;
                case GameState.SETTINGS:
                    break;
            }
        }

        protected override void Draw(GameTime gt)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null,
                null, null, camera.GetViewMatrix());
            DrawComponents(gt);
            spriteBatch.Draw(particleEffect);
            spriteBatch.End();

            base.Draw(gt);
        }

        private void DrawComponents(GameTime gt)
        {
            switch (CurrentGameState)
            {
                case GameState.IN_GAME:
                    plane.Draw(gt);
                    break;
                case GameState.SETTINGS:
                    break;
            }
        }
    }
}