using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/zalando-codesprint/challenges/which-warehouses-can-fullfill-these-orders
/// </summary>
class Solution5
{

    static long[,] WP = null;
    static int W = 0;
    static int P = 0;

    static int WNum(int ixw, long[] bp)
    {
        if (ixw == W) return bp.Any(p => p > 0) ? -1 : 0;
        int wnum1 = WNum(ixw + 1, bp);

        long[] bp2 = new long[P];
        Array.Copy(bp, bp2, P);
        for (int i = 0; i < P; i++) bp2[i] -= WP[ixw, i];
        int wnum2 = WNum(ixw + 1, bp2);

        if (wnum2 == -1) return wnum1;
        if (wnum1 == -1) return 1 + wnum2;

        return Math.Min(wnum1, 1 + wnum2);
    }


    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"2 3 2
//1 0
//0 1
//1 1
//2 0
//0 1
//");

        int[] wbp = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        W = wbp[0];
        int B = wbp[1];
        P = wbp[2];

        WP = new long[W, P];

        for (int i = 0; i < W; i++)
        {
            long[] x = tIn.ReadLine().Split().Select(p => long.Parse(p)).ToArray();
            for (int j = 0; j < P; j++) WP[i, j] = x[j];
        }

        for (int i = 0; i < B; i++)
        {
            long[] BP = tIn.ReadLine().Split().Select(p => long.Parse(p)).ToArray();

            tOut.WriteLine(WNum(0, BP));
        }

//        Console.ReadLine();
    }
}
