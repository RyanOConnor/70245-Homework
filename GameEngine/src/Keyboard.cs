using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace GameEngine
{
    public static class Keyboard
    {
        static bool[] currentPress = new bool[127];
        static bool[] previousPress = new bool[127];

		// TODO: Replace key type with Keys enum instead of string
		// http://msdn.microsoft.com/en-us/library/system.windows.forms.keys%28v=vs.110%29.aspx
        static Dictionary<string, bool> current = new Dictionary<string, bool>();
        static Dictionary<string, bool> previous = new Dictionary<string, bool>();

        public static void initializeKeyBuffers()
        {
            Keys[] keys = (Keys[])Enum.GetValues(typeof(Keys));
            
            foreach (object key in keys)
            {
                if (!current.ContainsKey(key.ToString()))
                {
                    current.Add(key.ToString(), false);
                    previous.Add(key.ToString(), false);
                }
            }
        }

        public static bool isKeyPressed(string key)
        {
            if (current[key] && !previous[key])
                return true;
            else
                return false;
        }

        public static bool isKeyDown(string key)
        {
            return current[key];
        }

        public static bool isKeyReleased(string key)
        {
            if (!current[key] && previous[key])
                return true;
            else
                return false;
        }

        public static bool isKeyUp(string key)
        {
            return current[key];
        }

        public static void Update()
        {
            previous = new Dictionary<string, bool>(current);
        }

        public static void KeyPressed(string key)
        {
            current[key] = true;
        }

        public static void KeyReleased(string key)
        {
            current[key] = false;
        }
    }
}
