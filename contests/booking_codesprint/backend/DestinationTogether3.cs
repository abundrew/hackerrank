using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/booking-passions-hacked-backend/challenges/destinations-together-3
/// </summary>
class Solution1
{
    static long Fact(int n)
    {
        if (n == 0) return 1;
        return Fact(n - 1) * n;
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3 4 2");

        int[] nmc = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int N = nmc[0] - 1;
        int M = nmc[1] - 1;
        int C = nmc[2] - 1;

        tOut.WriteLine(Fact(N - C + M - C + C));

        tIn.ReadLine();
    }
}
