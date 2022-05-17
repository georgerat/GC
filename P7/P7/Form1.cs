using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace P7
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
            Pen p = new Pen(Color.Green, 3);

            Random rand = new Random();
            int n = rand.Next(5, 30);

            PointF[] m1 = new PointF[n];
            float ymax = float.MinValue, x1 = 0;

            for (int i = 0; i < n; i++)
            {
                m1[i].X = rand.Next(50, panel1.Width - 50);
                m1[i].Y = rand.Next(50, panel1.Height - 50);
                g.DrawEllipse(p, m1[i].X, m1[i].Y, 2, 2);
            }

            for (int i = 0; i < n; i++)
            {
                if (m1[i].Y > ymax)
                {
                    ymax = m1[i].Y;
                    x1 = m1[i].X;
                }
                else if (m1[i].Y == ymax)
                {
                    if (m1[i].X < x1)
                        x1 = m1[i].X;
                }
            }

            int k = 0;
            float xk = x1;
            float yk = ymax;
            bool valid = true;
            p = new Pen(Color.Blue, 3);
            while (valid)
            {
                float xp = m1[k].X;
                float yp = m1[k].Y;

                for (int i = 0; i < n; i++)
                {
                    if (determinant(xk, yk, m1[i].X, m1[i].Y, xp, yp) < 0)
                    {
                        xp = m1[i].X;
                        yp = m1[i].Y;
                    }
                }

                if (xp != x1 && yp != ymax)
                {
                    g.DrawLine(p, xk, yk, xp, yp);
                    Thread.Sleep(50);
                    k++;
                    xk = xp;
                    yk = yp;
                }
                else
                {
                    valid = false;
                    g.DrawLine(p, xk, yk, xp, yp);
                }
            }
        }

        private float determinant(float xp, float yp, float xq, float yq, float xr, float yr)
        {
            return xp * (yq - yr) + xq * (yr - yp) + xr * (yp - yq);
        }
    }
}
