using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Screens;

namespace Monogame1945.Scene
{
    public class MenuScreen : Screen
    {
        readonly IServiceProvider serviceProvider;
        SpriteBatch spriteBatch;
        MouseState prevMouseState;

        public List<MenuItem> MenuItems { get; set; }
        SpriteFont font { get; set; }
        ContentManager Content { get; set; }
        public MenuScreen(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            MenuItems = new List<MenuItem>();
        }

        public void AddMenu(string text, Action action)
        {
            var menuItem = new MenuItem(font, text)
            {
                Position = new Vector2(300, 200 + 32 * MenuItems.Count),
                Action = action
            };

            MenuItems.Add(menuItem);
        }

        public override void Initialize()
        {
            base.Initialize();
            Content = new ContentManager(serviceProvider,"Content");
        }

        public override void LoadContent()
        {
            base.LoadContent();
            IGraphicsDeviceService graphicsDeviceService = (IGraphicsDeviceService)serviceProvider.GetService(typeof(IGraphicsDeviceService));
            spriteBatch = new SpriteBatch(graphicsDeviceService.GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MouseState mouseState = Mouse.GetState();

            bool isPressed = Ispressed(mouseState, prevMouseState);

            foreach (var menuItem in MenuItems)
            {
                bool hovered = menuItem.BoundingRectangle.Contains(mouseState.X, mouseState.Y);
                menuItem.Color = hovered ? Color.Yellow : Color.White;
                if(hovered && isPressed)
                {
                    Debug.WriteLine("Pressed");
                    menuItem.Action?.Invoke();
                    break;
                }
            }
            prevMouseState = mouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            foreach (var menuItem in MenuItems)
            {
                menuItem.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        bool Ispressed(MouseState mouseState,MouseState prevMouseState)
        {
            return mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
