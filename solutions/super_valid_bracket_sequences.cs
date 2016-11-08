using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/ncr-codesprint/challenges/super-valid-bracket-sequences
/// </summary>
class Solution4
{
    const int R = 1000 * 1000 * 1000 + 7;
    const int N_MAX = 210;
    static int N = 0;
    static int K = 0;
    static Dictionary<int, int> mem = null;
    static int dp(int n, int k, int h, bool prev_open)
    {
        if (n == 0 && h == 0 && k >= K) return 1;
        if (h > n) return 0;
        if (h == n) {
            if (k >= K || prev_open && k + 1 == K) return 1;
            return 0;
        }
        int key = ((n * N_MAX + h) * N_MAX + k) * 10 + (prev_open ? 1 : 0);
        if (mem.ContainsKey(key)) return mem[key];

        mem[key] = (int)((long)dp(n - 1, prev_open ? k : k + 1, h + 1, true) + (h > 0 ? dp(n - 1, prev_open ? k + 1 : k, h - 1, false) : 0)) % R;

        return mem[key];
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5
2 1
8 5
20 0
6 0
100 0
");

        int Q = int.Parse(tIn.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            int[] nk = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
            N = nk[0];
            K = nk[1];
            mem = new Dictionary<int, int>();

            tOut.WriteLine(N % 2 == 0 ? dp(N, 0, 0, true) : 0);
        }

        tIn.ReadLine();
    }
}
