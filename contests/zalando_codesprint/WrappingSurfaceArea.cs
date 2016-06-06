using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/zalando-codesprint/challenges/wrappingsurfacearea
/// </summary>
class Solution3
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        //        tIn = new StringReader(@"8
        //2 2 2
        //");

        long N = int.Parse(tIn.ReadLine());
        int[] wlh = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        long W = wlh[0];
        long L = wlh[1];
        long H = wlh[2];

        long S = (W * L + (W + L) * H * N) * 2;

        for (long nW = 1; nW <= N; nW++)
            for (long nL = 1; nL <= (N + nW - 1) / nW; nL++)
            {
                long nH = (N + nW * nL - 1) / (nW * nL);
                long s = (nW * W * nL * L + nW * W * nH * H + nL * L * nH * H) * 2;
                if (s < S) S = s;
            }

        tOut.WriteLine(S);

        //Console.ReadLine();
    }
}
