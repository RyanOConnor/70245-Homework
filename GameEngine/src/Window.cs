using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GameEngine
{
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            Game.GetInstance().startGraphics(g);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.GetInstance().stopGame();
        }

        private void Window_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Keyboard.KeyPressed(e.KeyData.ToString());
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Keyboard.KeyReleased(e.KeyData.ToString());
        }
    }
}
