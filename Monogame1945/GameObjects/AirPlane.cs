using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame1945.GameObjects
{
    public class AirPlane : BaseGameObject
    {
        public Vector2 Position => Sprite.Position;

        private Dictionary<Keys, Vector2> keyMap;
        private KeyboardState state;
        private Vector2 direction;
        private readonly float speed = 400;

        public AirPlane(Game game, Texture2D texture, GraphicsDevice graphics, SpriteBatch batch)
            : base(game, texture, graphics, batch)
        {
            Sprite.Position = new Vector2(
                Viewport.Width/2f, Viewport.Height - Sprite.GetBoundingRectangle().Height/2f);

            keyMap = new Dictionary<Keys, Vector2>();
            keyMap.Add(Keys.Left, new Vector2(-1, 0));
            keyMap.Add(Keys.Right, new Vector2(1, 0));
            keyMap.Add(Keys.Up, new Vector2(0, -1));
            keyMap.Add(Keys.Down, new Vector2(0, 1));
        }

        public override void Update(GameTime gt)
        {
            state = Keyboard.GetState();
            Move(gt);
        }

        Vector2 InputHandler()
        {
            Vector2 tmpDirection = Vector2.Zero;
            // Iterate over pressed keys, not over all mappings regardless of how many keys are currently pressed.
            foreach (var key in state.GetPressedKeys())
            {
                Vector2 v;
                if (keyMap.TryGetValue(key, out v))
                {
                    tmpDirection += v;
                }
            }
            return tmpDirection;
        }

        void Move(GameTime dt)
        {
            direction = InputHandler();
            Vector2 newPosition = Position + direction*(float) dt.ElapsedGameTime.TotalSeconds*speed;
            var b = Sprite.GetBoundingRectangle();
            // Translate to {0,0}.
            b.Offset(-Sprite.Position);
            // Translate to newPosition.
            b.Offset(newPosition);
            // Now constrain the movement.
            if (newPosition.X < Viewport.Bounds.Right && newPosition.X > Viewport.Bounds.Left &&
                newPosition.Y < Viewport.Bounds.Bottom && newPosition.Y > Viewport.Bounds.Top)
            {
                Sprite.Position = newPosition;
            }
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            keyMap.Clear();
            keyMap = null;
        }
    }
}