using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/june-world-codesprint/challenges/equal-stacks
/// </summary>
class Solution2
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5 3 4
3 2 1 1 1
4 3 2
1 1 4 1
");

        tIn.ReadLine();
        int[][] H = new int[3][];
        for (int i = 0; i < 3; i++)
        {
            H[i] = tIn.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).Reverse().ToArray();
            for (int j = 1; j < H[i].Length; j++) H[i][j] += H[i][j - 1];
        }

        HashSet<int> A = new HashSet<int>(H[0]);
        A.IntersectWith(H[1]);
        A.IntersectWith(H[2]);
        tOut.WriteLine(A.Count > 0 ? A.Max() : 0);

        tIn.ReadLine();
    }
}
