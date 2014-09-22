using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanOConnor_HW1
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public interface Interface<T>
    {
        void Attack(T obj);
    }

    public class Enemy : Interface<Player>
    {
        public void Attack(Player P)
        {
            P.health = P.health - R.Next(1, 10);
        }

        public void Speak()
        {
            Console.WriteLine("\n\t{0}: Bring it on!\n", type);
        }

        Random R = new Random();
        private int val;
        public int health
        {
            get { return val; }
            set { val = value; }
        }
        public string type { get; set; }
    }

    public class Player : Interface<Enemy>
    {
        public void Attack(Enemy E)
        {
            E.health = E.health - R.Next(1, 10);
        }

        Random R = new Random();
        private int val;
        public int health
        {
            get { return val; }
            set { val = value; }
        }
    }
}
