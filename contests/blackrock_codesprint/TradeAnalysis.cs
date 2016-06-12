using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/blackrock-codesprint/challenges/trade-analysis
/// </summary>
class Solution7
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
1 2 3
");

        tIn.ReadLine();
        long[] A = tIn.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();

        long MOD = 1000000007;
        long GS = 0;

        long[] X = new long[A.Length];

        for (int i = 0; i < A.Length; i++)
        {
            long[] Y = new long[A.Length];
            Y[0] = A[i];

            GS += A[i];
            GS %= MOD;

            for (int k = 0; k < i; k++) 
            {
                Y[k + 1] = X[k] * A[i];
                Y[k + 1] %= MOD;

                GS += Y[k + 1] * (k + 1 + 1);
                GS %= MOD;
            }

            for (int k = 0; k <= i; k++)
            {
                X[k] += Y[k];
                X[k] %= MOD;
            }
        }

        tOut.WriteLine(GS);

        Console.ReadLine();
    }
}
