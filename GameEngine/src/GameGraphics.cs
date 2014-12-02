using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;

namespace GameEngine
{
    public class GameGraphics
    {
        public Graphics drawHandle { get; private set; }
        private Thread renderThread { get; set; }
        public Bitmap frame { get; set; }
        public Graphics frameGraphics { get; private set; }


        public GameGraphics(Graphics g)
        {
            drawHandle = g;
        }

        public void Initialize()
        {
            SceneFacade.InitializeScenes();

            renderThread = new Thread(new ThreadStart(Render));
            renderThread.Start();
        }


        public void Stop()
        {
            renderThread.Abort();
        }

        private void Render()
        {
            int frameCounter = 0;
            long startTime = Environment.TickCount;

            frame = new Bitmap(Game.WINDOW_WIDTH, Game.WINDOW_HEIGHT);
            frameGraphics = Graphics.FromImage(frame);
            Keyboard.initializeKeyBuffers();
            
            while (true)
            {
                SceneFacade.currentScene.HandleEvents();

                Keyboard.Update();
                SceneFacade.currentScene.Update();

                SceneFacade.currentScene.DrawMap(frame);

                SceneFacade.currentScene.Draw();

                drawHandle.DrawImage(frame, 0, 0);
              
                frameCounter++;
                if (Environment.TickCount >= startTime + 1000)
                {
                    Console.WriteLine("GameGraphics: " + frameCounter + " fps");
                    frameCounter = 0;
                    startTime = Environment.TickCount;
                }
            }
        }
    }
}
