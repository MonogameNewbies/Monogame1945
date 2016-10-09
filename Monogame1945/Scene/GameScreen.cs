using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Monogame1945.GameObjects;
using MonoGame.Extended.Screens;

namespace Monogame1945.Scene
{
    public class GameScreen : MenuScreen
    {
        SpriteBatch spriteBatch;
        ContentManager Content;
        IServiceProvider serviceProvider;
        AirPlane airPlane;
        Game game;
        GraphicsDevice graphicsDevice;
        public GameScreen(IServiceProvider serviceProvider,Game game) : base(serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.game = game;

           
        }

        public override void Initialize()
        {
            base.Initialize();
            Content = new ContentManager(serviceProvider, "Content");
        }

        public override void LoadContent()
        {
            IGraphicsDeviceService graphicsDeviceService = (IGraphicsDeviceService)serviceProvider.GetService(typeof(IGraphicsDeviceService));
            graphicsDevice = graphicsDeviceService.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            airPlane = new AirPlane(game, Content, graphicsDevice, spriteBatch);
            base.LoadContent();
            

        }

        public override void Update(GameTime gameTime)
        {
            airPlane.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            airPlane.Draw(gameTime);
            spriteBatch.End();
            
            base.Draw(gameTime);
           
        }
    }
}
