using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/june-world-codesprint/challenges/minimum-distances
/// </summary>
class Solution1
{
    static int[] A = null;

    class AComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return A[x].CompareTo(A[y]);
        }
    }


    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

                tIn = new StringReader(@"6
7 1 3 4 1 7");

        tIn.ReadLine();
        A = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int[] X = Enumerable.Range(0, A.Length).ToArray();
        Array.Sort(X, new AComparer());

        int minD = int.MaxValue;
        for (int i = 0; i < A.Length - 1; i++)
            if (A[X[i]] == A[X[i + 1]] && Math.Abs(X[i] - X[i + 1]) < minD) minD = Math.Abs(X[i] - X[i + 1]);

        tOut.WriteLine(minD < int.MaxValue ? minD : -1);

        tIn.ReadLine();
    }
}
