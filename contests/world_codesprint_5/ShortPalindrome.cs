using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-5/challenges/short-palindrome
/// </summary>
class Solution3
{
    static long R = 1000000007;

    static long C4K(long k)
    {
        if (k < 4) return 0;
        long[] kk = new long[4];
        kk[0] = k;
        for (int i = 1; i < 4; i++) kk[i] = kk[i - 1] - 1;
        for (int i = 4; i > 1; i--)
            for (int j = 0; j < 4; j++)
                if (kk[j] % i == 0)
                {
                    kk[j] /= i;
                    break;
                }
        long c4k = 1;
        for (int i = 0; i < 4; i++)
        {
            c4k *= kk[i];
            c4k %= R;
        }
        return c4k;
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"aaaaz");

        byte valA = Convert.ToByte('a');

        int[] B = tIn.ReadLine().Select(p => Convert.ToByte(p) - valA).ToArray();
        int M = B.Max() + 1;

        long X = 0;

        long[] NN = new long[M];
        foreach (int b in B) NN[b]++;

        for (int i = 0; i < M; i++) X += C4K(NN[i]);

        long[] AA = new long[M];
        long[,] AB = new long[M, M];

        for (int a = 0; a < B.Length; a++)
        {

            for (int b = 0; b < M; b++)
            {
                if (b != B[a])
                {
                    X += AB[b, B[a]] * (NN[b] - AA[b]);
                    X %= R;
                }

                AB[b, B[a]] += AA[b];
                AB[b, B[a]] %= R;
            }
            AA[B[a]]++;
        }

        tOut.WriteLine(X);

        tIn.ReadLine();
    }
}
