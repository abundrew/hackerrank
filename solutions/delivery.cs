using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/walmart-codesprint-algo/challenges/delivery
/// </summary>
class Solution5
{
    static int Time(int a, int b)
    {
        int t = 0;
        while (a != b)
        {
            while (a > b) { a = (a - 1) / 2; t++; }
            while (b > a) { b = (b - 1) / 2; t++; }
        }
        return t;
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5 5 6
3 1 4 5
1 3
3 2 3 5
4 1 3 4 5
3 1 2 5
5 3
3 5
3 2
2 5
2 1
3 3");

        int[] nmq = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nmq[0];
        int M = nmq[1];
        int Q = nmq[2];

        int[][] F = new int[M][];
        for (int i = 0; i < M; i++)
        {
            int[] f = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
            F[i] = new int[f[0]];
            for (int j = 0; j < f[0]; j++) F[i][j] = f[j + 1] - 1;
        }

        long time = 0;
        int J = 0;

        for (int q = 0; q < Q; q++)
        {
            int[] fp = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p1 => int.Parse(p1)).ToArray();
            int f = fp[0] - 1;
            int p = fp[1] - 1;

            int tp = int.MaxValue;
            foreach (int r in F[f])
            {
                int tm = Time(J, r) + Time(r, p);
                if (tm < tp) tp = tm;
            }

            time += tp;
            J = p;
        }
        tOut.WriteLine(time);

        tIn.ReadLine();
    }
}
