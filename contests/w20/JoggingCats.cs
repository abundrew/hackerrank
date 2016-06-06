using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w20/challenges/cat-jogging
/// </summary>
class Solution5
{

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"4 6
1 2
2 3
3 4
4 1
1 3
2 4
");

        int[] nm = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int N = nm[0];
        int M = nm[1];

        List<int>[] R = new List<int>[N];
        for (int i = 0; i < N; i++) R[i] = new List<int>();

        for (int i = 0; i < M; i++)
        {
            int[] nn = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            R[nn[0] - 1].Add(nn[1] - 1);
            R[nn[1] - 1].Add(nn[0] - 1);
        }

        Dictionary<long, int> triads = new Dictionary<long, int>();

        for (int x = 0; x < N; x++)
            if (R[x].Count > 1)
            {
                R[x].Sort();
                for (int i = 0; i < R[x].Count - 1; i++)
                    for (int j = i + 1; j < R[x].Count; j++)
                    {
                        long key = (long)R[x][i] * 100000 + R[x][j];
                        if (!triads.ContainsKey(key)) triads[key] = 0;
                        triads[key]++;
                    }
            }

        int z = 0;
        foreach (int value in triads.Values)
            z += value * (value - 1) / 2;

        Console.WriteLine(z / 2);
    }
}
