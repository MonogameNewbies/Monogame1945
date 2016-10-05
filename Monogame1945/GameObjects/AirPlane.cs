using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Particles;

namespace Monogame1945.GameObjects
{
    public class AirPlane : BaseGameObject
    {
        public Vector2 Position => Sprite.Position;

        private readonly Dictionary<Keys, Vector2> moveMappings = new Dictionary<Keys, Vector2>();
        private readonly Dictionary<Keys, int> shootMappings = new Dictionary<Keys, int>();
        private KeyboardState state;
        private Vector2 direction;
        private readonly float speed = 400;
        private readonly ParticleEffect particleEffect;

        public AirPlane(Texture2D texture, GraphicsDevice graphics, SpriteBatch batch, ParticleEffect particleEffect)
            : base(texture, graphics, batch)
        {
            this.particleEffect = particleEffect;
            Sprite.Position = new Vector2(
                Viewport.Width/2f, Viewport.Height - Sprite.GetBoundingRectangle().Height);

            moveMappings.Add(Keys.Left, new Vector2(-1, 0));
            moveMappings.Add(Keys.Right, new Vector2(1, 0));
            moveMappings.Add(Keys.Up, new Vector2(0, -1));
            moveMappings.Add(Keys.Down, new Vector2(0, 1));
            moveMappings.Add(Keys.A, new Vector2(-1, 0));
            moveMappings.Add(Keys.D, new Vector2(1, 0));
            moveMappings.Add(Keys.W, new Vector2(0, -1));
            moveMappings.Add(Keys.S, new Vector2(0, 1));

            shootMappings.Add(Keys.Space, 0);
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
                if (moveMappings.TryGetValue(key, out v))
                {
                    tmpDirection += v;
                }

                int s;
                if (shootMappings.TryGetValue(key, out s))
                {
                    switch (s)
                    {
                        case 0:
                            particleEffect.Trigger(Sprite.Position - new Vector2(0f, 30f));
                            break;
                    }
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
            if (b.Right < Viewport.Bounds.Right && b.Left > Viewport.Bounds.Left &&
                b.Bottom < Viewport.Bounds.Bottom && b.Top > Viewport.Bounds.Top)
            {
                Sprite.Position = newPosition;
            }
        }
    }
}