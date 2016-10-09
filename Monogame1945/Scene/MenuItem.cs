using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Shapes;

namespace Monogame1945.Scene
{
    public class MenuItem
    {

        public SpriteFont Font { get; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public RectangleF BoundingRectangle => new RectangleF(Position, Font.MeasureString(Text));
        public Action Action { get; set; }
        public MenuItem(SpriteFont font, string text)
        {
            Text = text;
            Font = font;
            Color = Color.White;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color);
        }
    }
}
