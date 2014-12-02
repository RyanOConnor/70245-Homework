using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GameEngine
{
    public interface IObject
    {
        IObject Clone();
    }

    // Prototype utilized 
    public class Object
    {
        public string name { get; private set; }
        public Bitmap image { get; private set; }
        public Physics physics { get; private set; }


        public Object(Bitmap i, string n, Point location)
        {
            image = i;
            name = n;

            GraphicsUnit units = GraphicsUnit.Point;
            RectangleF boundingBox = image.GetBounds(ref units);

            physics = new Physics(location, boundingBox);
        }

        public virtual void Update()
        {
            physics.setPosition(physics.getPosition().X + physics.getVelocity().X, physics.getPosition().Y + physics.getVelocity().Y );
            physics.setVelocity(physics.getVelocity().X + physics.getAccel().X, physics.getVelocity().Y + physics.getAccel().Y);
            
            double speed = 0.0;
            if( speed < (physics.getSpeed() - physics.getFriction()))
                speed = physics.getSpeed() - physics.getFriction();

            physics.setSpeed(speed);
        }

        public void Draw()
        {
            if (physics.getVisibility())
            {
                Game.GetInstance().graphics.frameGraphics.DrawImage(image, physics.getPosition());
            }
        }

        public Physics getPhysics()
        {
            return physics;
        }
    }

    public class Asteroid : Object, IObject
    {
        public Asteroid(Bitmap i, string n, Point p)
            :base(i, n, p)
        {   }

        public IObject Clone()
        {
            return (IObject)this.MemberwiseClone();
        }
    }

    // Decorator utilized
    public abstract class AsteroidDecorator : Object, IObject
    {
        private Object _object;
        public AsteroidDecorator(Object o)
           : base(o.image, o.name, o.physics.getPosition())
        {
            _object = o;
        }

        public IObject Clone()
        {
            return (IObject)this.MemberwiseClone();
        }

        public Bitmap image
        {
            get { return _object.image; }
        }

        public string name
        {
            get { return _object.name; }
        }

        public Physics physics
        {
            get { return _object.physics; }
        }
    }

    public class DestroyedAsteroid : AsteroidDecorator
    {
        public DestroyedAsteroid(Object o)
            : base(o)
        {
            o.physics.setFriction(0.5);
        }

        // Use image of destroyed asteroid instead
        public Bitmap image 
        { 
            get { return GameEngine.Properties.Resources.mario; }
        }

        public override void Update()
        {
            // slow destroyed asteroid down
            physics.setSpeed(base.physics.getSpeed() - base.physics.getFriction());

            // if asteroid has fragmented and slowed down enough, remove it from map
            if (base.physics.getSpeed() < 0.001)
            {
                base.physics.setVisibiility(false);
            }
        }
        
    }


}
