namespace Monogame1945.GameObjects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extended.Sprites;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class serves as the base class of the gameobjects.
    /// </summary>
    public abstract class BaseGameObject
    {
        public Sprite SpriteObject { get; private set; }

        public BaseGameObject(Texture2D texture)
        {
            SpriteObject = new Sprite(texture);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            SpriteObject.Draw(spriteBatch);
        }
    }
}
