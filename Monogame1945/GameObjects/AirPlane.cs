using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.Animations.Tweens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace Monogame1945.GameObjects
{
    public class AirPlane : BaseGameObject
    {

        enum GetAnimationState
        {
            Idle,
            Left,
            Right
        }
        public Vector2 Position;

        private Dictionary<Keys, Vector2> keyMap;
        private KeyboardState state;
        private Vector2 direction;
        private readonly float speed = 400;

        Texture2D left, right,forward;
        SpriteSheetAnimator animator;
        Sprite sprite;
        public AirPlane(Game game, ContentManager content, GraphicsDevice graphics, SpriteBatch batch)
            : base(game, content, graphics, batch)
        {
           

            keyMap = new Dictionary<Keys, Vector2>();
            keyMap.Add(Keys.Left, new Vector2(-1, 0));
            keyMap.Add(Keys.Right, new Vector2(1, 0));
            keyMap.Add(Keys.Up, new Vector2(0, -1));
            keyMap.Add(Keys.Down, new Vector2(0, 1));

        



            var texture = Content.Load<Texture2D>("plane");
            var atlas = TextureAtlas.Create(texture, 66, 64);
            var animationfactory = new SpriteSheetAnimationFactory(atlas);
            animationfactory.Add("Idle",new SpriteSheetAnimationData(new[] { 0 }, isLooping: false));
            animationfactory.Add("Moveleft", new SpriteSheetAnimationData(new[] { 2 }, isLooping: false));
            animationfactory.Add("Moveright", new SpriteSheetAnimationData(new[] { 3 }, isLooping: false));
            animator = new SpriteSheetAnimator(animationfactory);
            sprite = animator.CreateSprite();
            animator.Play("Idle");
            sprite.Position = new Vector2(Viewport.Width / 2f, Viewport.Height - sprite.GetBoundingRectangle().Height);


        }

        public override void Update(GameTime gt)
        {
            state = Keyboard.GetState();
            Move(gt);
            animator.Update(gt);
        }


        GetAnimationState getAnimationState(Vector2 direction)
        {
            if(direction.X < 0)
            {
                return GetAnimationState.Left; 
            }

            if(direction.X > 0)
            {
                return  GetAnimationState.Right;
            }
            return  GetAnimationState.Idle;
        }
        void playAnimator(GetAnimationState state)
        {
            switch (state)
            {
                case GetAnimationState.Idle:
                    animator.Play("Idle");
                  break;
                case GetAnimationState.Left:
                    animator.Play("Moveleft");
                  break;
                case GetAnimationState.Right:
                    animator.Play("Moveright");
                    break;
            }
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

            playAnimator(getAnimationState(tmpDirection));
            return tmpDirection;
        }

        void Move(GameTime dt)
        {
           
            direction = InputHandler();

            Vector2 newPosition = sprite.Position + direction*(float) dt.ElapsedGameTime.TotalSeconds*speed;
            var b = sprite.GetBoundingRectangle();
            // Translate to {0,0}.
            b.Offset(-sprite.Position);
            // Translate to newPosition.
            b.Offset(newPosition);
            // Now constrain the movement.
            if (b.Right < Viewport.Bounds.Right && b.Left > Viewport.Bounds.Left &&
                b.Bottom < Viewport.Bounds.Bottom && b.Top > Viewport.Bounds.Top)
            {
                sprite.Position = newPosition;
            }
           
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            keyMap.Clear();
            keyMap = null;
        }

        public override void Draw(GameTime gameTime)
        {
            Batch.Draw(sprite);
        }
    }
}