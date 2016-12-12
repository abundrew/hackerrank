using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/university-codesprint/challenges/array-construction
/// </summary>
class Solution6
{
    // -------------------------------------------------------------------------
    // A[0],A[1],A[2],...,A[N-1]                          # sorted
    // h[i]=A[i]-A[i-1]
    // S=A[0]*N+h[1]*(N-1)+h[2]*(N-2)+...+h[N-1]*1
    // K=h[1]*(N-1)*1+h[2]*(N-2)*2+...+h[N-1]*1*(N-1)
    // -------------------------------------------------------------------------
    static int N = 0;
    static int S = 0;
    static int K = 0;
    static int[] h = null;
    static bool[] hs = new bool[51 * 201 * 2001];
    static bool Construct(int ix, int sx, int kx)
    {
        if (sx == 0 && kx == 0) {
            for (int i = ix; i < N; i++) h[i] = 0;
            return true;
        }
        if (ix == N || sx <= 0 || kx <= 0) return false;

        long key = ((long)sx * (K + 1) + kx) * (N + 1) + ix;
        if (hs[key]) return false;

        h[ix] = 0;
        int sxx = sx;
        int kxx = kx;
        int dsx = (N - ix);
        int dkx = (N - ix) * ix;
        while (true)
        {
            if (sxx < 0) break;
            if (kxx < 0) break;
            if (Construct(ix + 1, sxx, kxx)) return true;
            h[ix]++;
            sxx -= dsx;
            kxx -= dkx;
        }

        hs[key] = true;
        return false;
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"20
50 200 1860
30 200 2000
2 200 2
50 0 0
40 190 1873
4 200 0
48 180 1990
11 200 2000
7 200 200
15 180 1982
50 200 1900
50 200 1948
10 200 990
50 200 1964
47 174 1899
21 200 1530
44 177 1721
15 159 700
36 197 1900
50 3 141
");

        int Q = int.Parse(tIn.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            int[] nsk = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
            N = nsk[0];
            S = nsk[1];
            K = nsk[2];

            if (N == 1)
            {
                tOut.WriteLine(K == 0 ? S : -1);
                continue;
            }

            h = new int[N];
            int[] A = new int[N];

            Array.Clear(hs, 0, hs.Length);

            bool built = false;

            A[0] = 0;
            while (A[0] * N <= S)
            {
                built = Construct(1, S - A[0] * N, K);
                if (built)
                {
                    for (int i = 1; i < N; i++) A[i] = A[i - 1] + h[i];
                    break;
                }
                A[0]++;
            }
            tOut.WriteLine(built ? string.Join(" ", A.Select(p => p.ToString()).ToArray()) : "-1");
        }

        tIn.ReadLine();
    }
}
