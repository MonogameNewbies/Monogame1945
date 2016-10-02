using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame1945.GameObjects
{
    public class AirPlane : BaseGameObject
    {
        public Vector2 Position => SpriteObject.Position;

        private KeyboardState state;
        private Vector2 direction;
        private readonly float speed = 400;

        public AirPlane(Texture2D texture, GraphicsDeviceManager graphics)
            : base(texture)
        {
            var viewport = new Viewport(graphics.GraphicsDevice.Viewport.Bounds);
            SpriteObject.Position = new Vector2(
                viewport.Width/2f, viewport.Height - SpriteObject.GetBoundingRectangle().Height/2f);
        }

        public override void Update(GameTime gameTime)
        {
            state = Keyboard.GetState();
            Move(gameTime);
        }

        Vector2 InputHandler()
        {
            Vector2 tmpDirection = Vector2.Zero;

            Dictionary<Keys, Vector2> keyMap = new Dictionary<Keys, Vector2>();
            keyMap.Add(Keys.Left, new Vector2(-1, 0));
            keyMap.Add(Keys.Right, new Vector2(1, 0));

            foreach (var key in keyMap)
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
            direction = InputHandler();
            Vector2 newPosition = Position + direction*(float) dt.ElapsedGameTime.TotalSeconds*speed;

            SpriteObject.Position = newPosition;
        }
    }
}