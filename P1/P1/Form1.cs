using System;
using System.Drawing;
using System.Windows.Forms;

namespace P1
{
    public partial class Form1 : Form
    {

        //Se dau n puncte in plan (mici cerculete). Sa se construiasca dreptunghiul de arie minima care contine toate punctele date.

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Green, 3);

            Random r = new Random();
            int n = r.Next(2, 40); // 2 <= n <= 40

            float xmin = int.MaxValue, ymin = int.MaxValue;
            float xmax = int.MinValue, ymax = int.MinValue;

            float raza1 = 1;
            PointF[] m1 = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                m1[i].X = r.Next(50, panel1.Width - 50);
                m1[i].Y = r.Next(50, panel1.Height - 50);
                g.DrawEllipse(p, m1[i].X - raza1, m1[i].Y - raza1, raza1 * 2, raza1 * 2);

                if (m1[i].X < xmin)
                    xmin = m1[i].X;
                if (m1[i].X > xmax)
                    xmax = m1[i].X;
                if (m1[i].Y < ymin)
                    ymin = m1[i].Y;
                if (m1[i].Y > ymax)
                    ymax = m1[i].Y;
            }

            g.DrawLine(p, xmin, ymin, xmax, ymin);
            g.DrawLine(p, xmax, ymin, xmax, ymax);
            g.DrawLine(p, xmax, ymax, xmin, ymax);
            g.DrawLine(p, xmin, ymax, xmin, ymin);

            float L = xmax - xmin;
            float l = ymax - ymin;
            float A = L * l;
            label1.Text = Convert.ToString(A);
        }
    }
}
