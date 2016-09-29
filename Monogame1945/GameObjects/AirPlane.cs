namespace Monogame1945.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoGame.Extended.InputListeners;
    using MonoGame.Extended.Sprites;

    public class AirPlane : BaseGameObject
    {
        KeyboardState state;
        Vector2 Direction;
        public Vector2 Position { get { return SpriteObject.Position; } }
        Viewport viewport;
        float speed = 400;

        public AirPlane(Texture2D texture, GraphicsDeviceManager graphics)
            : base(texture)
        {
            viewport = new Viewport(graphics.GraphicsDevice.Viewport.Bounds);
            SpriteObject.Position = new Vector2(
                viewport.Width / 2, viewport.Height - SpriteObject.GetBoundingRectangle().Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            state = Keyboard.GetState();

            Move(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        Vector2 InputHandler()
        {
            Vector2 tmpDirection = Vector2.Zero;

            Dictionary<Keys, Vector2> KeyMap = new Dictionary<Keys, Vector2>();
            KeyMap.Add(Keys.Left, new Vector2(-1, 0));
            KeyMap.Add(Keys.Right, new Vector2(1, 0));

            foreach (var key in KeyMap)
            {
                if (state.IsKeyDown(key.Key))
                {
                    tmpDirection = key.Value;
                }
            }

            return tmpDirection;
        }

        void Move(GameTime dt)
        {
            Direction = InputHandler();
            Vector2 newPosition = Position + ((Direction * (float)dt.ElapsedGameTime.TotalSeconds) * speed);

            SpriteObject.Position = newPosition;
        }
    }
}
