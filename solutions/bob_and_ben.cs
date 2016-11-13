using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/university-codesprint/challenges/bob-and-ben
/// </summary>
class Solution4
{
    static bool Game(List<int> forest)
    {
        if (forest.Count == 0) return false;
        bool win = false;
        for (int i = 0; i < forest.Count; i++)
        {
            List<int> f = new List<int>(forest);
            f[i]--;
            if (f[i] == 0) f.RemoveAt(i);
            win = !Game(f);
            if (win) break;
            if (forest[i] > 2)
            {
                f = new List<int>(forest);
                f.RemoveAt(i);
                win = !Game(f);
                if (win) break;
            }
        }
        return win;
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"2
2
1 2
1 3
1
3 2
");

        int G = int.Parse(tIn.ReadLine());
        for (int g = 0; g < G; g++)
        {
            int N = int.Parse(tIn.ReadLine());
            int[] M = new int[N];
            for (int i = 0; i < N; i++)
                M[i] = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).First();

            int[] C = new int[4];
            foreach (int m in M) {
                int x = m;
                if (x > 2) x = x % 2 != 0 ? 3 : 4;
                C[x - 1]++;
            }
            List<int> T = new List<int>();
            for (int i = 0; i < 4; i++)
                if (C[i] % 2 != 0) T.Add(i + 1);

            tOut.WriteLine(Game(T) ? "BOB" : "BEN");
        }

        tIn.ReadLine();
    }
}
