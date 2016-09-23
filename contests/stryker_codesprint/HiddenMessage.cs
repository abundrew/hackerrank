using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/stryker-codesprint/challenges/the-hidden-message
/// </summary>
class Solution3
{
    static int LCS(string x, string y)
    {
        int[,] L = new int[x.Length + 1, y.Length + 1];
        for (int i = 0; i <= x.Length; i++)
        {
            for (int j = 0; j <= y.Length; j++)
            {
                if (i == 0 || j == 0)
                    L[i, j] = 0;

                else if (x[i - 1] == y[j - 1])
                    L[i, j] = L[i - 1, j - 1] + 1;

                else
                    L[i, j] = Math.Max(L[i - 1, j], L[i, j - 1]);
            }
        }
        return L[x.Length, y.Length];
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"acfarenbthedgesdg
far are the hedges
");

        string T = tIn.ReadLine();
        string phrase = tIn.ReadLine();
        string[] P = phrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        int N = P.Length;
        int[] xP = new int[N];
        int fP = 0;
        while (fP < N)
        {
            xP[fP] = T.IndexOf(P[fP], fP == 0 ? 0 : xP[fP - 1] + 1);
            if (xP[fP] == -1) break;
            fP++;
        }

        tOut.WriteLine(fP == N ? "YES" : "NO");
        tOut.WriteLine(fP > 0 ? string.Join(" ", Enumerable.Range(0, fP).Select(p => string.Format("{0} {1} {2}", P[p], xP[p], xP[p] + P[p].Length - 1)).ToArray()) : "0");
        if (fP == N)
            tOut.WriteLine(T.Length + phrase.Length - LCS(T, phrase) * 2);
        else
            tOut.WriteLine(0);

        tIn.ReadLine();
    }
}
