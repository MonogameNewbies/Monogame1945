using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame1945.GameObjects
{
    public class LaserBullet : BaseGameObject
    {
        public LaserBullet(Game game, Texture2D texture, GraphicsDevice graphics, SpriteBatch batch)
            : base(game, texture, graphics, batch)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gt)
        {
            base.Draw(gt);
        }
    }
}