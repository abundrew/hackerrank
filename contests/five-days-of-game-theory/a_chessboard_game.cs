using System;
using System.Collections.Generic;

/// <summary>
/// https://www.hackerrank.com/contests/5-days-of-game-theory/challenges/day-1-a-chessboard-game
/// </summary>

class Solution3
{
    static Dictionary<string, bool> dp = new Dictionary<string, bool>();

    static string ToKey(int x, int y)
    {
        return string.Format("{0} {1}", x, y);
    }

    static int[] DX = new int[] { -2, -2, 1, -1 };
    static int[] DY = new int[] { 1, -1, -2, -2 };

    static bool Lost(int x, int y)
    {
        if (x < 3 && y < 3) return true;
        string key = ToKey(x, y);
        if (dp.ContainsKey(key)) return dp[key];
        bool lost = true;
        for (int i = 0; i < 4; i++)
            if (x + DX[i] > 0 && y + DY[i] > 0 && Lost(x + DX[i], y + DY[i]))
            {
                lost = false;
                break;
            }
        dp[key] = lost;
        return lost;
    }

    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            string[] ss = Console.ReadLine().Split(' ');
            int x = int.Parse(ss[0]);
            int y = int.Parse(ss[1]);
            Console.WriteLine(Lost(x, y) ? "Second" : "First");
        }
    }
}