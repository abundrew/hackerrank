using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/walmart-codesprint-algo/challenges/popsicle-stick-mountains
/// </summary>
class Solution4
{
    const int N_MAX = 4000;
    const int R = 1000 * 1000 * 1000 + 7;
    static Dictionary<int, int> mem = new Dictionary<int, int>();

    static int dp(int n, int h)
    {
        if (h > n) return 0;
        if (h == n) return 1;
        int key = (N_MAX + 1) * n + h;
        if (mem.ContainsKey(key)) return mem[key];

        mem[key] = (dp(n - 1, h + 1) + (h > 0 ? dp(n - 1, h - 1) : 0) + (h == 0 ? 1 : 0)) % R;
        return mem[key];
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"4
4000
6
20
100");

        for (int n = 1; n < N_MAX; n++) dp(n, 0);

        int T = int.Parse(tIn.ReadLine());
        for (int t = 0; t < T; t++)
        {
            int N = int.Parse(tIn.ReadLine());
            tOut.WriteLine(dp(N, 0) - 1);
        }

        tIn.ReadLine();
    }
}
