using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/stryker-codesprint/challenges/escaping-the-force-field
/// </summary>
class Solution6
{
    // ---------------------------------------------------------------------
    public class P3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
    // ---------------------------------------------------------------------
    public class V3D : P3D
    {
        public V3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public V3D(P3D p) : this(p.X, p.Y, p.Z) { }
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
    // ---------------------------------------------------------------------
    public static double PointToTriangleDistance(P3D p0, P3D p1, P3D p2, P3D p3)
    {
        V3D B = new V3D(p1);
        V3D E0 = V3D.Sub(B, new V3D(p2));
        V3D E1 = V3D.Sub(B, new V3D(p3));

        V3D D = V3D.Sub(new V3D(p0), B);
        double a = V3D.Dot(E0, E0);
        double b = V3D.Dot(E0, E1);
        double c = V3D.Dot(E1, E1);
        double d = V3D.Dot(E0, D);
        double e = V3D.Dot(E1, D);
        double f = V3D.Dot(D, D);

        double det = a * c - b * b;
        double s = b * e - c * d;
        double t = b * d - a * e;

        double sqrDistance = 0;

        if ((s + t) <= det)
        {
            if (s < 0)
            {
                if (t < 0)
                {
                    // region4
                    if (d < 0)
                    {
                        t = 0;
                        if (-d >= a)
                        {
                            s = 1;
                            sqrDistance = a + 2 * d + f;
                        }
                        else
                        {
                            s = -d / a;
                            sqrDistance = d * s + f;
                        }
                    }
                    else
                    {
                        s = 0;
                        if (e >= 0)
                        {
                            t = 0;
                            sqrDistance = f;
                        }
                        else
                        {
                            if (-e >= c)
                            {
                                t = 1;
                                sqrDistance = c + 2 * e + f;
                            }
                            else
                            {
                                t = -e / c;
                                sqrDistance = e * t + f;
                            }
                        }
                    }
                    // region 4
                }
                else
                {
                    // region 3
                    s = 0;
                    if (e >= 0)
                    {
                        t = 0;
                        sqrDistance = f;
                    }
                    else
                    {
                        if (-e >= c)
                        {
                            t = 1;
                            sqrDistance = c + 2 * e + f;
                        }
                        else
                        {
                            t = -e / c;
                            sqrDistance = e * t + f;
                        }
                    }
                    // region 3
                }
            }
            else
            {
                if (t < 0)
                {
                    // region 5
                    t = 0;
                    if (d >= 0)
                    {
                        s = 0;
                        sqrDistance = f;
                    }
                    else
                    {
                        if (-d >= a)
                        {
                            s = 1;
                            sqrDistance = a + 2 * d + f;
                        }
                        else
                        {
                            s = -d / a;
                            sqrDistance = d * s + f;
                        }
                    }
                    // region 5
                }
                else
                {
                    // region 0
                    double invDet = 1 / det;
                    s = s * invDet;
                    t = t * invDet;
                    sqrDistance = s * (a * s + b * t + 2 * d) + t * (b * s + c * t + 2 * e) + f;
                    // region 0
                }
            }
        }
        else
        {
            if (s < 0)
            {
                // region 2
                double tmp0 = b + d;
                double tmp1 = c + e;
                if (tmp1 > tmp0)
                {
                    double numer = tmp1 - tmp0;
                    double denom = a - 2 * b + c;
                    if (numer >= denom)
                    {
                        s = 1;
                        t = 0;
                        sqrDistance = a + 2 * d + f;
                    }
                    else
                    {
                        s = numer / denom;
                        t = 1 - s;
                        sqrDistance = s * (a * s + b * t + 2 * d) + t * (b * s + c * t + 2 * e) + f;
                    }
                }
                else
                {
                    s = 0;
                    if (tmp1 <= 0)
                    {
                        t = 1;
                        sqrDistance = c + 2 * e + f;
                    }
                    else
                    {
                        if (e >= 0)
                        {
                            t = 0;
                            sqrDistance = f;
                        }
                        else
                        {
                            t = -e / c;
                            sqrDistance = e * t + f;
                        }
                    }
                }
                // region 2
            }
            else
            {
                if (t < 0)
                {
                    // region6
                    double tmp0 = b + e;
                    double tmp1 = a + d;
                    if (tmp1 > tmp0)
                    {
                        double numer = tmp1 - tmp0;
                        double denom = a - 2 * b + c;
                        if (numer >= denom)
                        {
                            t = 1;
                            s = 0;
                            sqrDistance = c + 2 * e + f;
                        }
                        else
                        {
                            t = numer / denom;
                            s = 1 - t;
                            sqrDistance = s * (a * s + b * t + 2 * d) + t * (b * s + c * t + 2 * e) + f;
                        }
                    }
                    else
                    {
                        t = 0;
                        if (tmp1 <= 0)
                        {
                            s = 1;
                            sqrDistance = a + 2 * d + f;
                        }
                        else
                        {
                            if (d >= 0)
                            {
                                s = 0;
                                sqrDistance = f;
                            }
                            else
                            {
                                s = -d / a;
                                sqrDistance = d * s + f;
                            }
                        }
                    }
                    // region 6
                }
                else
                {
                    // region 1
                    double numer = c + e - b - d;
                    if (numer <= 0)
                    {
                        s = 0;
                        t = 1;
                        sqrDistance = c + 2 * e + f;
                    }
                    else
                    {
                        double denom = a - 2 * b + c;
                        if (numer >= denom)
                        {
                            s = 1;
                            t = 0;
                            sqrDistance = a + 2 * d + f;
                        }
                        else
                        {
                            s = numer / denom;
                            t = 1 - s;
                            sqrDistance = s * (a * s + b * t + 2 * d) + t * (b * s + c * t + 2 * e) + f;
                        }
                    }
                    // region 1
                }
            }
        }

        if (sqrDistance < 0) sqrDistance = 0;

        double dist = Math.Sqrt(sqrDistance);

        //if nargout > 1
        //  PP0 = B + s * E0 + t * E1;
        //end

        return dist;
    }
    // ---------------------------------------------------------------------
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"12
8
3.00 -2.50 18.00
0 1 3
0 3 2
5 4 7
7 4 6
5 7 1
7 3 1
4 5 0
5 1 0
3 7 2
7 6 2
6 2 0
6 0 4
-15.00 -7.50 0.00
-15.00 -7.50 20.00
-15.00 7.50 0.00
-15.00 7.50 20.00
15.00 -7.50 0.00
15.00 -7.50 20.00
15.00 7.50 0.00
15.00 7.50 20.00
");

        int N = int.Parse(tIn.ReadLine());
        int M = int.Parse(tIn.ReadLine());

        double[] xyz = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();
        P3D P0 = new P3D() { X = xyz[0], Y = xyz[1], Z = xyz[2] };

        int[][] T = new int[N][];
        for (int i = 0; i < N; i++)
            T[i] = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        P3D[] P = new P3D[M];
        for (int i = 0; i < M; i++)
        {
            xyz = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();
            P[i] = new P3D() { X = xyz[0], Y = xyz[1], Z = xyz[2] };
        }

        double dmin = PointToTriangleDistance(P0, P[T[0][0]], P[T[0][1]], P[T[0][2]]);
        int tmin = 0;

        for (int i = 1; i < N; i++)
        {
            double d = PointToTriangleDistance(P0, P[T[i][0]], P[T[i][1]], P[T[i][2]]);
            if (d < dmin)
            {
                dmin = d;
                tmin = i;
            }
        }

        tOut.WriteLine("{0:0.00}", dmin);
        tOut.WriteLine(tmin);

        tIn.ReadLine();
    }
}
