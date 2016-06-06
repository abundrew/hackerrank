using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w20/challenges/simple-game
/// </summary>
class Solution4
{
    static Dictionary<string, long> DP = new Dictionary<string, long>();

    static long Binomial(int n, int m)
    {
        if (m == 0) return 1;
        if (n == 0) return 0;

        string key = string.Format("{0} {1}", n, m);
        if (DP.ContainsKey(key)) return DP[key];

        long b = (Binomial(n - 1, m) + Binomial(n - 1, m - 1)) % 1000000007;

        DP[key] = b;
        return b;
    }

    static void Main(String[] args)
    {
        int[] nmk = Console.ReadLine().Split(' ').Select(p => int.Parse(p)).ToArray();
        int N = nmk[0];
        int M = nmk[1];
        int K = nmk[2];

        long b = 0;

        if (K == 2)
        {
            int nm = N - M;
            if (nm % K != 0)
            {
                b = Binomial(nm + M - 1, nm);
            }
        }

        Console.WriteLine(b);
    }
}
