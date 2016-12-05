using System;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w26/challenges/hard-homework
/// </summary>
class Solution6
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3000000");

        int N = int.Parse(tIn.ReadLine());

        int xmax = 1;
        int ymax = 1;
        int zmax = N - 2;
        double max = Math.Sin(xmax) * Math.Sin(ymax) * Math.Sin(zmax);

        for (int x = 1; x <= (N - 1) / 2; x++)
        {
            int z = N - (x + x);
            double sinx = Math.Sin(x);
            double sinz = Math.Sin(z);
            if (sinx + sinx + sinz > max)
            {
                max = sinx + sinx + sinz;
                xmax = x;
                ymax = x;
                zmax = z;
            }
        }

        for (int z = 1; z <= N - 2; z++)
        {
            double sinz = Math.Sin(z);
            if (max - sinz >= 2) continue;

            int m = N - z;

            double sinm = Math.Sin(m);
            double cosm = Math.Cos(m);

            double x0 = Math.Atan2(1 - cosm, sinm);
            double x1 = Math.Atan2(cosm - 1, -sinm);

            double f0 = Math.Sin(x0) * (1 - cosm) + Math.Cos(x0) * sinm;
            double f1 = Math.Sin(x1) * (1 - cosm) + Math.Cos(x1) * sinm;

            if (f0 + sinz <= max && f1 + sinz <= max) continue;

            for (int x = 1; x <= m / 2; x++)
            {
                int y = m - x;
                double sinx = Math.Sin(x);
                double siny = Math.Sin(y);
                if (sinx + siny + sinz > max)
                {
                    max = sinx + siny + sinz;
                    xmax = x;
                    ymax = y;
                    zmax = z;
                }
            }
        }

        tOut.WriteLine(max.ToString("0.000000000"));

        tIn.ReadLine();
    }
}
