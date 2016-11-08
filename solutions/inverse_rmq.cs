using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-7/challenges/inverse-rmq
/// </summary>
class Solution5
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"8
-381858837 -460552444 -381858837 95397836 -460552444 855898381 -242860726 405278568 -460552444 982130115 -381858837 -460552444 95397836 981764727 855898381");

        int N = int.Parse(tIn.ReadLine());
        int[] A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        if (N == 1)
        {
            tOut.WriteLine("YES");
            tOut.WriteLine(A[0]);
            return;
        }

        Dictionary<int, int> D = new Dictionary<int, int>();
        foreach (int a in A)
        {
            if (!D.ContainsKey(a)) D[a] = 0;
            D[a]++;
        }

        bool ok = D.Count == N && A.Length == N * 2 - 1;

        if (ok)
        {
            Dictionary<int, int> C = new Dictionary<int, int>();
            foreach (int a in D.Keys)
            {
                int c = D[a];
                if (!C.ContainsKey(c)) C[c] = 0;
                C[c]++;
            }
            int[] V = C.Values.OrderBy(p => p).ToArray();

            ok = V[0] == 1 && V.Length == 1 || V[1] == 1;
            int ix = 2;
            while (ok && ix < V.Length)
            {
                ok = V[ix] == V[ix - 1] * 2;
                ix++;
            }
        }

        int[] AAA = new int[N * 2 - 1];
        if (ok)
        {
            int[][] DD = D.Keys.Select(p => new int[] { p, D[p] }).OrderByDescending(p => p[1]).ThenBy(p => p[0]).ToArray();
            AAA[0] = DD[0][0];
            int ix = 0;
            int w2 = 1;
            while (w2 < N)
            {
                List<int> dd = Enumerable.Range(0, w2).Select(p => DD[w2 + p][0]).ToList();
                for (int i = 0; i < w2; i++)
                {
                    AAA[(ix + i) * 2 + 1] = AAA[ix + i];
                    int dx = ~dd.BinarySearch(AAA[ix + i]);
                    AAA[(ix + i) * 2 + 2] = dd[dx];
                    dd.RemoveAt(dx);
                }
                ix += w2;
                w2 *= 2;
            }
        }

        tOut.WriteLine(ok ? "YES" : "NO");
        if (ok)
            tOut.WriteLine(string.Join(" ", AAA.Select(p => p.ToString())));

        tIn.ReadLine();
    }
}
