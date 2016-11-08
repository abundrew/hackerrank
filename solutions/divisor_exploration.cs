using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/infinitum16/challenges/divisor-exploration
/// </summary>
class Solution3
{
    const int R = 1000000007;
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        int D = int.Parse(tIn.ReadLine());
        int[] M = new int[D];
        int[] A = new int[D];
        for (int d = 0; d < D; d++)
        {
            int[] ma = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
            M[d] = ma[0];
            A[d] = ma[1];
        }

        long[] RX = new long[200001];
        int minA1 = A.Min() + 1;
        int maxAM = Enumerable.Range(0, D).Select(p => A[p] + M[p]).Max();
        for (int x = minA1; x <= maxAM; x++)
        {
            RX[x] = (long)(x + 1) * (x + 2) / 2;
            RX[x] %= R;
        }

        long[] RX10 = new long[1000];
        for (int x10 = 0; x10 < RX10.Length; x10++)
            if (x10 * 200 >= minA1 && x10 * 200 + 199 <= maxAM)
            {
                RX10[x10] = 1;
                for (int x = x10 * 200; x <= x10 * 200 + 199; x++)
                {
                    RX10[x10] *= RX[x];
                    RX10[x10] %= R;
                }
            }

        for (int d = 0; d < D; d++)
        {
            long s = 1;
            int x = A[d] + 1;
            while (x <= A[d] + M[d])
            {
                if (x % 200 == 0 && x + 199 < A[d] + M[d])
                {
                    s *= RX10[x / 200];
                    s %= R;
                    x += 200;
                    continue;
                }

                s *= RX[x];
                s %= R;
                x++;
            }
            tOut.WriteLine(s);
        }
    }
}
