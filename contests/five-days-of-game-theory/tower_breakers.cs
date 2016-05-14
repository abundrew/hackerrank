using System;
using System.Collections.Generic;

/// <summary>
/// https://www.hackerrank.com/contests/5-days-of-game-theory/challenges/tower-breakers
/// </summary>

class Solution2
{
    static Dictionary<int, bool> dp = new Dictionary<int, bool>();

    static bool Lost(int m)
    {
        if (m == 1) return true;
        if (dp.ContainsKey(m)) return dp[m];
        bool lost = true;
        int rm = (int)Math.Sqrt(m);
        for (int x = 1; x <= rm; x++)
            if (x < m && m % x == 0 && (Lost(x) || x > 1 && Lost(m / x)))
            {
                lost = false;
                break;
            }
        dp[m] = lost;
        return lost;
    }

    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            string[] ss = Console.ReadLine().Split(' ');
            int N = int.Parse(ss[0]);
            int M = int.Parse(ss[1]);

            if (N % 2 == 0)
            {
                Console.WriteLine("2");
                continue;
            }

            Console.WriteLine(Lost(M) ? "2" : "1");
        }
    }
}