using System;
using System.Drawing;
using System.Windows.Forms;

namespace P4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Se da o multime de n puncte. Sa se determine cercul de arie minima care sa contina toate punctele in interior (minimal enclosing circle).

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Green, 3);
            Random r = new Random();
            int n = r.Next(20, 60);
            PointF[] m1 = new PointF[n];

            float xmin = int.MaxValue, ymin = int.MaxValue;
            float xmax = int.MinValue, ymax = int.MinValue;

            for (int i = 0; i < n; i++)
            {
                m1[i].X = r.Next(150, panel1.Width - 150);
                m1[i].Y = r.Next(60, panel1.Height - 60);

                if (m1[i].X < xmin)
                    xmin = m1[i].X;
                if (m1[i].X > xmax)
                    xmax = m1[i].X;
                if (m1[i].Y < ymin)
                    ymin = m1[i].Y;
                if (m1[i].Y > ymax)
                    ymax = m1[i].Y;

                g.DrawEllipse(p, m1[i].X, m1[i].Y, 2, 2);
            }

            float x1 = 0, y1 = 0, x2 = 0, y2 = 0, xc = 0, yc = 0, dist_max = int.MinValue;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    float dist = (float)Math.Sqrt(Math.Pow(m1[i].X - m1[j].X, 2) + Math.Pow(m1[i].Y - m1[j].Y, 2));
                    if (dist > dist_max)
                    {
                        dist_max = dist;
                        x1 = m1[i].X;
                        y1 = m1[i].Y;
                        x2 = m1[j].X;
                        y2 = m1[j].Y;
                    }
                }
            }

            xc = (x1 + x2) / 2;
            yc = (y1 + y2) / 2;

            float diametru = (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            float raza = diametru / 2;

            //g.DrawLine(p, x1, y1, x2, y2); //cea mai mare distanta intre 2 puncte din plan;
            //p = new Pen(Color.Blue, 3);
            //g.DrawEllipse(p, xc, yc, 2, 2);  //centrul cercului;

            p = new Pen(Color.Black, 3);
            g.DrawEllipse(p, xc - raza, yc - raza, diametru, diametru);
        }
    }
}
