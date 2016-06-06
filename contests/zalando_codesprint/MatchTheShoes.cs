using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/zalando-codesprint/challenges/match-the-shoes
/// </summary>
class Solution1
{
    class ShoeComparer : IComparer<int>
    {
        public int[] Frequency { get; private set; }

        public ShoeComparer(int[] frequency)
        {
            Frequency = frequency;
        }

        public int Compare(int x, int y)
        {
            int comp = Frequency[y].CompareTo(Frequency[x]);
            if (comp == 0) comp = x.CompareTo(y);
            return comp;
        }
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"3 4 8
//2
//1
//2
//0
//3
//3
//1
//2
//");

        int[] kmn = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int K = kmn[0];
        int M = kmn[1];
        int N = kmn[2];

        int[] frequency = new int[M];
        for (int i = 0; i < N; i++) frequency[int.Parse(tIn.ReadLine())]++;

        ShoeComparer comparer = new ShoeComparer(frequency);
        List<int> suggestions = new List<int>();

        for (int ix = 0; ix < M; ix++)
        {
            if (suggestions.Count < K || comparer.Compare(ix, suggestions[K - 1]) < 0) {
                int index = ~suggestions.BinarySearch(ix, comparer);
                suggestions.Insert(index, ix);
                if (suggestions.Count > K) suggestions.RemoveAt(K);
            }
        }

        foreach (int x in suggestions) tOut.WriteLine(x);

//        Console.ReadLine();
    }
}
