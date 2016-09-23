using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/stryker-codesprint/challenges/cylinder-slice
/// </summary>
class Solution7
{
    class P2D
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
    class V3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public V3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public V3D(V3D v) : this(v.X, v.Y, v.Z) { }
        public static V3D Add(V3D v1, V3D v2)
        {
            return new V3D(v2.X + v1.X, v2.Y + v1.Y, v2.Z + v1.Z);
        }
        public static V3D Sub(V3D v1, V3D v2)
        {
            return new V3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static V3D Cross(V3D v1, V3D v2)
        {
            return new V3D(
                v1.Y * v2.Z - v2.Y * v1.Z,
                (v1.X * v2.Z - v2.X * v1.Z) * -1,
                v1.X * v2.Y - v2.X * v1.Y
            );
        }
        public static double Dot(V3D v1, V3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
        public static V3D Mul(V3D v, double k)
        {
            return new V3D(v.X * k, v.Y * k, v.Z * k);
        }
        public static V3D Div(V3D v, double k)
        {
            return new V3D(v.X / k, v.Y / k, v.Z / k);
        }
        public static double Mag(V3D v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }
        public static V3D Nrm(V3D v)
        {
            double mag = Mag(v);
            return V3D.Div(v, mag);
        }
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5.000000 0.000000
1.000000 2.000000
-3.000000 0.000000
1.000000 -2.000000
0.000000 0.000000 0.000000
1.000000 0.000000 0.000000
0.000000 1.000000 0.000000
");

        double[] xyz = null;

        P2D[] ellipse2D = new P2D[4];
        for (int i = 0; i < 4; i++)
        {
            xyz = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();
            ellipse2D[i] = new P2D() { X = xyz[0], Y = xyz[1] };
        }
        xyz = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();
        V3D planeO = new V3D(xyz[0], xyz[1], xyz[2]);
        xyz = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();
        V3D planeX = new V3D(xyz[0], xyz[1], xyz[2]);
        xyz = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();
        V3D planeY = new V3D(xyz[0], xyz[1], xyz[2]);
        V3D planeZ = V3D.Cross(planeX, planeY);

        V3D[] ellipse3D = new V3D[4];
        for (int i = 0; i < 4; i++)
            ellipse3D[i] = V3D.Add(new V3D(planeO), V3D.Add(V3D.Mul(planeX, ellipse2D[i].X), V3D.Mul(planeY, ellipse2D[i].Y)));

        V3D C = V3D.Add(new V3D(ellipse3D[0]), V3D.Div(V3D.Sub(new V3D(ellipse3D[2]), new V3D(ellipse3D[0])), 2));

        V3D ve02 = V3D.Sub(new V3D(ellipse3D[2]), new V3D(ellipse3D[0]));
        V3D ve13 = V3D.Sub(new V3D(ellipse3D[1]), new V3D(ellipse3D[3]));

        double re02 = V3D.Mag(ve02) / 2;
        double re13 = V3D.Mag(ve13) / 2;

        double remax = Math.Max(re02, re13);
        double remin = Math.Min(re02, re13);
        double R = remin;

        double rh = Math.Sqrt(remax * remax - remin * remin) / remin;
        V3D vmax = remax == re02 ? V3D.Nrm(ve02): V3D.Nrm(ve13);

        V3D[] va = new V3D[4];
        va[0] = V3D.Nrm(V3D.Add(planeZ, V3D.Mul(vmax, rh)));
        va[1] = V3D.Nrm(V3D.Add(planeZ, V3D.Mul(vmax, -rh)));
        va[2] = V3D.Nrm(V3D.Add(V3D.Mul(planeZ, -1), V3D.Mul(vmax, rh)));
        va[3] = V3D.Nrm(V3D.Add(V3D.Mul(planeZ, -1), V3D.Mul(vmax, -rh)));

        V3D a = va[0];
        double alphaMinX = Math.Acos(V3D.Dot(planeX, va[0]) / (V3D.Mag(planeX) * V3D.Mag(va[0])));
        double alphaMinY = Math.Acos(V3D.Dot(planeY, va[0]) / (V3D.Mag(planeY) * V3D.Mag(va[0])));

        for (int i = 1; i < 2; i++)
        {
            double alphaX = Math.Acos(V3D.Dot(planeX, va[i]) / (V3D.Mag(planeX) * V3D.Mag(va[i])));
            double alphaY = Math.Acos(V3D.Dot(planeY, va[i]) / (V3D.Mag(planeY) * V3D.Mag(va[i])));
            if (alphaMinX > Math.PI / 2 || (alphaX > 0 && alphaX <= Math.PI / 2 && alphaY < alphaMinY))
            {
                alphaMinX = alphaX;
                alphaMinY = alphaY;
                a = va[i];
            }
        }

        tOut.WriteLine("{0:0.00} {1:0.00} {2:0.00}", C.X, C.Y, C.Z);
        tOut.WriteLine("{0:0.00}", R);
        tOut.WriteLine("{0:0.00} {1:0.00} {2:0.00}", a.X, a.Y, a.Z);

        tIn.ReadLine();
    }
}
