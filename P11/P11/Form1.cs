using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace P11
{
    //Partiționarea unui poligon simplu în poligoane monotone si triangularea poligoanelor monotone obtinute.

    public partial class Form1 : Form
    {
        Graphics g;
        Pen p1, p2;
        const int raza = 2;
        int n = 0;
        List<Point> p = new List<Point>();
        List<Tuple<Point, Point>> diagonale_rosii = new List<Tuple<Point, Point>>();
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
            if (n < 3)
                return;
            if (!poligon_inchis)
            {
                if (n < 3)
                    return;
                g.DrawLine(p1, p[n - 1], p[0]);
                poligon_inchis = true;
            }

            p1 = new Pen(Color.Red);
            int j = 0;

            if (n >= 3)
            {
                for (int i = 0; i < n; i++)
                {
                    if (reflex(i))
                    {
                        if (i == 0)
                        {
                            if (p[n - 1].Y > p[i].Y && p[i + 1].Y > p[i].Y)
                                if (Primul_deasupra(i) != -1)
                                {
                                    j = Primul_deasupra(i);
                                    g.DrawLine(p1, p[i], p[j]);
                                    diagonale_rosii.Add(new Tuple<Point, Point>(p[i], p[j]));
                                    List<Point> puncte = new List<Point>();
                                    if (i > j)
                                    {
                                        int aux = i;
                                        i = j;
                                        j = aux;
                                    }
                                    for (int k1 = i; k1 <= j; k1++)
                                        puncte.Add(p[k1]);
                                    Triangulare(i, j, puncte);
                                    puncte.Clear();
                                }
                            if (p[n - 1].Y < p[i].Y && p[i + 1].Y < p[i].Y)
                                if (Primul_dedesubt(i) != -1)
                                {
                                    j = Primul_dedesubt(i);
                                    g.DrawLine(p1, p[i], p[j]);
                                    diagonale_rosii.Add(new Tuple<Point, Point>(p[i], p[j]));
                                    List<Point> puncte = new List<Point>();
                                    if (i > j)
                                    {
                                        int aux = i;
                                        i = j;
                                        j = aux;
                                    }
                                    for (int k1 = i; k1 <= j; k1++)
                                        puncte.Add(p[k1]);
                                    Triangulare(i, j, puncte);
                                    puncte.Clear();
                                }
                        }
                        else if (i == n - 1)
                        {
                            if (p[i - 1].Y > p[i].Y && p[0].Y > p[i].Y)
                                if (Primul_deasupra(i) != -1)
                                {
                                    j = Primul_deasupra(i);
                                    g.DrawLine(p1, p[i], p[j]);
                                    diagonale_rosii.Add(new Tuple<Point, Point>(p[i], p[j]));
                                    List<Point> puncte = new List<Point>();
                                    if (i > j)
                                    {
                                        int aux = i;
                                        i = j;
                                        j = aux;
                                    }
                                    for (int k1 = i; k1 <= j; k1++)
                                        puncte.Add(p[k1]);
                                    Triangulare(i, j, puncte);
                                    puncte.Clear();
                                }
                            if (p[i - 1].Y < p[i].Y && p[0].Y < p[i].Y)
                                if (Primul_dedesubt(i) != -1)
                                {
                                    j = Primul_dedesubt(i);
                                    g.DrawLine(p1, p[i], p[j]);
                                    diagonale_rosii.Add(new Tuple<Point, Point>(p[i], p[j]));
                                    List<Point> puncte = new List<Point>();
                                    if (i > j)
                                    {
                                        int aux = i;
                                        i = j;
                                        j = aux;
                                    }
                                    for (int k1 = i; k1 <= j; k1++)
                                        puncte.Add(p[k1]);
                                    Triangulare(i, j, puncte);
                                    puncte.Clear();
                                }
                        }        
                        else
                        {
                            if (p[i - 1].Y > p[i].Y && p[i + 1].Y > p[i].Y)
                                if (Primul_deasupra(i) != -1)
                                {
                                    j = Primul_deasupra(i);
                                    g.DrawLine(p1, p[i], p[j]);
                                    diagonale_rosii.Add(new Tuple<Point, Point>(p[i], p[j]));
                                    List<Point> puncte = new List<Point>();
                                    if (i > j)
                                    {
                                        int aux = i;
                                        i = j;
                                        j = aux;
                                    }
                                    for (int k1 = i; k1 <= j; k1++)
                                        puncte.Add(p[k1]);
                                    Triangulare(i, j, puncte);
                                    puncte.Clear();
                                }
                            if (p[i - 1].Y < p[i].Y && p[i + 1].Y < p[i].Y)
                                if (Primul_dedesubt(i) != -1)
                                {
                                    j = Primul_dedesubt(i);
                                    g.DrawLine(p1, p[i], p[j]);
                                    diagonale_rosii.Add(new Tuple<Point, Point>(p[i], p[j]));
                                    List<Point> puncte = new List<Point>();
                                    if (i > j)
                                    {
                                        int aux = i;
                                        i = j;
                                        j = aux;
                                    }
                                    for (int k1 = i; k1 <= j; k1++)
                                        puncte.Add(p[k1]);
                                    Triangulare(i, j, puncte);
                                    puncte.Clear();
                                }
                        }
                    }
                }
                //List<Point> puncte1 = new List<Point>();
                //p.Add(p[0]);
                //n++;
                //p.Add(p[1]);
                //n++;
                //for (int k1 = j; k1 < n; k1++)
                    //puncte1.Add(p[k1]);
                //Triangulare(j, n - 1, puncte1);
                //puncte1.Clear();
                p2 = new Pen(Color.Orange);
                g.DrawLine(p2, p[n - 1], p[1]);
            }
        }

        private void Triangulare(int k1, int k2, List<Point> puncte)
        {
            if (puncte.Count <= 3)
                return;

            p2 = new Pen(Color.Orange);
            int nr_diagonale = 0;
            Tuple<int, int>[] diagonale = new Tuple<int, int>[k2 - k1 - 2];

            for (int i = k1; i < k2 - 1; i++)
                for (int j = i + 2; j <= k2; j++)
                {
                    if (i == k1 && j == k2)
                        break;
                    bool intersectie = false;
                    for (int k = k1; k < k2 - 1; k++)
                        if (i != k && i != (k + 1) && j != k && j != (k + 1) && intersecteaza(p[i], p[j], p[k], p[k + 1]))
                        {
                            intersectie = true;
                            break;
                        }
                    if (i != k2 - 1 && i != k1 && j != k2 - 1 && j != k1 && intersecteaza(p[i], p[j], p[k2 - 1], p[k1]))
                        intersectie = true;
                    if (!intersectie)
                    {
                        for (int k = 0; k < nr_diagonale; k++)
                            if (i != diagonale[k].Item1 && i != diagonale[k].Item2 && j != diagonale[k].Item1 && j != diagonale[k].Item2 && intersecteaza(p[i], p[j], p[diagonale[k].Item1], p[diagonale[k].Item2]))
                            {
                                intersectie = true;
                                break;
                            }
                        if (!intersectie)
                            if (in_interiorul_poligonului_monoton(i, j, k1, k2)) //&& nu_intersecteaza_alta_diagonala(i, j, diagonale, nr_diagonale)) //&& nu_intersecteaza_diagonale_rosii(i, j))
                            {
                                g.DrawLine(p2, p[i], p[j]);
                                diagonale[nr_diagonale] = new Tuple<int, int>(i, j);
                                nr_diagonale++;
                            }
                    }

                    if (nr_diagonale == k2 - k1 - 2)
                        return;
                }
        }

        private bool in_interiorul_poligonului_monoton(int pi, int pj, int k1, int k2)
        {
            int pi_ant = (pi > k1) ? pi - 1 : k2 - 1;
            int pi_urm = (pi < k2 - 1) ? pi + 1 : k1;
            if ((convex(pi) && intoarcere_spre_stanga(pi, pj, pi_urm) && intoarcere_spre_stanga(pi, pi_ant, pj)) ||
            (reflex(pi) && !(intoarcere_spre_dreapta(pi, pj, pi_urm) && intoarcere_spre_dreapta(pi, pi_ant, pj))))
                return true;
            return false;
        }

        private bool nu_intersecteaza_diagonale_rosii(int i, int j)
        {
            Point punctul1 = p[i];
            Point punctul2 = p[j];
            for (int g = 0; g < diagonale_rosii.Count; g++)
            {
                Point punctul3 = diagonale_rosii[g].Item1;
                Point punctul4 = diagonale_rosii[g].Item2;
                if (Sarrus(punctul4, punctul3, punctul1) * Sarrus(punctul4, punctul3, punctul2) <= 0 && Sarrus(punctul2, punctul1, punctul3) * Sarrus(punctul2, punctul1, punctul4) <= 0)
                    return false;
            }
            return true;
        }

        private bool nu_intersecteaza_alta_diagonala(int i, int j, Tuple<int, int>[] diagonale, int nr_diagonale)
        {
            Point punctul1 = p[i];
            Point punctul2 = p[j];
            for (int g = 0; g < nr_diagonale; g++)
            {
                Point punctul3 = p[diagonale[g].Item1];
                Point punctul4 = p[diagonale[g].Item2];
                if (Sarrus(punctul4, punctul3, punctul1) * Sarrus(punctul4, punctul3, punctul2) <= 0 && Sarrus(punctul2, punctul1, punctul3) * Sarrus(punctul2, punctul1, punctul4) <= 0)
                    return false;
            }
            return true;
        }

        private int Primul_dedesubt(int i)
        {
            for (int k = p[i].Y + 1; k < Height; k++)
                for (int h = 0; h < n; h++)
                    if (p[h].Y == k && isdiagonala(i, h))
                        return h;
            return -1;
        }

        private int Primul_deasupra(int i)
        {
            for (int k = p[i].Y - 1; k >= 0; k--)
                for (int h = 0; h < n; h++)
                    if (p[h].Y == k && isdiagonala(i, h))
                        return h;
            return -1;
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

        private bool intersecteaza(Point s1, Point s2, Point p1, Point p2)
        {
            if (Sarrus(p2, p1, s1) * Sarrus(p2, p1, s2) <= 0 && Sarrus(s2, s1, p1) * Sarrus(s2, s1, p2) <= 0)
                return true;
            return false;
        }

        private double Sarrus(Point p1, Point p2, Point p3)
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
    }
}
