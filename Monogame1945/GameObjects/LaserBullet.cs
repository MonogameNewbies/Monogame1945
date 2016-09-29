namespace Monogame1945.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extended.Sprites;
    using Microsoft.Xna.Framework;

    public class LaserBullet : BaseGameObject
    {
        public LaserBullet(Texture2D texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
