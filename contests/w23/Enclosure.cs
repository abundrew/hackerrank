using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w23/challenges/enclosure
/// </summary>
class Solution6
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"4
1 2 1 2
");

        int N = int.Parse(tIn.ReadLine());
        double[] L = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();

        double[] X = new double[N];
        double[] Y = new double[N];

        double lmax = L.Max();
        double[] lother = L.OrderBy(p => p).Take(L.Length - 1).ToArray();
        double R = lmax / 2;
        if (lother.Select(p => Math.Acos((R * R * 2 - p * p) / (R * R * 2))).Sum() < Math.PI)
        {
            double Rmin = lmax / 2;
            double Rmax = Rmin * 2;
            while (lother.Select(p => Math.Acos((Rmax * Rmax * 2 - p * p) / (Rmax * Rmax * 2))).Sum() < Math.Acos((Rmax * Rmax * 2 - (lmax / 2) * (lmax / 2)) / (Rmax * Rmax * 2))) {
                Rmin = Rmax;
                Rmax *= 2;
            }
            R = Rmax;
            double[] angles = L.Select(p => Math.Acos((R * R * 2 - p * p) / (R * R * 2)) * (p == lmax ? -1 : 1)).ToArray();
            while (Rmax - Rmin > 0.00001)
            {
                R = (Rmin + Rmax) / 2;
                angles = L.Select(p => Math.Acos((R * R * 2 - p * p) / (R * R * 2)) * (p == lmax ? -1 : 1)).ToArray();
                if (angles.Sum() < 0) Rmin = R; else Rmax = R;
            }
            double Yc = L[0] / 2;
            double Xc = Math.Sqrt(R * R - Yc * Yc);

            if (L[0] == lmax)
                for (int i = 0; i < angles.Length; i++)
                    angles[i] = -angles[i];

            double angle1 = Math.PI - angles[0] / 2;

            Y[1] = L[0];
            double angle = angle1;
            for (int i = 2; i < N; i++)
            {
                angle -= angles[i - 1];
                X[i] = Xc + R * Math.Cos(angle);
                Y[i] = Yc + R * Math.Sin(angle);
            }
        }
        else
        {
            double Rmin = L.Sum() / (Math.PI * 2);
            double Rmax = Rmin * 2;
            R = Rmax;
            double[] angles = L.Select(p => Math.Acos((R * R * 2 - p * p) / (R * R * 2))).ToArray();

            while (Rmax - Rmin > 0.00001)
            {
                R = (Rmin + Rmax) / 2;
                angles = L.Select(p => Math.Acos((R * R * 2 - p * p) / (R * R * 2))).ToArray();
                if (angles.Sum() > Math.PI * 2) Rmin = R; else Rmax = R;
            }

            double Yc = L[0] / 2;
            double Xc = Math.Sqrt(R * R - Yc * Yc);

            double angle1 = Math.PI - angles[0] / 2;

            Y[1] = L[0];
            double angle = angle1;
            for (int i = 2; i < N; i++)
            {
                angle -= angles[i - 1];
                X[i] = Xc + R * Math.Cos(angle);
                Y[i] = Yc + R * Math.Sin(angle);
            }
        }
        for (int i = 0; i < N; i++)
        {
            tOut.WriteLine(X[i]);
            tOut.WriteLine(Y[i]);
            tOut.WriteLine();
        }

        tIn.ReadLine();
    }
}
