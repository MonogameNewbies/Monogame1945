using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame1945.Scene
{
    class MainMenuScreen : MenuScreen
    {
        Game game;
        public MainMenuScreen(IServiceProvider serviceProvider,Game game) : base(serviceProvider)
        {
            this.game = game;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            AddMenu("New Game", Show<GameScreen>);
            AddMenu("Load Game", () => {});
        }
    }
}
