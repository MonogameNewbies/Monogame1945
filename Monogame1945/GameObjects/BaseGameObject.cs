using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace Monogame1945.GameObjects
{
    /// <summary>
    ///     This class serves as the base class of the gameobjects.
    /// </summary>
    public abstract class BaseGameObject : DrawableGameComponent, IDraw, IUpdate
    {
        public Viewport Viewport { get; }
        public SpriteBatch Batch { get; private set; }
        public Sprite Sprite { get; private set; }

        public BaseGameObject(Game game, Texture2D texture, GraphicsDevice graphics, SpriteBatch batch) : base(game)
        {
            Viewport = new Viewport(graphics.Viewport.Bounds);
            Batch = batch;
            Sprite = new Sprite(texture);
        }
        
        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(Batch);
        }

        protected override void UnloadContent()
        {
            Sprite = null;
            Batch = null;
        }
    }
}