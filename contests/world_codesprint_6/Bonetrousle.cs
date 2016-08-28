using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-6/challenges/bonetrousle
/// </summary>
class Solution5
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"4
12 8 3
10 3 3
9 10 2
9 10 2
");

        int T = int.Parse(tIn.ReadLine());
        for (int t = 0; t < T; t++)
        {
            long[] nkb = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();
            long N = nkb[0];
            long K = nkb[1];
            long B = nkb[2];

            long[] P = new long[B];
            int ix = 0;

            while (ix < P.Length && N > 0 && K > 0 && B > 0)
            {
                long min = B * (B + 1) / 2;
                if (N < min) break;
                if (N == min)
                {
                    for (int i = 0; i < B; i++) P[ix + i] = i + 1;
                    N -= min;
                    break;
                }

                P[ix] = Math.Min(K, N - (B - 1) * B / 2);
                K = P[ix] - 1;
                N -= P[ix];
                B--;
                ix++;
            }

            tOut.WriteLine(N > 0 ? "-1" : string.Join(" ", P.Select(p => p.ToString()).ToArray()));
        }

        tIn.ReadLine();
    }
}
