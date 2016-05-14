using System;
using System.Collections.Generic;

/// <summary>
/// https://www.hackerrank.com/contests/5-days-of-game-theory/challenges/a-game-of-stones
/// </summary>

class Solution1
{
    static Dictionary<int, bool> dp = new Dictionary<int, bool>();

    static bool Lost(int n)
    {
        if (n < 2) return true;
        if (dp.ContainsKey(n)) return dp[n];
        bool lost = true;
        if (n >= 2 && Lost(n - 2) || n >= 3 && Lost(n - 3) || n >= 5 && Lost(n - 5))
            lost = false;
        dp[n] = lost;
        return lost;
    }

    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine(Lost(N) ? "Second" : "First");
        }
    }
}