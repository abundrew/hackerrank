using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/infinitum16/challenges/hyperspace-travel
/// </summary>
class Solution2
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3 2
1 1
2 2
3 3
");

        int[] nm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nm[0];
        int M = nm[1];

        int[][] P = new int[N][];
        for (int i = 0; i < N; i++)
            P[i] = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        int[] X0 = new int[M];
        for (int i = 0; i < M; i++)
        {
            int[] X = new int[N];
            for (int j = 0; j < N; j++)
                X[j] = P[j][i];
            Array.Sort(X);
            X0[i] = X[(X.Length - 1) / 2];
        }

        tOut.WriteLine(string.Join(" ", X0.Select(p => p.ToString()).ToArray()));

        tIn.ReadLine();
    }
}
