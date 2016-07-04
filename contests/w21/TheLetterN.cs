using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w21/challenges/n-letter
/// </summary>
class Solution5
{
    class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool GoodForN(int x0, int y0, int x10, int y10, int x2, int y2)
        {
            return (x2 - x0) * y10 - (y2 - y0) * x10 > 0 && (x2 - x0) * x10 + (y2 - y0) * y10 >= 0;
        }
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"4
0 0
0 2
2 0
2 2
");

        int N = int.Parse(tIn.ReadLine());
        Point[] points = new Point[N];
        for (int i = 0; i < N; i++)
        {
            int[] xy = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            points[i] = new Point(xy[0], xy[1]);
        }

        long K = 0;

        for (int ib = 0; ib < N - 1; ib++)
            for (int ic = ib + 1; ic < N; ic++)
            {
                int ax = 0;
                int dx = 0;
                int cbx = points[ic].X - points[ib].X;
                int cby = points[ic].Y - points[ib].Y;
                int bcx = -cbx;
                int bcy = -cby;
                for (int ip = 0; ip < N; ip++)
                {
                    if (ip == ib || ip == ic) continue;

                    if (Point.GoodForN(points[ib].X, points[ib].Y, cbx, cby, points[ip].X, points[ip].Y)) ax++;
                    else
                    if (Point.GoodForN(points[ic].X, points[ic].Y, bcx, bcy, points[ip].X, points[ip].Y)) dx++;
                }

                K += ax * dx;
            }

        tOut.WriteLine(K);

        tIn.ReadLine();
    }
}
