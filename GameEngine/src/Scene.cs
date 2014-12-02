using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GameEngine
{
    public abstract class Scene
    {
        public List<Object> objects;
        private char keyEvent;

        public Scene()
        {
            objects = new List<Object>();
        }

        public virtual void Update()
        {
            foreach (Object obj in objects)
            {
                obj.Update();
            }
        }

        public virtual void Draw()
        {
            foreach (Object obj in objects)
            {
                obj.Draw();
            }
        }

        public virtual void InstanceCreate(Object newObject)
        {
            objects.Add(newObject);
        }

        public abstract void DrawMap(Bitmap frame);
        public abstract void HandleEvents();
    }

    // Facade Utilized
    public class SceneFacade
    {
        public static Scene currentScene { get; private set; }
        public static GameScene gameScene { get; private set; }
        public static PauseScene pauseScene { get; private set; }
        public static MenuScene menuScene { get; private set; }

        public static void InitializeScenes()
        {
            gameScene = new GameScene();
            pauseScene = new PauseScene();
            menuScene = new MenuScene();

            currentScene = menuScene;
        }

        public static void ChangeScene(Scene scene)
        {
            currentScene = scene;
        }
    }

    public class GameScene : Scene
    {
        public GameScene()
            :base()
        {
            Point startPosition = new Point();

            startPosition.X = 25;
            startPosition.Y = 200;
            Asteroid a1 = new Asteroid(GameEngine.Properties.Resources.mario, "mario", startPosition );

            startPosition.X = 700;
            startPosition.Y = 300;
            Asteroid a2 = new Asteroid(GameEngine.Properties.Resources.mario, "mario2", startPosition);

            InstanceCreate(a1);
            InstanceCreate(a2);
        }

        public override void DrawMap(Bitmap frame)
        {
            Graphics frameGraphics = Graphics.FromImage(frame);
            frameGraphics.FillRectangle(new SolidBrush(Color.LawnGreen), 0, 0, Game.WINDOW_WIDTH, Game.WINDOW_HEIGHT);
        }

        public override void HandleEvents()
        {
            if (objects.First().getPhysics().getSpeed() > 0)
                objects.First().getPhysics().setSpeed(0);

            if (Keyboard.isKeyDown("Up"))
                objects.First().getPhysics().setVSpeed(-10);

            if (Keyboard.isKeyDown("Down"))
                objects.First().getPhysics().setVSpeed(10);

            if (Keyboard.isKeyDown("Right"))
                objects.First().getPhysics().setHSpeed(10);

            if (Keyboard.isKeyDown("Left"))
                objects.First().getPhysics().setHSpeed(-10);

            if (Keyboard.isKeyPressed("P"))
                SceneFacade.ChangeScene(SceneFacade.pauseScene);
        }
    }

    public class MenuScene : Scene
    {
        public MenuScene()
            : base()
        {
            Point startPosition = new Point();

            startPosition.X = 500;
            startPosition.Y = 250;
            Object text = new Object(GameEngine.Properties.Resources.pressenter, "Text", startPosition);

            InstanceCreate(text);
        }

        public override void DrawMap(Bitmap frame)
        {
            Graphics frameGraphics = Graphics.FromImage(frame);
            frameGraphics.FillRectangle(new SolidBrush(Color.MediumPurple), 0, 0, Game.WINDOW_WIDTH, Game.WINDOW_HEIGHT);
        }

        public override void HandleEvents()
        {
            if (Keyboard.isKeyPressed("Return"))
                SceneFacade.ChangeScene(SceneFacade.gameScene);
        }
    }

    public class PauseScene : Scene
    {
        public PauseScene()
            : base()
        { }

        public override void DrawMap(Bitmap frame)
        {
            Graphics frameGraphics = Graphics.FromImage(frame);
            frameGraphics.FillRectangle(new SolidBrush(Color.OrangeRed), 0, 0, Game.WINDOW_WIDTH, Game.WINDOW_HEIGHT);
        }

        public override void HandleEvents()
        {
            if (Keyboard.isKeyPressed("P"))
                SceneFacade.ChangeScene(SceneFacade.gameScene);
            else if (Keyboard.isKeyPressed("Q"))
                SceneFacade.ChangeScene(SceneFacade.menuScene);
        }
    }
}
