using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame1945.GameObjects
{
    public class LaserBullet : BaseGameObject
    {
        public LaserBullet(Game game, ContentManager content, GraphicsDevice graphics, SpriteBatch batch)
            : base(game, content, graphics, batch)
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