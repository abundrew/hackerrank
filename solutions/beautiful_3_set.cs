using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-6/challenges/beautiful-3-set
/// </summary>
class Solution6
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5");

        int N = int.Parse(tIn.ReadLine());
        int M = (N * 2 + 3) / 3;

        int[][] triples = new int[M][];
        int ix;
        int t3;

        switch (N % 3)
        {
            case 0:
            case 1:
                ix = 0;
                t3 = M - 1;
                while (ix < M && t3 >= 0)
                {
                    triples[ix] = new int[] { ix, N - ix - t3, t3 };
                    ix++;
                    t3 -= 2;
                }
                t3 = M - 2;
                while (ix < M)
                {
                    triples[ix] = new int[] { ix, N - ix - t3, t3 };
                    ix++;
                    t3 -= 2;
                }
                break;
            case 2:
                triples[0] = new int[] { 0, 0, N };
                ix = 1;
                t3 = M - 2;
                while (ix < M && t3 >= 0)
                {
                    triples[ix] = new int[] { ix, N - ix - t3, t3 };
                    ix++;
                    t3 -= 2;
                }
                t3 = M - 3;
                while (ix < M)
                {
                    triples[ix] = new int[] { ix, N - ix - t3, t3 };
                    ix++;
                    t3 -= 2;
                }
                break;
        }

        tOut.WriteLine(M);
        for (int i = 0; i < M; i++)
            tOut.WriteLine("{0} {1} {2}", triples[i][0], triples[i][1], triples[i][2]);

        tIn.ReadLine();
    }
}
