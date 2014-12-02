using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GameEngine
{
    public class Physics
    {
        Point position;
        Point previous;
        Point startPosition;
        Point velocity;
        Point accel;
        double direction { get; set; }
        double friction { get; set; }
        bool visible { get; set; }
        RectangleF boundingBox { get; set; }

        public Physics(Point p, RectangleF boundingBox)
        {
            startPosition = p;
            position = startPosition;
            this.boundingBox = boundingBox;
            visible = true;
        }

        public RectangleF getBounds() { return boundingBox; }

        public void setPosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public void setPosition(Point p)
        { 
            position = p;
        }

        public Point getPosition()
        {
            return position;
        }

        public void setX(int x) { position.X = x; }

        public void setY(int y) { position.Y = y;  }

        public int getX() { return position.X; }

        public int getY() { return position.Y; }

        public bool getVisibility() { return visible; }

        public void setVisibiility(bool visibility) 
        {
            visible = visibility;
        }

        public Point getVelocity() { return velocity; }

        public void setVelocity(Point p)
        {
            velocity = p;
            direction = Math.Atan2(-velocity.Y, velocity.X);
        }

        public void setVelocity(int x, int y)
        {
            velocity.X = x;
            velocity.Y = y;

            direction = Math.Atan2(-velocity.Y, velocity.X);
        }

        public Point getAccel() { return accel; }

        public void setAccel(Point a)
        {
            accel = a;
        }

        public void setAccel(int x, int y)
        {
            accel.X = x;
            accel.Y = y;
        }

        public int getHAccel() { return accel.X; }

        public void setHAccel(int haccel)
        {
            accel.X = haccel;
        }

        public int getVAccel() { return accel.Y; }

        public void setVAccel(int vaccel)
        {
            accel.Y = vaccel;
        }

        public int getHSpeed() { return velocity.X; }

        public void setHSpeed(int hspeed)
        {
            velocity.X = hspeed;
            direction = Math.Atan2(-velocity.Y, velocity.X);
        }

        public int getVSpeed() { return velocity.Y; }

        public void setVSpeed(int vspeed)
        {
            velocity.Y = vspeed;
            direction = Math.Atan2(-velocity.Y, velocity.X);
        }

        public double getSpeed()
        {
            return Math.Sqrt( (velocity.X * velocity.X) + ( velocity.Y * velocity.Y) );
        }

        public void setSpeed(double newSpeed)
        {
            double oldSpeed = getSpeed();

            if (oldSpeed == 0)
            {
                velocity.X = (int)(newSpeed * Math.Cos(direction));
                velocity.Y = -(int)(newSpeed * Math.Sin(direction));
            }
            else
            {
                velocity.X *= (int)(newSpeed / oldSpeed);
                velocity.Y *= (int)(newSpeed / oldSpeed);
            }
        }

        public double getFriction() { return friction; }

        public void setFriction(double newFriction)
        {
            if (friction >= 0)
                friction = newFriction;
            else if (friction < 0)
                friction = 0;
        }
    }
}
