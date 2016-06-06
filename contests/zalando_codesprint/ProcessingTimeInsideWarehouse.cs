using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/zalando-codesprint/challenges/number-of-items-sorted-in-a-warehouse
/// </summary>
class Solution7
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"7 3
//1 2 3");

        int[] nm = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int N = nm[0];
        int M = nm[1];

        long[] P = tIn.ReadLine().Split().Select(p => long.Parse(p)).ToArray();
        Array.Sort(P);

        long X = 1;
        long Nx = 0;
        foreach (long p in P) Nx += X / p;
        while (Nx < N)
        {
            X *= 2;
            Nx = 0;
            foreach (long p in P) Nx += X / p;
        }

        long xL = X / 2;
        long xR = X;

        while (xL < xR)
        {
            long xM = (xL + xR) / 2;
            Nx = 0;
            foreach (long p in P) Nx += xM / p;
            if (Nx < N) xL = xM + 1;
            else xR = xM;
        }

        tOut.WriteLine(xR);

//        Console.ReadLine();
    }
}
