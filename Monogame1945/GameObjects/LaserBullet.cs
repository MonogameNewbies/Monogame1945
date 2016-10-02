using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame1945.GameObjects
{
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