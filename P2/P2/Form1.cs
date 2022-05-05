using System;
using System.Drawing;
using System.Windows.Forms;

namespace P2
{
    public partial class Form1 : Form
    {

        //Se dau n puncte in plan si un punct q. Sa se construiasca cercul de raza maxima, cu centrul in q, care nu contine alte puncte.

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Green, 2);
            Random r = new Random();
            int n = r.Next(100);
            float qx = r.Next(50, panel1.Width - 50);
            float qy = r.Next(50, panel1.Height - 50);
            float raza = 1;
            g.DrawEllipse(p, qx - raza, qy - raza, raza * 2, raza * 2);
            p = new Pen(Color.Black, 2);
            float dist_min = panel1.Width;
            for (int i = 0; i < n; i++)
            {
                float x = r.Next(10, panel1.Width);
                float y = r.Next(10, panel1.Height);
                g.DrawEllipse(p, x - raza, y - raza, raza * 2, raza * 2);
                float dist = (float)Math.Sqrt(Math.Pow(qx - x, 2) + Math.Pow(qy - y, 2));
                if (dist < dist_min)
                    dist_min = dist;
            }
            p = new Pen(Color.Black, 1);
            g.DrawEllipse(p, qx - dist_min, qy - dist_min, dist_min * 2, dist_min * 2);
        }
    }
}
