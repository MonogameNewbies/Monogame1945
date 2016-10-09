using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Monogame1945.GameObjects;

namespace Monogame1945.Scene
{
    class GameplayScene : DrawableGameComponent
    {
        Game game;
        AirPlane airplane;
        ContentManager Content;
        GraphicsDeviceManager graphicsManager;
        SpriteBatch batch;
        public GameplayScene(Game game) : base(game)
        {
            this.game = game;
            graphicsManager = ((Game1)game).graphicsManager;
            Content = game.Content;
        }

        public override void Initialize()
        {
            
            
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            airplane = new AirPlane(game, Content, graphicsManager.GraphicsDevice, batch);


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            airplane.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            batch.Begin();
            airplane.Draw(gameTime);
            batch.End();
        }
    }
}
