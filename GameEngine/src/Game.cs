using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameEngine
{
    public class Game
    {
        public const int WINDOW_HEIGHT = 700;
        public const int WINDOW_WIDTH = 1200;

        public static Game instance { get; private set; }
        public GameGraphics graphics { get; private set; }

        // Singleton utilized
        public static Game GetInstance()
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }

        public void startGraphics(Graphics g)
        {
            graphics = new GameGraphics(g);
            graphics.Initialize();
        }

        public void stopGame()
        {
            graphics.Stop();
        }
    }
}
