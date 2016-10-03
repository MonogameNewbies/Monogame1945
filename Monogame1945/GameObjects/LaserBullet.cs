using Microsoft.Xna.Framework.Graphics;

namespace Monogame1945.GameObjects
{
    public class LaserBullet : BaseGameObject
    {
        public LaserBullet(Texture2D texture, GraphicsDevice graphics, SpriteBatch batch)
            : base(texture, graphics, batch)
        {
        }
    }
}