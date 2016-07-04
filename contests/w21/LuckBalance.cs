using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w21/challenges/luck-balance
/// </summary>
class Solution2
{
    static int N = 0;
    static int K = 0;
    static int[] L = null;
    static int[] T = null;

    static Dictionary<int, int> DP = new Dictionary<int, int>();

    static int Luck(int ix, int k)
    {
        if (ix == N) return 0;
        int key = ix * 1000 + k;
        if (DP.ContainsKey(key)) return DP[key];

        int luck = -L[ix] + Luck(ix + 1, k);
        if (T[ix] == 0 || k > 0)
        {
            int luck2 = L[ix] + Luck(ix + 1, k - (T[ix] != 0 ? 1 : 0));
            if (luck2 > luck) luck = luck2;
        }

        DP[key] = luck;
        return luck;
    }


    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"6 3
5 1
2 1
1 1
8 1
10 0
5 0
");

        int[] nk = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        N = nk[0];
        K = nk[1];
        L = new int[N];
        T = new int[N];
        for (int i = 0; i < N; i++)
        {
            int[] lt = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            L[i] = lt[0];
            T[i] = lt[1];
        }

        tOut.WriteLine(Luck(0, K));

        tIn.ReadLine();
    }
}
