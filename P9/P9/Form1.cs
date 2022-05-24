using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace P9
{
    //Să se reprezinte grafic o triangulare a unui poligon simplu cu n > 3 vârfuri, folosind metoda eliminării urechilor.
    //Să se reprezinte vizual o 3 - colorare a grafului asociat triangulării obținute. Să se determine aria poligonului.

    public partial class Form1 : Form
    {
        Graphics g;
        Pen p1;
        const int raza = 2;
        int n = 0, cn;
        List<Point> p = new List<Point>();
        List<Point> puncte = new List<Point>();
        List<Tuple<int, int, int>> indexinitial = new List<Tuple<int, int, int>>();
        List<Tuple<Point, Point, Point>> triangles = new List<Tuple<Point, Point, Point>>();
        bool poligon_inchis = false;

        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
            p1 = new Pen(Color.Green, 3);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            p.Add(PointToClient(new Point(MousePosition.X, MousePosition.Y)));
            puncte.Add(PointToClient(new Point(MousePosition.X, MousePosition.Y)));
            g.DrawString((n + 1).ToString(), new Font(FontFamily.GenericSansSerif, 10), new SolidBrush(Color.Blue), p[n].X + raza, p[n].Y - raza);
            if (!poligon_inchis)
            {
                g.DrawEllipse(p1, p[n].X, p[n].Y, raza, raza);
                if (n > 0)
                    g.DrawLine(p1, p[n - 1], p[n]);
                n++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cn = n;
            if (!poligon_inchis)
            {
                if (n < 3)
                    return;
                g.DrawLine(p1, p[n - 1], p[0]);
                poligon_inchis = true;
            }
            if (n < 3)
                return;
            if (n == 3)
                label3.Text = Convert.ToString(Aria(p[0], p[1], p[2]));

            p1 = new Pen(Color.Red);
            double aria_poligon = 0;

            while (n > 3)
                for (int i = 0; i < n; i++)
                {
                    if (i != n - 1 && i != n - 2 && isdiagonala(i, i + 2))
                    {
                        double aria_triunghi = Aria(p[i], p[i + 1], p[i + 2]);
                        aria_poligon += aria_triunghi;
                        g.DrawLine(p1, p[i], p[i + 2]);
                        triangles.Add(new Tuple<Point, Point, Point>(p[i], p[i + 1], p[i + 2]));
                        Thread.Sleep(100);
                        p.Remove(p[i + 1]);
                        n--;
                        break;
                    }
                    if (i == n - 1 && isdiagonala(i, 1))
                    {
                        double aria_triunghi = Aria(p[i], p[0], p[1]);
                        aria_poligon += aria_triunghi;
                        g.DrawLine(p1, p[i], p[1]);
                        triangles.Add(new Tuple<Point, Point, Point>(p[i], p[0], p[1]));
                        Thread.Sleep(100);
                        p.Remove(p[0]);
                        n--;
                        break;
                    }
                    if (i == n - 2 && isdiagonala(i, 0))
                    {
                        double aria_triunghi = Aria(p[i], p[i - 1], p[0]);
                        aria_poligon += aria_triunghi;
                        g.DrawLine(p1, p[i], p[0]);
                        triangles.Add(new Tuple<Point, Point, Point>(p[i], p[i - 1], p[0]));
                        Thread.Sleep(100);
                        p.Remove(p[i + 1]);
                        n--;
                        break;
                    }
                }
            triangles.Add(new Tuple<Point, Point, Point>(p[n - 1], p[n - 2], p[0]));
            if (cn > 3)
                label3.Text = Convert.ToString(aria_poligon);
        }

        private double Aria(PointF p1, PointF p2, PointF p3)
        {
            return Math.Abs(0.5 * Sarrus(p1, p2, p3));
        }

        private bool isdiagonala(int i, int j)
        {
            int nr_diagonale = 0;
            Tuple<int, int>[] diagonale = new Tuple<int, int>[n - 3];
            bool intersectie = false;
            for (int k = 0; k < n - 1; k++)
                if (i != k && i != (k + 1) && j != k && j != (k + 1) && intersecteaza(p[i], p[j], p[k], p[k + 1]))
                {
                    intersectie = true;
                    break;
                }
            if (i != n - 1 && i != 0 && j != n - 1 && j != 0 && intersecteaza(p[i], p[j], p[n - 1], p[0]))
                intersectie = true;
            if (!intersectie)
            {
                if (in_interiorul_poligonului(i, j))
                {
                    diagonale[nr_diagonale] = new Tuple<int, int>(i, j);
                    nr_diagonale++;
                    return true;
                }
            }
            return false;
        }

        private bool intersecteaza(PointF s1, PointF s2, PointF p1, PointF p2)
        {
            if (Sarrus(p2, p1, s1) * Sarrus(p2, p1, s2) <= 0 && Sarrus(s2, s1, p1) * Sarrus(s2, s1, p2) <= 0)
                return true;
            return false;
        }

        private double Sarrus(PointF p1, PointF p2, PointF p3)
        {
            return p1.X * p2.Y + p2.X * p3.Y + p3.X * p1.Y - p3.X * p2.Y - p2.X * p1.Y - p1.X * p3.Y;
        }

        private bool in_interiorul_poligonului(int pi, int pj)
        {
            int pi_ant = (pi > 0) ? pi - 1 : n - 1;
            int pi_urm = (pi < n - 1) ? pi + 1 : 0;
            if ((convex(pi) && intoarcere_spre_stanga(pi, pj, pi_urm) && intoarcere_spre_stanga(pi, pi_ant, pj)) ||
            (reflex(pi) && !(intoarcere_spre_dreapta(pi, pj, pi_urm) && intoarcere_spre_dreapta(pi, pi_ant, pj))))
                return true;
            return false;
        }

        private bool convex(int p)
        {
            int p_ant = (p > 0) ? p - 1 : n - 1;
            int p_urm = (p < n - 1) ? p + 1 : 0;
            return intoarcere_spre_dreapta(p_ant, p, p_urm);
        }

        private bool reflex(int p)
        {
            int p_ant = (p > 0) ? p - 1 : n - 1;
            int p_urm = (p < n - 1) ? p + 1 : 0;
            return intoarcere_spre_stanga(p_ant, p, p_urm);
        }

        private bool intoarcere_spre_stanga(int p1, int p2, int p3)
        {
            if (Sarrus(p[p1], p[p2], p[p3]) < 0)
                return true;
            return false;
        }

        private bool intoarcere_spre_dreapta(int p1, int p2, int p3)
        {
            if (Sarrus(p[p1], p[p2], p[p3]) > 0)
                return true;
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pen[] pens = new Pen[] { new Pen(Color.Blue, 3), new Pen(Color.Green, 3), new Pen(Color.Red, 3) };
            List<Tuple<Point, int>> varfuriMarcate = new List<Tuple<Point, int>>();
            for (int i = triangles.Count - 1; i >= 0; i--)
            {
                int colorat1 = EsteMarcat(triangles[i].Item1, varfuriMarcate);
                int colorat2 = EsteMarcat(triangles[i].Item2, varfuriMarcate);
                int colorat3 = EsteMarcat(triangles[i].Item3, varfuriMarcate);

                if (colorat1 == -1 && colorat2 == -1 && colorat3 == -1)
                {
                    varfuriMarcate.Add(new Tuple<Point, int>(triangles[i].Item1, 0));
                    g.DrawEllipse(pens[0], triangles[i].Item1.X - 8, triangles[i].Item1.Y - 8, 16, 16);
                    varfuriMarcate.Add(new Tuple<Point, int>(triangles[i].Item2, 1));
                    g.DrawEllipse(pens[1], triangles[i].Item2.X - 8, triangles[i].Item2.Y - 8, 16, 16);
                    varfuriMarcate.Add(new Tuple<Point, int>(triangles[i].Item3, 2));
                    g.DrawEllipse(pens[2], triangles[i].Item3.X - 8, triangles[i].Item3.Y - 8, 16, 16);
                }
                else if (colorat1 == -1)
                {
                    varfuriMarcate.Add(new Tuple<Point, int>(triangles[i].Item1, CuloareLipsa(colorat2, colorat3)));
                    g.DrawEllipse(pens[CuloareLipsa(colorat2, colorat3)], triangles[i].Item1.X - 8, triangles[i].Item1.Y - 8, 16, 16);
                }
                else if (colorat2 == -1)
                {
                    varfuriMarcate.Add(new Tuple<Point, int>(triangles[i].Item2, CuloareLipsa(colorat1, colorat3)));
                    g.DrawEllipse(pens[CuloareLipsa(colorat1, colorat3)], triangles[i].Item2.X - 8, triangles[i].Item2.Y - 8, 16, 16);
                }
                else if (colorat3 == -1)
                {
                    varfuriMarcate.Add(new Tuple<Point, int>(triangles[i].Item3, CuloareLipsa(colorat1, colorat2)));
                    g.DrawEllipse(pens[CuloareLipsa(colorat1, colorat2)], triangles[i].Item3.X - 8, triangles[i].Item3.Y - 8, 16, 16);
                }
            }
        }

        private int EsteMarcat(Point punct, List<Tuple<Point, int>> varfuriMarcate)
        {
            for (int i = 0; i < varfuriMarcate.Count; i++)
                if (varfuriMarcate[i].Item1 == punct)
                    return varfuriMarcate[i].Item2;
            return -1;
        }

        private int CuloareLipsa(int a, int b)
        {
            if ((a == 0 && b == 1) || (a == 1 && b == 0))
                return 2;
            if ((a == 0 && b == 2) || (a == 2 && b == 0))
                return 1;
            if ((a == 1 && b == 2) || (a == 2 && b == 1))
                return 0;
            return -1;
        }
    }
}
