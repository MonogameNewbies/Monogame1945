using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace Monogame1945.GameObjects
{
    /// <summary>
    ///     This class serves as the base class of the gameobjects.
    /// </summary>
    public abstract class BaseGameObject : DrawableGameComponent, IUpdate
    {
        public ContentManager Content;
        public Viewport Viewport { get; }
        public SpriteBatch Batch { get; private set; }
        public Sprite Sprite { get;  set; }

        public BaseGameObject(Game game, ContentManager content, GraphicsDevice graphics, SpriteBatch batch) : base(game)
        {
            Viewport = new Viewport(graphics.Viewport.Bounds);
            Batch = batch;
            this.Content = content;
            
        }
        
        public override void Draw(GameTime gameTime)
        {
            if(Sprite != null)
            {
                Sprite.Draw(Batch);
            }
        }

        protected override void UnloadContent()
        {
            Sprite = null;
            Batch = null;
        }
    }
}