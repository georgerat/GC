using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace P6
{
    //Invelitoare convexa

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p1 = new Pen(Color.Green, 3);

            Random rand = new Random();
            int n = rand.Next(3, 30);

            PointF[] m1 = new PointF[n];

            for (int i = 0; i < n; i++)
            {
                m1[i].X = rand.Next(50, panel1.Width - 50);
                m1[i].Y = rand.Next(50, panel1.Height - 50);
                g.DrawEllipse(p1, m1[i].X, m1[i].Y, 2, 2);
            }

            p1 = new Pen(Color.Blue, 3);
            int p, q, r;
            bool ok;

            for (p = 0; p < n; p++)
                for (q = 0; q < n; q++)
                {
                    ok = true;
                    for (r = 0; r < n; r++)
                    {
                        if (determinant(m1[p].X, m1[p].Y, m1[q].X, m1[q].Y, m1[r].X, m1[r].Y) > 0)
                            ok = false;
                    }
                    if (ok)
                    {
                        g.DrawLine(p1, m1[p].X, m1[p].Y, m1[q].X, m1[q].Y);
                        Thread.Sleep(20);
                    }
                }
        }

        private float determinant(float xp, float yp, float xq, float yq, float xr, float yr)
        {
            return xp * (yq - yr) + xq * (yr - yp) + xr * (yp - yq);
        }
    }
}
